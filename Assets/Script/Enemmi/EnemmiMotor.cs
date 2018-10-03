
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemmiMotor : MonoBehaviour
{

	public float max_speed;
	public float radius_detection;
	public float player_detection;

	public float boost;
	public float time_boost;
	public bool boost_active;
	bool player_detected;
	float timer;

	public bool in_map;

	public float speed;
	float speed_reaction = 0.01f;
	Vector3 rotation;
	Rigidbody rgb;
	bool started_corout;

	void computeEscape()
	{
		if (in_map)
		{
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius_detection);
			if (hitColliders != null)
			{
				Vector3 newRotation = Vector3.zero;
				foreach (Collider col in hitColliders)
				{
					if (col.gameObject.tag != "Map" && col.gameObject.tag != "Enemmi")
					{
						if (col.gameObject.tag == "Player")
						{
							Vector3 Dirplayer = col.gameObject.transform.position - transform.position;
							//Debug.Log(Dirplayer.sqrMagnitude);
							if (Dirplayer.sqrMagnitude < player_detection)
							{
								newRotation = newRotation - Dirplayer;
								player_detected = true;
							}
							else
							{
								player_detected = false;
							}
						}
						else
						{
							player_detected = false;
							newRotation = newRotation + transform.position - col.gameObject.transform.position;
						}
					}
				}
				newRotation = Vector3.Normalize(newRotation);
				rotation = Vector3.Lerp(rotation, newRotation, speed_reaction);
			}
		}else
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			Vector3 newRotation = player.transform.position - transform.position;
			newRotation = Vector3.Normalize(newRotation);
			rotation = Vector3.Lerp(rotation, newRotation, speed_reaction);
		}
	}

	void boost_player()
	{
		if (player_detected && timer >= time_boost)
		{
			boost_active = true;
			speed = speed + boost;
			timer -= Time.deltaTime;
		}
		else if (boost_active && timer > 0)
		{
			speed = speed + boost;
			timer -= Time.deltaTime;
		}
		else if (timer < time_boost)
		{
			boost_active = false;
			timer += Time.deltaTime;
		}

	}

	void Start()
	{
		rgb = GetComponent<Rigidbody>();
		player_detected = false;
		in_map = true;
	}

	void FixedUpdate()
	{
		speed = max_speed;
		computeEscape();
		boost_player();

		if (rotation != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(rotation);
		}
		

		rgb.velocity = Vector3.zero;
		rgb.AddRelativeForce(0f, 0f, speed, ForceMode.VelocityChange);

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color32(255, 0, 0, 125);
		Gizmos.DrawWireSphere(transform.position, radius_detection);

		Gizmos.DrawRay(transform.position, transform.position + rotation);
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		foreach (ContactPoint contact in collisionInfo.contacts)
		{
			if (contact.otherCollider.gameObject.tag == "Player")
			{
				if (!started_corout)
				{
					StartCoroutine(Win(12f));
				}
			}
		}
	}

	public IEnumerator Win(float time)
	{
		GameObject.Find("Camera").GetComponent<CameraScript>().win = true;
		MusicManager.Instance.win = true;
		started_corout = true;
		yield return new WaitForSecondsRealtime(time);
		HUDManager.Instance.changeEndGame();
	}
}


