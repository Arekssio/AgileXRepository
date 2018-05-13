using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Animator animator;
	private SpriteRenderer spriteRenderer;
	public bool facingRight = true;
	public GameObject body;
	private Rigidbody2D rigidBody;
	private float verticalVelocity;

	public GameObject game;
	public GameObject enemyGenerator;
	public GameObject spikedBallGenerator;
	public GameObject coinGenerator;
	public GameObject shotGenerator;
	public GameObject currentGunSprite;

	GameObject objetoSeleccion;
	public string personajeSeleccionado;
	public string actualWeapon;


	// Use this for initialization
	void Start () {
		actualWeapon = "Pistol";
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
	public void ChangeWeapon(int weapon) {
		if (weapon == 1) {
			
			actualWeapon = "Pistol";
			currentGunSprite.GetComponent<Animator> ().Play ("currentGunPistol");
		} else if (weapon == 2) {
			actualWeapon = "Rifle";
		currentGunSprite.GetComponent<Animator> ().Play ("currentGunRifle");
		} else if (weapon == 3) {
			actualWeapon = "Bazooka";
			currentGunSprite.GetComponent<Animator> ().Play ("currentGunBazooka");
		}
	}

	public void UpdateState(string state) {
		if (state != null) {
			if(state == "Player_Shoots") {
				print ("HA ENTRADO");
				switch(actualWeapon) {
				case "Pistol":
					state += "_Pistol";
					break;
				case "Rifle":
					state += "_Rifle";
					break;
				case "Bazooka":
					state += "_Bazooka";
					break;
				}
			}
			print (state);
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

			print (state);
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

	public void Shoot() {
		if (facingRight) {
			shotGenerator.SendMessage ("CreateShot", 450f);
		} else {
			shotGenerator.SendMessage ("CreateShot", -450f);

		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Spike") {
			UpdateState ("Player_Die");
			game.GetComponent<GameCanvasController> ().gameState = GameCanvasController.GameState.Ended;
			enemyGenerator.SendMessage ("CancelGenerator");
			spikedBallGenerator.SendMessage ("CancelCreating");
			coinGenerator.SendMessage ("CancelGenerator");
		} else if (other.gameObject.tag == "Coin") {
			game.SendMessage ("IncreaseScoreOro");
		}
		else if (other.gameObject.tag == "CoinPlata") {
			game.SendMessage ("IncreaseScorePlata");
		}
		else if (other.gameObject.tag == "CoinBronce") {
			game.SendMessage ("IncreaseScoreBronce");
		}
	}
}
