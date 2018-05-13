using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

	public Rigidbody2D rigidBody;
	private Animator animator;

	// Use this for initialization
	void Start () {

		rigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		transform.position = new Vector3 (Random.Range (-600, 600), -189f, 3.41f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			animator.Play ("Coin_Picked");
			Destroy (gameObject, 0.25f);
		}
	}

}
