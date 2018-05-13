using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

	public Animator animator;
	public Rigidbody2D shotRigidBody;
	public GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		string actualWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().actualWeapon;
		if (actualWeapon == "Pistol") {
			tag = "Shot_1";
		}else if (actualWeapon == "Rifle") {
			tag = "Shot_2";
		}else if (actualWeapon == "Bazooka") {
			tag = "Shot_3";
		}

		animator = GetComponent<Animator>();
		shotRigidBody = GetComponent<Rigidbody2D> ();
		animator.Play ("Shot_1");
		SetAnimation ();
		SetSpeed ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void destroyMySelf() {
		animator.Play ("Explosion");
		Destroy (gameObject, 1);
	}

	public void SetSpeed() {
		bool direction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().facingRight;
		if (direction) {
			shotRigidBody.velocity = new Vector2 (450f, 0);

			//489
		} else {
			transform.localScale = transform.localScale * -1;
			shotRigidBody.velocity = new Vector2 (-450f, 0);
			transform.localPosition = new Vector3 (transform.localPosition.x - 100, transform.localPosition.y, transform.localPosition.z);
			//592
		}
	}

	public void SetAnimation() {
		string actualWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().actualWeapon;
		switch (actualWeapon) {
		case "Pistol":
			animator.Play ("Shot_1");
			transform.localScale = new Vector3 (1, 1, transform.localScale.z);
			break;
		case "Rifle":
			animator.Play ("Shot_2");
			transform.localScale = new Vector3 (2, 2, transform.localScale.z);
			break;
		case "Bazooka":
			animator.Play ("Shot_3");
			transform.localScale = new Vector3 (3, 3, transform.localScale.z);
				break;
		}
	}

}
