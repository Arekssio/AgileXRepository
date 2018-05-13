using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour {

	public float velocity = 5;
	public Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		//spikedball.transform.position = new Vector2 (0f, transform.position.y);
		rb2d = GetComponent<Rigidbody2D>();
		velocity = velocity - 350f;
		rb2d.velocity = new Vector2(0f, velocity);
	}
	
	// Update is called once per frame
	void Update () {
		velocity = velocity - 3f;
		rb2d.velocity = new Vector2(0f, velocity);
	}
	/*public void Movimiento() {
		//spikedball.transform.position.y += transform.forward * velocidad * Time.deltaTime;

		spikedball.transform.position = new Vector2 (0f, transform.position.y*velocidad);
		velocidad = velocidad - (float)0.1;
	}*/
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Destroyer")
		{
			Destroy(gameObject);
		}

	}
}
