
using UnityEngine;
using UnityEngine.UI;

public class RoomScript : MonoBehaviour
{
	private int _roomId;
	private NewBehaviourScript _signalR;
	private StartFormScript _startForm;

	public Text RoomName;
	public Text UserName;
	public Image RoomAvatar;
	
	public void SetRoom(int roomId, NewBehaviourScript signalR, StartFormScript startFormScript, string roomName, string userName)
	{
		_roomId = roomId;
		_signalR = signalR;
		_startForm = startFormScript;
		RoomName.text = roomName;
		UserName.text = userName;
	}

	public void ConectToRoom()
	{
		if (string.IsNullOrEmpty(_startForm.Name.text))
			return;
		
		_signalR.JoinToRoom(_roomId, _startForm.Name.text, _startForm.CurAvatar);
	}
}
