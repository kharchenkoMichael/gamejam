using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR.Client;
using Model;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
	public Text Text;
	public GameObject Capsule;
	public GameObject UI;

	public Text Debug;
	private string _debug;
	private bool _debugChanged = false;
	
	private List<GameObject> _users = new List<GameObject>();
	public string _name = string.Empty;

	private bool _refresh = false;
	private bool _create = false;

	public string signalUrl;

	private HubConnection _hubConnection = null;

	private IHubProxy _hubProxy;
	
	// Use this for initialization
	IEnumerator Start()
	{
		_debug += "Start()\n";
		yield return new WaitForSeconds(1);
		_debug += "Start() 1 second.\n";

		StartSignalR();
		_debugChanged = true;
	}

	private void StartSignalR()
	{
		_debug += "StartSignalR\n";
		if (_hubConnection == null)
		{
			_hubConnection = new HubConnection(signalUrl);
			_debug += signalUrl + "\n";
			_hubConnection.Error += HubConnection_Error;

			_hubProxy = _hubConnection.CreateHubProxy("MyHub");
			_hubProxy.On<List<User>>("refresh",
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
				_debug += $"{change.NewState} {change.OldState}\n";
				_debugChanged = true;
			}; 
			
			_debug += "_hubConnection.Start();\n";
		}
		else
		{
			_debug += "Signalr  already connected...\n";
		}
	}
	
	#region Callbacks

	
	public void Create()
	{
		_hubProxy.Invoke("create", Text.text);
		_name = Text.text;
		_create = true;
		
		_debug += "Create;\n";
		_debugChanged = true;
	}
	
	public void UpdateCapsul(Transform myTransform)
	{
		var user = GameContext.Instance.Users.Find(item => item.Name == _name);
		user.Update(myTransform);
		_hubProxy.Invoke("update", user);
	}

	public void ResetDebug()
	{
		Debug.text = string.Empty;
	}

	#endregion

	void OnAppliacationQuit()
	{
		
		_debug += $"OnAppliacationQuit() {Time.time} seconds\n";
		_debugChanged = true;
		_hubConnection.Error -= HubConnection_Error;
		_hubConnection.Stop();
	}
	
	private void HubConnection_Error(Exception obj)
	{
		
		_debug += "Hub Error - " + obj.Message + Environment.NewLine+
		          obj.Data+Environment.NewLine+
		          obj.StackTrace +Environment.NewLine+
		          obj.TargetSite +"\n";
		_debugChanged = true;
	}

	private void hubConnection_Closed()
	{
		_debug += "_hubConnection.Start();\n";
		_debugChanged = true;
	}
	

	// Update is called once per frame
	void Update()
	{
		if (_refresh)
			RefreshUpdate();
		if (_create)
		{
			UI.SetActive(false);
			_create = false;
		}

		if (_debugChanged)
		{
			Debug.text += _debug;
			_debugChanged = false;
		}
	}

	private void RefreshUpdate()
	{
		foreach (var user in GameContext.Instance.Users)
		{
			var prefab = _users.FirstOrDefault(item => item.GetComponent<CapsulScript>().Name == user.Name);
			if (prefab == null)
			{
				var obj = Instantiate(Capsule, new Vector3(user.PositionX, user.PositionY, user.PositionZ),
					new Quaternion(user.RotationX, user.RotationY, user.RotationZ, 0));
				var script = obj.GetComponent<CapsulScript>();
				script.SignalR = this;
				script.SetName(user.Name);
				_users.Add(obj);
				continue;
			}

			if (user.Name == _name)
				continue;

			prefab.transform.position = new Vector3(user.PositionX, user.PositionY, user.PositionZ);
			prefab.transform.rotation = new Quaternion(user.RotationX, user.RotationY, user.RotationZ, 0);
		}
		
		_refresh = false;
	}
}
