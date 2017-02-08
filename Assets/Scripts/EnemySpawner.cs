using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;

	public float width = 10f;
	public float height = 5f;
	public float padding;

	private bool goLeft = true;
	public float speed;

	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

		xmin = leftMost.x;
		xmax = rightMost.x;

		foreach(Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
			enemy.transform.parent = child;	
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}

	// Update is called once per frame
	void Update () {
		if (goLeft) {
			transform.position += Vector3.left * speed * Time.deltaTime;
			if (transform.position.x - 0.5 * width <= xmin) {
				goLeft = false;
			} 
		} else {
			transform.position += Vector3.right * speed * Time.deltaTime;
			if (transform.position.x + 0.5 * width >= xmax) {
				goLeft = true;
			}
		}
	}
}
