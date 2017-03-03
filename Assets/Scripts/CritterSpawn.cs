using UnityEngine;
using System.Collections;

public class CritterSpawn : MonoBehaviour {

	public Transform[] SpawnPoints; 
	public float spawnTime = 1.5f; 

	//public GameObject Critters; 

	public GameObject[] Critters; 

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn() {
		int spawnIndex = Random.Range (0, SpawnPoints.Length); 
		int critterIndex = Random.Range (0, Critters.Length);
		Instantiate (Critters[critterIndex], SpawnPoints [spawnIndex].position, SpawnPoints [spawnIndex].rotation);

		//set the index number of the array randomly 

	}
}
