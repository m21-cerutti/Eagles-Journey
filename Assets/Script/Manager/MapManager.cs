using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonBehaviour<MapManager> {
	
		
	public Vector3 sizeArea;
	GameObject GameZone;
	public GameObject[] prefab_cube;
	public int number_cube_max;
	public float max_size_cube;
	public float max_speed_cube;
	public float max_torque_cube;

	public float concentration;
	
	public void InitializeMap()
	{
		GameZone = new GameObject();
		GameZone.name = "GameZone";
		GameZone.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
		GameZone.transform.localPosition = new Vector3(0, 0, 0);
		GameZone.transform.localScale = sizeArea;

		//Box collider
		GameZone.AddComponent<BoxCollider>();
		BoxCollider box = GameZone.GetComponent<BoxCollider>();
		box.size = new Vector3(1, 1, 1);
		box.center = new Vector3(0, 0, 0);
		box.isTrigger = true;
		//Informations
		GameZone.tag = "Map";
		GameZone.AddComponent<Rigidbody>();
		Rigidbody rgb = GameZone.GetComponent<Rigidbody>();
		rgb.isKinematic = true;

		GameZone.AddComponent<DetectionTeleport>();
		GameZone.GetComponent<DetectionTeleport>();

		//Cube
		for(int i =0;i< number_cube_max; i++)
		{
			spawnAleatoryCube();
		}
		

	}

	public void repullCube(GameObject cube)
	{
		Vector3 velocity = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity;
		float ranX = Random.Range(-(sizeArea.x- concentration) / 2, (sizeArea.x - concentration) / 2);
		float ranY = Random.Range(-(sizeArea.y - concentration) / 2, (sizeArea.y - concentration) / 2);
		float ranZ = Random.Range(-(sizeArea.z - concentration) / 2, (sizeArea.z - concentration) / 2);

		Vector3 positionRelative = new Vector3(ranX, ranY, ranZ);
		cube.transform.position = GameZone.transform.position+ positionRelative + velocity * 50;
	}

	public void spawnAleatoryCube()
	{
		int ranCube = Random.Range(0, prefab_cube.Length - 1);

		float ranX = Random.Range(-(sizeArea.x - concentration) / 2, (sizeArea.x - concentration) / 2);
		float ranY = Random.Range(-(sizeArea.y - concentration) / 2, (sizeArea.y - concentration) / 2);
		float ranZ = Random.Range(-(sizeArea.z - concentration) / 2, (sizeArea.z - concentration) / 2);

		float ranSpeedX = 0f;
		float ranSpeedY = 0f;
		float ranSpeedZ = 0f;

		float ranTorqueX = 0f;
		float ranTorqueY = 0f;
		float ranTorqueZ = 0f;

		float ranSize = 1f;
		//Velocity
		if (Random.value < 0.5)
		{
			ranSpeedX = Random.Range(0, max_speed_cube);
			ranSpeedY = Random.Range(0, max_speed_cube);
			ranSpeedZ = Random.Range(0, max_speed_cube);
		}
		//Rotation
		if (Random.value < 0.5)
		{
			ranTorqueX = Random.Range(0, max_torque_cube);
			ranTorqueY = Random.Range(0, max_torque_cube);
			ranTorqueZ = Random.Range(0, max_torque_cube);
		}
		//Size
		if (Random.value < 0.5)
		{
			ranSize = Random.Range(1, max_size_cube);

		}


		Vector3 positionRelative = new Vector3(ranX, ranY, ranZ);
		GameObject cube = Instantiate(prefab_cube[ranCube], positionRelative, Quaternion.identity, this.transform);
		Vector3 StartSize = new Vector3(1, 1, 1) * ranSize;
		Vector3 StartSpeed = new Vector3(ranSpeedX, ranSpeedY, ranSpeedZ);
		Vector3 StartRotate = new Vector3(ranTorqueX, ranTorqueY, ranTorqueZ);
		
		Light light= cube.transform.Find("Directional light").GetComponent<Light>();
		light.range = light.range * ranSize/4;
		//Debug.Log(light.name);
		Rigidbody rb = cube.GetComponent<Rigidbody>();
		cube.transform.localScale = StartSize;
		rb.mass = StartSize.x;
		rb.AddForce(StartSpeed, ForceMode.VelocityChange);
		rb.AddTorque(StartRotate, ForceMode.VelocityChange);


	}

	new void Awake()
	{
		base.Awake();
		InitializeMap();
		
	}
	void Start()
	{
		HUDManager.Instance.setUi();
		HUDManager.Instance.SetBlack();
	}

	void Update () {
		if (this.transform.childCount < number_cube_max+1)
		{
			spawnAleatoryCube();
		}
	}
}

