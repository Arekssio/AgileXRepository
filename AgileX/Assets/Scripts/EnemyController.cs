using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float velocity = -150f;
	public Rigidbody2D rb2d;
	public GameObject player;
	public GameObject body;
	public bool working;
	private Animator animator;
	private float health = 5.0f;

	// Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        velocity = velocity - 150f;
        rb2d.velocity = new Vector2(velocity, 0f);
		player = GameObject.FindGameObjectWithTag ("Player");
		working = true;
		animator = GetComponent<Animator> ();
    }
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			animator.Play ("Enemy_Die");
			Destroy (gameObject, 2);
			rb2d.velocity = new Vector2 (0, 0f);
			rb2d.simulated = false;
		} else {
			if (transform.position.x + 300 < player.transform.position.x - 545.4775 && working) {
				rb2d.velocity = new Vector2 (150f, 0f);
				body.transform.localScale = new Vector3 (-28, body.transform.localScale.y, body.transform.localScale.z);
			} else if (transform.position.x + 200 > player.transform.position.x - 445.4775 && working) {
				rb2d.velocity = new Vector2 (-150f, 0f);
				body.transform.localScale = new Vector3 (28, body.transform.localScale.y, body.transform.localScale.z);

			}
		}

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
		}else if (other.gameObject.tag == "Shot_1") {
			health -= 1f;
			other.SendMessage ("destroyMySelf");
		}else if (other.gameObject.tag == "Shot_2") {
			health -= 0.5f;
			other.SendMessage ("destroyMySelf");
		}else if (other.gameObject.tag == "Shot_3") {
			health -= 5f;
			other.SendMessage ("destroyMySelf");
		}
        
    }
	public void ChangeMode() {
		working = false;
		rb2d.velocity = new Vector2(0f, 0f);
		animator.Play ("Enemy_Die");
	}



}
