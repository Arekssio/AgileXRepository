using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikedBallGenerator : MonoBehaviour {

	public GameObject spikedBalls;
	public float generatorTimer = 5f; //1.75 segundos

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void CreateBalls() {
		Vector3 position = new Vector3(Random.Range(-700.0f, 700.0f), 0, 0);
		Instantiate(spikedBalls, position, Quaternion.identity);
	}
	public void StartCreating()
	{
		InvokeRepeating("CreateBalls", 0f, generatorTimer);
	}

	public void CancelCreating()
	{
		CancelInvoke("CreateBalls");
	}
}
