using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private int score = 0;

	public void Score(int points) {
		score += points;
	}

	public void Reset() {
		score = 0;
	}

	void Update() {
		gameObject.GetComponent<Text>().text = score.ToString();
	}
}
