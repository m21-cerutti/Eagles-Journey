using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionTeleport : MonoBehaviour {


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemmi")
		{
			other.gameObject.GetComponent<EnemmiMotor>().in_map = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{

		//Debug.Log(other.gameObject.name);
		if (other.gameObject.tag == "Cube")
		{
			StartCoroutine(other.gameObject.GetComponent<ObstaclesScript>().poolCube());
		}
		if (other.gameObject.tag == "Enemmi")
		{
			other.gameObject.GetComponent<EnemmiMotor>().in_map = false;
		}
	}

}
