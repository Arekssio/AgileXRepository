using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGeneratorController : MonoBehaviour {

	public GameObject shotPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateShot(float speed) {
		GameObject shot = Instantiate (shotPrefab, transform.position, Quaternion.identity);
	}
}
