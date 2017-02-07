using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float padding = 1f;

	float xmin;
	float xmax;

	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding; 
	}
	
	void Update () {
		if (Input.GetKey("right")) {
			gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
		} else if (Input.GetKey("left")) {
			gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
		} 

		// stay within gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
