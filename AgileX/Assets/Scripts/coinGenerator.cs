using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class coinGenerator : MonoBehaviour {

	public GameObject coinOro;
	public GameObject coinPlata;
	public GameObject coinBronce;
	public float generatorTimer = 5f;
	private GameObject coinInstance;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	void SpawnCoin() {
		int numero = UnityEngine.Random.Range (0, 3);
		switch (numero) {
		case 0:
			Instantiate (coinOro, transform.position, Quaternion.identity);
			break;
		case 1: 
			Instantiate (coinPlata, transform.position, Quaternion.identity);
			break;
		case 2: 
			Instantiate (coinBronce, transform.position, Quaternion.identity);
			break;
		}
	}

	public void StartGenerator() {
		InvokeRepeating ("SpawnCoin", 0f, generatorTimer);
	}
	public void CancelGenerator() {
		CancelInvoke ("SpawnCoin");
	}
}
