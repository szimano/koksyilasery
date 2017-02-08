using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float health = 150f;

	void OnTriggerEnter2D(Collider2D collider) {
		Laser laser = collider.gameObject.GetComponent<Laser>();
		if (laser != null) {
			Hit(laser);	
		}
	}

	void Hit(Laser laser) {
		health -= laser.damage;
		if (health <= 0) {
			Destroy(gameObject);
		}
		Destroy(laser.gameObject);
	}
}
