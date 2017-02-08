using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCatcher : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D collider) {
		Destroy(collider.gameObject);
	}
}
