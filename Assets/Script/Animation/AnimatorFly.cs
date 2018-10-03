using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFly : MonoBehaviour {

	Animator anim;
	EnemmiMotor enemmi;
	PlayerMotor player;
	

	void Start()
	{
		anim = GetComponent<Animator>();
		player = GetComponent<PlayerMotor>();
		enemmi = GetComponent<EnemmiMotor>();

	}


	void Update()
	{
		float move = 0;
		bool boost=false;

		if (enemmi != null)
		{
			move = enemmi.speed;
			boost = enemmi.boost_active;
		}
		else if (player!= null)
		{
			move = player.speed;
			boost = player.boost_anim;
		}
		
		anim.SetFloat("Speed", move);
		anim.SetBool("Boost", boost);
	}
}
