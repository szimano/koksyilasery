using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float padding = 1f;

	public float laserSpeed = 10;

	public GameObject laserPrefab;

	public float firingRate = 0.2f;

	public float health = 200f;

	public GameObject explosionPrefab;

	public AudioClip looseSound;

	float xmin;
	float xmax;

	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding; 
	}

	void Fire() {
		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
		laser.GetComponent<Rigidbody2D>().velocity = Vector3.up * laserSpeed;
	}
	
	void Update () {
		if (Input.GetKey("right")) {
			gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
		} else if (Input.GetKey("left")) {
			gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate);
		} else if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}

		// stay within gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D collider) {
		health -= 50f;

		if (health <= 0f) {
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			AudioSource.PlayClipAtPoint(looseSound, transform.position);
			LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
			man.LoadLevel("Win Screen");
			Destroy(gameObject);
		}
	}
}
