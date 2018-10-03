using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

	/*Control
	 * */
	public PlayerController control;


	/*Movement
	 * */
	public float speed_forward;
	public float angle_up;
	public float angle_turn;
	public float boost;
	public float time_boost;
	public float max_speed_forward;
	public float min_speed_forward;

	public bool boost_anim;

	private Rigidbody rb;
	public float speed;
	public Vector3 localRotation;
	float timer;

	public void Move()
	{

		//Get local velocity
		//Vector3 currentVelocity = rb.velocity;
		//Vector3 currentVelocityLocal = transform.InverseTransformDirection(currentVelocity);
		

		//Calculation
		rb.velocity = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		
		

		//Speed
		speed = speed + speed_forward*control.Acceleration;

			

		//Rotation
		localRotation = new Vector3(Mathf.Clamp(localRotation.x+ angle_up * control.Vertical, -90, 90), localRotation.y+angle_turn*control.Horizontal, 0f);
		
		//Limit
		Limit();

		//Boost
		if (control.Boost && timer > 0)
		{
			boost_anim = true;
			speed = speed + boost;
			timer -= Time.deltaTime;
		}
		else if (control.Boost && timer < 0)
		{
			boost_anim = false;
		}
		else if (!control.Boost && timer < time_boost)
		{
			boost_anim = false;
			timer += Time.deltaTime;
		}


		// Compute
		rb.AddRelativeForce(0f, 0f, speed, ForceMode.VelocityChange);
		this.transform.localRotation = Quaternion.Euler(localRotation);


	}

	public void Limit()
	{
		speed = Mathf.Clamp(speed, min_speed_forward, max_speed_forward);

	}
	
	public void Start()
	{
		boost_anim = false;
		timer = -1;
		rb = GetComponent<Rigidbody>();
		control = GetComponent<PlayerController>();
	}

	public void FixedUpdate()
	{
		if (HUDManager.Instance.state != stateMenu.Pause)
		{
			Move();
		}
	}

	
}
