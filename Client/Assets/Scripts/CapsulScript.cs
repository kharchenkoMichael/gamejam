using System;
using Model.Dto;
using UnityEngine;

public class CapsulScript : MonoBehaviour
{
	public NewBehaviourScript SignalR;

	public float Spead;

	public string Name = string.Empty;

	public void SetName(string name, NewBehaviourScript signalR)
	{
	  SignalR = signalR;
		Name = name;
	}

	// Update is called once per frame
	void Update()
	{
		if (SignalR == null || SignalR._name != Name)
			return;

		var vector = transform.position;
	  var zMove = Input.GetAxis("Vertical");
    var yMove = Input.GetAxis("Horizontal");
    if (Math.Abs(zMove) < 0.01 && Math.Abs(yMove) < 0.01)
      return;

    vector.z += Spead * zMove;
		vector.x += Spead * yMove;
		transform.position = vector;

    SignalR.UpdateCapsul(transform);
	}

  public void SetByUser(UserDto user)
  {
    transform.position = new Vector3(user.Position.PositionX, user.Position.PositionY, user.Position.PositionZ);
  }
}
