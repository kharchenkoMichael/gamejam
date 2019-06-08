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
			return;
		
		SignalR.CreateRoom(CurAvatar, Name.text);
	}
}
