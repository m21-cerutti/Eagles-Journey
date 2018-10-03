using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour {



	
	public IEnumerator poolCube()
	{
		yield return new WaitForSeconds(0.25f);
		MapManager.Instance.repullCube(this.gameObject);
	}
	
}
