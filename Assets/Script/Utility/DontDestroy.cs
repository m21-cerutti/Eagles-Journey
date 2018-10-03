using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

	public bool Dont_Destroy;
	// Use this for initialization
	void Start () {
		if (Dont_Destroy)
		{
			DontDestroyOnLoad(gameObject);
		}
	}
	
}
