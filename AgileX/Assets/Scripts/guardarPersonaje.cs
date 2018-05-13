using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardarPersonaje : MonoBehaviour {

	//GameObject objeto;

	public string personaje;
	GameObject buscar;

	void Awake () {
	}
	// Use this for initialization
	void Start () {
		buscar = GameObject.Find ("seleccion");
		personaje = buscar.GetComponent<seleccionScript> ().seleccionado;
	}
	
	// Update is called once per frame
	void Update () {
		DontDestroyOnLoad (gameObject);
		personaje = buscar.GetComponent<seleccionScript> ().seleccionado;
	}
}
