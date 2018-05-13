using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Animator animator;
	private SpriteRenderer spriteRenderer;
	private bool facingRight = true;
	public GameObject body;
	private Rigidbody2D rigidBody;
	private float verticalVelocity;

	public GameObject game;
	public GameObject enemyGenerator;
	public GameObject spikedBallGenerator;
	public GameObject fireBallGenerator;
	public GameObject coinGenerator;

	GameObject objetoSeleccion;
	//guardarPersonaje guardarpersonaje;
	public string personajeSeleccionado;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		rigidBody = GetComponent<Rigidbody2D> ();

		objetoSeleccion = GameObject.Find ("seleccion");
		personajeSeleccionado = objetoSeleccion.GetComponent<guardarPersonaje> ().personaje;
		//body.GetComponent<SpriteRenderer> ().sprite = guardarpersonaje.personaje;
	}
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= 0) {
			rigidBody.velocity = new Vector2 (0f, 0f);
			transform.position = new Vector2 (transform.position.x, 0f);
			
		} else {
			verticalVelocity = verticalVelocity - 20f;
			rigidBody.velocity = new Vector2 (0f, verticalVelocity);
		}
	}
	public void UpdateState(string state) {
		if (state != null) {
			switch (personajeSeleccionado) {
			case "personaje1": 
				state += "_1";
				break;
			case "personaje2":
				state += "_2";
				break;
			case "personaje3":
				state += "_3";
				break;
			}
			animator.Play (state);
		}
	}
	public void UpdateSpriteFlip(string direction) {
		if (direction.Equals("right") && !facingRight) {
			facingRight = true;
			Vector3 theScale = body.transform.localScale;
			theScale.x *= -1;
			body.transform.localScale = theScale;
		} else if (direction.Equals("left") && facingRight) {
			facingRight = false;
			Vector3 theScale = body.transform.localScale;
			theScale.x *= -1;
			body.transform.localScale = theScale;
		}
	}
	public void Jump() {
		rigidBody.velocity = new Vector2(0f, 900.0f);
		verticalVelocity = 900.0f;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Spike" || other.gameObject.tag == "Fire") {
			UpdateState ("Player_Die");
			game.GetComponent<GameCanvasController> ().cansado = false;
			game.GetComponent<GameCanvasController> ().gameState = GameCanvasController.GameState.Ended;
			enemyGenerator.SendMessage ("CancelGenerator");
			spikedBallGenerator.SendMessage ("CancelCreating");
			fireBallGenerator.SendMessage ("CancelCreating");
			coinGenerator.SendMessage ("CancelGenerator");
		} else if (other.gameObject.tag == "Coin") {
			game.SendMessage ("IncreaseScoreOro");
		} else if (other.gameObject.tag == "CoinPlata") {
			game.SendMessage ("IncreaseScorePlata");
		} else if (other.gameObject.tag == "CoinBronce") {
			game.SendMessage ("IncreaseScoreBronce");
		} else if (other.gameObject.tag == "hamburguesa") {
			GameObject.Find ("EnergyBar").SendMessage ("TakeEnergy", -50f);
			Destroy (other.gameObject);
		}
	}
}
