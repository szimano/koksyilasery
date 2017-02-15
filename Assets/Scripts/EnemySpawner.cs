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

	public float spawnDelay = 0.5f;

	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

		xmin = leftMost.x;
		xmax = rightMost.x;

		ReSpawnEnemies();
	}

	void ReSpawnEnemies() {
		SpawnUntilFull();
	}

	void SpawnUntilFull() {
		Transform position = NextFreePosition();
		if (position) {
			GameObject enemy = Instantiate(enemyPrefab, position.position, Quaternion.identity);
			enemy.transform.parent = position;	
			Invoke("SpawnUntilFull", spawnDelay);
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

		if (AllMembersDead()) {
			ReSpawnEnemies();
		}
	}

	Transform NextFreePosition() {
		foreach(Transform childPosition in transform) {
			if (childPosition.childCount == 0) {
				return childPosition;
			}
		}

		return null;
	}

	bool AllMembersDead() {
		foreach(Transform childPosition in transform) {
			if (childPosition.childCount > 0) {
				return false;
			}
		}
		return true;
	}
}
