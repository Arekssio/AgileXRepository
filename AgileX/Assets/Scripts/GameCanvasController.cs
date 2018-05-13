using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameCanvasController : MonoBehaviour {

	public float parallaxSpeed = 0.04f;
	public RawImage background;
	public RawImage platform;
	public GameObject startText;
	public GameObject startMenu;
	public GameObject gameOver;
	public GameObject levelCompleted;
	public GameObject enemy;

	public enum GameState {Idle, Playing, Ended, LevelFinished, Shooting};
	public GameState gameState = GameState.Idle;

	public static int currentLevel = 1;

	public GameObject player;
	public GameObject enemyGenerator;
	public GameObject spikedBall;
	public GameObject coinGenerator;
	public Text score;
	public Text timeLeft;
	public float targetTimer = 60.0f;
	public GameObject energyBar;
	public GameObject energy;
	public int shotAvailable = 0;

	public enum CharState {Ground, Air};
	public CharState charState;

	// Use this for initialization
	void Start () {
		gameOver.SetActive (false);
		player.SendMessage ("UpdateState", "Player_Idle");
		energyBar.SendMessage ("TakeEnergy", -0.9);
	}

	// Update is called once per frame
	void Update () {
		updateCharState();
		if (gameState == GameState.Playing) {
			DecreaseTime ();
		}
		if (Input.GetKeyDown ("[1]")) {
			player.SendMessage ("ChangeWeapon", 1);
		}
			else if (Input.GetKeyDown ("[2]")) {
			player.SendMessage ("ChangeWeapon", 2);
		}
		else if(Input.GetKeyDown ("[3]")) {
			player.SendMessage ("ChangeWeapon", 3);
		}
		//Empieza el juego
		if (gameState == GameState.Idle && Input.GetKeyDown("up")) {
			gameState = GameState.Playing;
			startText.SetActive (false);
			startMenu.SetActive (false);
			levelCompleted.SetActive (false);
			charState = CharState.Ground;
			enemyGenerator.SendMessage ("StartGenerator");
			spikedBall.SendMessage ("StartCreating");
			coinGenerator.SendMessage ("StartGenerator");
		}
		if (shotAvailable > 0) {
			shotAvailable -= 1;
		}

		if (Input.GetKey("space") && shotAvailable == 0 && gameState != GameState.Ended) {
			string actualWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().actualWeapon;
			if (actualWeapon == "Pistol") {
				shotAvailable = 80;
			} else if (actualWeapon == "Rifle") {
				shotAvailable = 40;
			}
			else if (actualWeapon == "Bazooka") {
				shotAvailable = 150;
			}
			gameState = GameState.Shooting;
			player.SendMessage ("UpdateState", "Player_Shoots");
			player.SendMessage ("Shoot");

		}
		else if (Input.GetKeyUp("space") && gameState != GameState.Ended) {
			gameState = GameState.Playing;
			//player.SendMessage ("UpdateState", "Player_Idle");
		}
		//Saltar
		if (gameState == GameState.Playing && Input.GetKey("up") && charState == CharState.Ground) {
			charState = CharState.Air;
			player.SendMessage ("UpdateState", "Player_Jump");
			player.SendMessage("Jump");
			energyBar.SendMessage ("TakeEnergy", 0.9);
		}
		//Mover personaje a la izquierda
		if (gameState == GameState.Playing && Input.GetKey ("left")) {
			if (player.transform.position.x <= 0f) {
				//Debug.Log (enemy.GetComponent<EnemyController>().transform.position + "    -    "  + enemy.GetComponent<EnemyController>().velocity);
				float finalSpeed = parallaxSpeed * Time.deltaTime;
				background.uvRect = new Rect (background.uvRect.x - finalSpeed, 0f, 1f, 1f);
				platform.uvRect = new Rect (platform.uvRect.x - finalSpeed * 4, 0f, 1f, 1f);
				player.transform.position = new Vector3 (Math.Max ((player.transform.position.x - 6f), 0f), player.transform.position.y, 0);
			}
			if (charState == CharState.Ground) {
				player.SendMessage ("UpdateState", "Player_Run");
				energyBar.SendMessage ("TakeEnergy", 0.90);
			}
			player.SendMessage ("UpdateSpriteFlip", "left");
			player.transform.position = new Vector3 (Math.Max ((player.transform.position.x - 4f), 0f), player.transform.position.y, 0);
		}
		//Mover personaje a la derecha
		else if (gameState == GameState.Playing && Input.GetKey ("right")) {
			if (player.transform.position.x >= 1050f) {
				float finalSpeed = parallaxSpeed * Time.deltaTime;
				background.uvRect = new Rect (background.uvRect.x + finalSpeed, 0f, 1f, 1f);
				platform.uvRect = new Rect (platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
				player.transform.position = new Vector3 (Math.Max ((player.transform.position.x + 6f), 0f), player.transform.position.y, 0);
			}
			if (charState == CharState.Ground) {
				player.SendMessage ("UpdateState", "Player_Run");
				energyBar.SendMessage ("TakeEnergy", 0.9);
			}
			player.SendMessage ("UpdateSpriteFlip", "right");
			player.transform.position = new Vector3 (Math.Min ((player.transform.position.x + 4f), 1050f), player.transform.position.y, 0);
		}

		//Pararse
		else if (gameState == GameState.Playing && (Input.GetKeyUp ("left") || Input.GetKeyUp ("right"))) {
			player.SendMessage ("UpdateState", "Player_Idle");
			energyBar.SendMessage ("TakeEnergy", -0.9);

		//Cuando muere el prota
		} else if (gameState == GameState.Ended) {
			gameOver.SetActive (true);
			if (Input.GetKeyDown("up")) {
				gameOver.SetActive (false);
				RestartGame();
			}
		}
		//SII completas los puntos requeridos...
		else if (gameState == GameState.LevelFinished) {
			player.SendMessage ("UpdateState", "Player_Idle");
			energyBar.SendMessage ("TakeEnergy", -0.9);
			levelCompleted.SetActive (true);
			startMenu.SetActive (true);
			enemyGenerator.SendMessage ("CancelGenerator");
			spikedBall.SendMessage ("CancelCreating");
			coinGenerator.SendMessage ("CancelGenerator");
			foreach(GameObject enmy in GameObject.FindGameObjectsWithTag("Enemy")) {
				enmy.SendMessage ("ChangeMode");
			}
			if (Input.GetKeyDown ("up")) {
				startMenu.SetActive (false);
				LoadNextLevel ();
				gameState = GameState.Idle;
			}
		}
	}
	//Actualiza el estado espcial del personaje
	public void updateCharState() {
		if (charState == CharState.Air && player.transform.position.y <= 0) {
			charState = CharState.Ground;
		}
	}
	public void PlatformReached() {
		charState = CharState.Ground;
	}
	//Metodo para volver a reiniciar el juego
	public void RestartGame() {
		SceneManager.LoadScene ("Principal");
	}
	public void LoadNextLevel() {
		SceneManager.LoadScene ("level" + (currentLevel + 1).ToString());
		currentLevel++;
		targetTimer = 60.0f;
		score.text = 0.ToString();
		levelCompleted.SetActive (false);

	}
	//Incrementa la puntuación de la moneda de oro
	public void IncreaseScoreOro() {
		int points = int.Parse(score.text) + 15;
		score.text = points.ToString();
		if (points >= 150) {
			gameState = GameState.LevelFinished;
		}

	}
	//Incrementa la puntuación de la moneda de plata
	public void IncreaseScorePlata() {
		int points = int.Parse(score.text) + 10;
		score.text = points.ToString();
		if (points >= 150) {
			gameState = GameState.LevelFinished;
		}

	}
	//Incrementa la puntuación de la moneda de bronce
	public void IncreaseScoreBronce() {
		int points = int.Parse(score.text) + 5;
		score.text = points.ToString();
		if (points >= 150) {
			gameState = GameState.LevelFinished;
		}

	}

	//Actualiza el tiempo restante
	public void DecreaseTime() {
		targetTimer = (targetTimer - Time.deltaTime);
		if (targetTimer <= 0) {
			gameOver.SetActive (true);
			if (Input.GetKeyDown ("up")) {
				gameOver.SetActive (false);
				RestartGame ();
			}
		} else {
			timeLeft.text = ((int)targetTimer).ToString ();
		}
	}

}
