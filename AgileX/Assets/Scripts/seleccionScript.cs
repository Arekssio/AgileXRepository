using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class seleccionScript : MonoBehaviour {

	Vector3 rotacion;
	Vector3 angulo;
	int seleccionActual;
	int totalPersonajes = 3;
	public GameObject personaje1;
	public GameObject personaje2;
	public GameObject personaje3;

	//SPRITES DE LOS PERSONAJES
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public string seleccionado;
	// Use this for initialization

	void Start () {
		seleccionActual = 2;
		personaje1.GetComponent<GameObject>();
		personaje2.GetComponent<GameObject>();
		personaje3.GetComponent<GameObject>();
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.RightArrow) && seleccionActual < totalPersonajes) {
			angulo = transform.eulerAngles;
			rotacion = rotacion + new Vector3 (0, 90, 0);
			seleccionActual++;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow) && seleccionActual > 1) {
			angulo = transform.eulerAngles;
			rotacion = rotacion - new Vector3 (0, 90, 0);
			seleccionActual--;
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			SceneManager.LoadScene ("Principal");
		}

		if (seleccionActual == 2) {
			personaje1.transform.eulerAngles = new Vector3 (0, 0, 0);
			personaje3.transform.eulerAngles = new Vector3 (0, 0, 0);
			seleccionado = "personaje2";
		} else if (seleccionActual == 1) {
			personaje2.transform.eulerAngles = new Vector3 (0, 90, 0);
			personaje3.transform.eulerAngles = new Vector3 (0, 90, 0);
			personaje1.transform.eulerAngles = new Vector3 (0, 0, 0);
			seleccionado = "personaje1";
		}
		else if (seleccionActual == 3) {
			personaje2.transform.eulerAngles = new Vector3 (0, 90, 0);
			personaje3.transform.eulerAngles = new Vector3 (0, 0, 0);
			personaje1.transform.eulerAngles = new Vector3 (0, 90, 0);
			seleccionado = "personaje3";
		}
		angulo = new Vector3 (0, Mathf.LerpAngle(angulo.y, rotacion.y, 8 * Time.deltaTime), 0);
		transform.eulerAngles = angulo;
	}
}
