using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

	GameObject player;
	GameObject enemmi;

	bool followTarget;
	public bool win;
	public float timer;

	void Start()
	{
		player = GameObject.Find("Player");
		enemmi = GameObject.Find("Target");
		followTarget = false;
		win = false;
	}


	void LateUpdate()
	{

		transform.position = player.transform.position;
		if (win)
		{
			Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, 0.01f);
			float fov = GetComponentInChildren<Camera>().fieldOfView;
			GetComponentInChildren<Camera>().fieldOfView = Mathf.Lerp(fov, 160, 0.01f);

		}else
		{
			if (player.GetComponent<PlayerController>().Camera)
			{
				followTarget = !followTarget;
			}
			if (player)


				if (followTarget)
				{
					transform.rotation = Quaternion.LookRotation(enemmi.transform.position - player.transform.position);
				}
				else
				{
					transform.rotation = player.transform.rotation;
				}
		}
	}
}
