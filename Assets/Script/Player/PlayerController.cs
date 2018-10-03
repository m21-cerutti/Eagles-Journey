using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public bool Boost
	{
		get { return Input.GetButton("Boost"); }
	}
	public bool Camera
	{
		get { return Input.GetButtonDown("Camera"); }
	}
	public bool Menu
	{
		get { return Input.GetButtonDown("Escape"); }
	}


	//Axis

	public float Horizontal
	{
		get { return Input.GetAxisRaw("Horizontal"); }
	}

	public float Vertical
	{
		get { return Input.GetAxisRaw("Vertical"); }
	}

	public float Acceleration
	{
		get { return Input.GetAxisRaw("Acceleration"); }
	}
}
