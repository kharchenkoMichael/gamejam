using UnityEngine;

public class CapsulScript : MonoBehaviour
{
	public NewBehaviourScript SignalR;
	public Material MyMaterial;
	public Material OponentMaterial;

	public float Spead;
	public float Rotation;

	public string Name = string.Empty;

	public void SetName(string name)
	{
		Name = name;
		gameObject.GetComponent<MeshRenderer>().materials[0] = SignalR.name == Name ? MyMaterial : OponentMaterial;
	}

	// Update is called once per frame
	void Update()
	{
		if (SignalR._name != Name)
			return;

		var vector = transform.position;
		vector.z += Spead * Input.GetAxis("Vertical");
		vector.x += Spead * Input.GetAxis("Horizontal");
		transform.position = vector;
		
		if (SignalR != null)
			SignalR.UpdateCapsul(transform);
	}
}
