using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class energyBar : MonoBehaviour {

	public float hp, maxHp = 100f;
	public Image energy;
	public GameObject player;
	private GameObject game;
	//Animator animator;
	//AnimatorStateInfo animacion;
	// Use this for initialization
	void Start () {
		hp = maxHp;
		//animator = new Animator();
		//animacion = animator.GetCurrentAnimatorStateInfo (0);
		game = GameObject.Find("GameCanvas");
		energy.transform.localScale = new Vector3(1, 0.83f, 1);
	}

	public void TakeEnergy(float cantidad) {
		hp = Mathf.Clamp (hp - cantidad, 0f, maxHp);
		energy.transform.localScale = new Vector3(hp / maxHp, 0.83f, 1);
	}

	void Update() {
		if (hp <= 0.02) {
			//player.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints.FreezePositionX;
			game.GetComponent<GameCanvasController>().cansado = true;
			player.SendMessage ("UpdateState", "Player_Idle");
		}
	}
}
