using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float health = 150f;

	public GameObject laserPrefab;

	public float laserSpeed = 10f;

	public float shotsPerSecond = 0.5f;

	public int scoreValue = 150;

	public GameObject explosionPrefab;

	private ScoreKeeper scoreKeeper;

	void Start() {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Laser laser = collider.gameObject.GetComponent<Laser>();
		if (laser != null) {
			Hit(laser);	
		}
	}

	void Fire() {
		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);	
		laser.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserSpeed;
	}


	void Hit(Laser laser) {
		health -= laser.damage;
		if (health <= 0) {
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
			scoreKeeper.Score(scoreValue);
		}
		Destroy(laser.gameObject);
	}

	void Update() {
		float shotProbab = Time.deltaTime * shotsPerSecond;
		if (Random.value < shotProbab) {
			Fire();
		}	
	}
}
