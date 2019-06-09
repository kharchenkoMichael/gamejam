using System.Linq;
using Model;
using UnityEngine;
using UnityEngine.UI;

public class StartFormScript : MonoBehaviour
{
	public NewBehaviourScript SignalR;
	public Text Name;
	public Image Avatar;
	public int CurAvatar;

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void CreateRoom()
	{
	  if (string.IsNullOrEmpty(Name.text))
	  {
      SignalR.OpenPopup("введи имя");
	    return;
	  }

	  if (GameContext.Instance.Rooms.Values.Count(item => item.isActive) >= 2)
	  {
	    SignalR.OpenPopup("Извини, пока доступно только две комнаты и обе уже заняты");
	    return;
    }
		
		SignalR.CreateRoom(CurAvatar, Name.text);
	}
}
