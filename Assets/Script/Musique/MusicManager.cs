using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager :  SingletonBehaviour<MusicManager>
{

	public GameObject audioFond;
	GameObject Audio;
	public bool win;

	new void Awake ()
	{
		base.Awake();
		if (!GameObject.Find ("AudioBackground(Clone)")) {
			Audio= Instantiate(audioFond);
			Audio.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
		}	
	}

	private void Update()
	{
		if (win && Audio != null)
		{
			Audio.GetComponent<AudioSource>().volume = Mathf.Lerp(Audio.GetComponent<AudioSource>().volume, 0f, 0.02f);
		}
	}
}
