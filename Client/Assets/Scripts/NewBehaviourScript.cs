using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Model.Dto;
using Assets.Scripts.Model.Entities;
using Microsoft.AspNet.SignalR.Client;
using Model;
using Model.Dto;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Model.MagicFolder;
using Assets.Scripts;

public enum Form
{
	LoadingForm,
	StartForm,
	RoomForm
}

public class NewBehaviourScript : MonoBehaviour
{
	public GameObject Capsule;

	public Dictionary<int, GameObject> Forms = new Dictionary<int, GameObject>();

	public GameObject LoadingForm;
  public GameObject StartForm;
	public GameObject RoomForm;
  public GameObject MagicContainer;
	public GameObject[] Rooms;
	
	private List<GameObject> _users = new List<GameObject>();
	public string _name = string.Empty;

	private bool _refreshUser = false;
	private bool _refreshRoom = false;

	public string signalUrl;

	private HubConnection _hubConnection = null;

	private IHubProxy _hubProxy;

  private bool _magicUpdated;
  private void InitializeDictionary()
	{
		Forms[(int) Form.LoadingForm] = LoadingForm;
		Forms[(int) Form.StartForm] = StartForm;
		Forms[(int) Form.RoomForm] = RoomForm;
	}
	// Use this for initialization
	IEnumerator Start()
	{
		InitializeDictionary();
		OpenForm(Form.LoadingForm);
		Debug.Log("Start()");
		yield return new WaitForSeconds(1);
		
		Debug.Log("Start() 1 second.");
		StartSignalR();
		OpenForm(Form.StartForm);
    InitializeMagic();
  }
  private void InitializeMagic()
  {
    var magic =  MagicContainer.GetComponent<MagicContainerScript>();
	  magic.InitializeMagic();
	  var elements = magic.Elements;
    foreach (var element in elements)
    {
      element.Value.GetComponent<MagicScript>().ActionDelegate += ChooseMagic;
    }
  }

  private void StartSignalR()
	{
		Debug.Log("StartSignalR");
		if (_hubConnection == null)
		{
			_hubConnection = new HubConnection(signalUrl);
			Debug.Log(signalUrl);
			_hubConnection.Error += HubConnection_Error;

			_hubProxy = _hubConnection.CreateHubProxy("MyHub");
			_hubProxy.On<List<UserDto>>("refreshUsers", RefreshUsers);
			_hubProxy.On<RoomUpdateDto>("refreshRoomIds", RefreshRoom);
			
			_hubConnection.Start().Wait();
			_hubConnection.StateChanged += change =>
			{
				Debug.Log($"{change.NewState} {change.OldState}");
			}; 
			Debug.Log("_hubConnection.Start();");
			GetRoomIds();
		}
		else
		{
			Debug.Log("Signalr  already connected...");
		}
	}

	private void RefreshUsers(List<UserDto> users)
	{
		var myUsers = GameContext.Instance.Users;
		foreach (var user in users)
		{
			var curUser = myUsers.FirstOrDefault(item => item.Name == user.Name);
			if (curUser == null)
				myUsers.Add(user);
			else
				curUser.Clone(user);
		}

		_refreshUser = true;
	}
	
	private void RefreshRoom(RoomUpdateDto dto)
	{
		GameContext.Instance.Rooms = dto.Rooms.ToDictionary(item => item.Id, item => item);
		
		_refreshRoom = true;
	}

	#region Callbacks

	public void OpenForm(Form form)
	{
		foreach (var formsValue in Forms.Values)
		{
			formsValue.SetActive(false);
		}
		Forms[(int) form].SetActive(true);
	}
	
	public void GetRoomIds()
	{
		_hubProxy.Invoke("getRoomIds");
		
		Debug.Log("GetRoomIds;\n");
	}
	
