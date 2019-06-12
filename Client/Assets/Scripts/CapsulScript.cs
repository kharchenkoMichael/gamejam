using System;
using Model;
using Model.Dto;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CapsulScript : MonoBehaviour
{
	public NewBehaviourScript SignalR;

	public float Spead;

	public string Name = string.Empty;
  public CharacterController Controller;

	private Animator _animator;
	private int _spellHash;

	public void SetName(string name, NewBehaviourScript signalR)
	{
	  SignalR = signalR;
		Name = name;
	}

	public void PlaySpell()
	{
		_animator.SetTrigger(_spellHash);
	}

	private void OnEnable()
	{
		_animator = gameObject.GetComponent<Animator>();
		_spellHash = Animator.StringToHash("Spell");
	}
	

	// Update is called once per frame
	private void Update()
	{
		if (SignalR == null || SignalR.Name != Name)
			return;

		var vector = Vector3.zero;
	  var zMove = Input.GetAxis("Vertical");
    var yMove = Input.GetAxis("Horizontal");
    if (Math.Abs(zMove) < 0.01 && Math.Abs(yMove) < 0.01)
      return;

    vector.z -= Spead * zMove;
		vector.x -= Spead * yMove;

	  Controller.Move(vector);

		var user = GameContext.Instance.Users.Find(item => item.Name == Name);
		user.Position.Update(transform.position);
    SignalR.UpdateCapsul(user);
	}

  public void SetByUser(UserDto user)
  {
    transform.position = new Vector3(user.Position.PositionX, user.Position.PositionY, user.Position.PositionZ);
  }
}
