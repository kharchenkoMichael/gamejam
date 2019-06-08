using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR.Client;
using Model;
using Model.Dto;
using UnityEngine;
using UnityEngine.UI;

public enum Form
{
	LoadingForm,
	StartForm,
	RoomForm
}

public class NewBehaviourScript : MonoBehaviour
{
	public Text Text;
	public GameObject Capsule;

	public Dictionary<int, GameObject> Forms = new Dictionary<int, GameObject>();

	public GameObject LoadingForm;
	public GameObject StartForm;
	public GameObject RoomForm;
	
	private List<GameObject> _users = new List<GameObject>();
	public string _name = string.Empty;

	private bool _refresh = false;
	private bool _create = false;

	public string signalUrl;

	private HubConnection _hubConnection = null;

	private IHubProxy _hubProxy;

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
	}

	private void StartSignalR()
	{
		Debug.Log("StartSignalR");
		if (_hubConnection == null)
		{
			_hubConnection = new HubConnection(signalUrl);
			Debug.Log("signalUrl");
			_hubConnection.Error += HubConnection_Error;

			_hubProxy = _hubConnection.CreateHubProxy("MyHub");
			_hubProxy.On<List<UserDto>>("refresh",
				(users) =>
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

					_refresh = true;
				});
			
			_hubConnection.Start().Wait();
			_hubConnection.StateChanged += change =>
			{
				Debug.Log($"{change.NewState} {change.OldState}");
			}; 
			
			Debug.Log("_hubConnection.Start();");
		}
		else
		{
			Debug.Log("Signalr  already connected...");
		}
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
	
	public void Create()
	{
		_hubProxy.Invoke("create", Text.text);
		_name = Text.text;
		_create = true;
		
		Debug.Log("Create;\n");
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
		if (_refresh)
			RefreshUpdate();
	}

	private void RefreshUpdate()
	{
		foreach (var user in GameContext.Instance.Users)
		{
			var prefab = _users.FirstOrDefault(item => item.GetComponent<CapsulScript>().Name == user.Name);
			if (prefab == null)
			{
				var obj = Instantiate(Capsule, new Vector3(user.Position.PositionX, user.Position.PositionY, user.Position.PositionZ),Quaternion.identity);
				var script = obj.GetComponent<CapsulScript>();
				script.SignalR = this;
				script.SetName(user.Name);
				_users.Add(obj);
				continue;
			}

			if (user.Name == _name)
				continue;

			prefab.transform.position = new Vector3(user.Position.PositionX, user.Position.PositionY, user.Position.PositionZ);
		}
		
		_refresh = false;
	}
}