	public void CreateRoom(int avatarId, string myName)
	{
		_name = myName;
		_hubProxy.Invoke("createRoom", myName, avatarId );
		OpenForm(Form.RoomForm);
		Debug.Log("CreateRoom;\n");
	}
	
	public void JoinToRoom(int roomId, string myName, int avatarId)
	{
		_name = myName;
		_hubProxy.Invoke("joinToRoom", myName, avatarId, roomId);
		OpenForm(Form.RoomForm);
		Debug.Log("ConnectToRoom;\n");
	}
	
	public void UpdateCapsul(Transform myTransform)
	{
		var user = GameContext.Instance.Users.Find(item => item.Name == _name);
		user.Position.Update(myTransform.position);
		_hubProxy.Invoke("update", user);
	}

	#endregion

	void OnAppliacationQuit()
	{
		Debug.Log($"OnAppliacationQuit() {Time.time} seconds");
		_hubConnection.Error -= HubConnection_Error;
		_hubConnection.Stop();
	}
	
	private void HubConnection_Error(Exception obj)
	{
		Debug.Log("Hub Error - " + obj.Message + Environment.NewLine+
		          obj.Data+Environment.NewLine+
		          obj.StackTrace +Environment.NewLine+
		          obj.TargetSite);
	}

	private void hubConnection_Closed()
	{
		Debug.Log("Closed");
	}
	

	// Update is called once per frame
	void Update()
	{
		if (_refreshUser)
			RefreshUserUpdate();
		if (_refreshRoom)
			RefreshRoomUpdate();
	}

	private void RefreshUserUpdate()
	{
		foreach (var user in GameContext.Instance.Users)
		{
			var prefab = _users.FirstOrDefault(item => item.GetComponent<CapsulScript>().Name == user.Name);
			if (prefab == null)
			{
				var obj = Instantiate(Capsule,
					new Vector3(user.Position.PositionX, user.Position.PositionY, user.Position.PositionZ), Quaternion.identity);
				var script = obj.GetComponent<CapsulScript>();
				script.SignalR = this;
				script.SetName(user.Name);
				_users.Add(obj);
				continue;
			}

			var elements = MagicContainer.GetComponent<MagicContainerScript>().Elements;
			foreach (var magicId in user.Magic)
			{
				elements[magicId].SetActive(false);
			}


			if (user.Name == _name)
				continue;

			prefab.transform.position = new Vector3(user.Position.PositionX, user.Position.PositionY, user.Position.PositionZ);
		}

		_refreshUser = false;
    _magicUpdated = false;
	}
	
	private void RefreshRoomUpdate()
	{
		var i = 0;
		foreach (var room in GameContext.Instance.Rooms)
		{
			var userCreator = room.Value.Users.FirstOrDefault();
			if (userCreator == null)
			{
				Rooms[i].SetActive(false);
				i++;
				continue;
			}
			
			Rooms[i].GetComponent<RoomScript>().SetRoom(room.Key, this, StartForm.GetComponent<StartFormScript>(), room.Value.Name, userCreator.Name);
			i++;
		}

		_refreshRoom = false;
	}

	private void ChooseMagic(int Id)
	{
		var user = GameContext.Instance.Users.Find(item => item.Name == _name);
		if (GameContext.Instance.Rooms[user.RoomId].Users[0].Id == user.Id
		    && (!user.Magic.Any()
		        || GameContext.Instance.Rooms[user.RoomId].Users[1].Magic.Count == 2)) 
		{
			user.Magic.Add(Id);
			_hubProxy.Invoke("update", user);
			Debug.Log("ChooseMagic Creator;\n");
		}
		else if (GameContext.Instance.Rooms[user.RoomId].Users[0].Id != user.Id
		         && (GameContext.Instance.Rooms[user.RoomId].Users[0].Magic.Any()))
		{
			user.Magic.Add(Id);
			_hubProxy.Invoke("update", user);
			Debug.Log("ChooseMagic Not creator;\n");
		}
	}
}
