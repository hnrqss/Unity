using UnityEngine;
using System.Collections;

public class MoveShip : MonoBehaviour {

	// Use this for initialization
	public float moveSpeed = 5.0f;
	public float drag = 0.5f;
	public float terminalRotationSpeed = 25.0f;
	public Vector3 moveVector{set;get;}
	public VirtualJoystick joystick;

	public Rigidbody thisRigidbody;

	void Start () {
		//thisRigidbody = gameObject.AddComponent<Rigidbody>();
		thisRigidbody.maxAngularVelocity = terminalRotationSpeed;
		thisRigidbody.drag = drag;
	}
	
	// Update is called once per frame
	void Update () 
	{
		moveVector = PoolInput();
		move();
	}

	private void move()
	{
		thisRigidbody.AddForce((moveVector*moveSpeed));
	}
	private Vector3 PoolInput()
	{
		Vector3 dir = Vector3.zero;
		//dir.x  = Input.GetAxis("Horizontal");
		//dir.z  = Input.GetAxis("Vertical");
		dir.x = joystick.Horizontal();
		dir.y = joystick.Vertical();
		//transform.rotation = Quaternion.AngleAxis(30, dir);
		if(dir.magnitude > 1)
			dir.Normalize();
		return dir;


	}
}
