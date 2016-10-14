using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour {

	private int numCollectibles;
	// This is the total score across all levels.
	private int totalScore;
	// This will be the current score for the level.
	private int currentScore;
	private float timeInLevel;

	void Start() {
		// TODO: Change numCollectibles to read from saved data.
		// TODO: Add saving and loading probably, lol.
		numCollectibles = 0;
		timeInLevel = 0.0f;
	}

	void Update() {
		UpdateTime ();
	}
	// Method used to update the timer in the top left corner of the in game screen.
	private void UpdateTime() {
		timeInLevel += Time.deltaTime;
		Text timerText = GameObject.Find ("Timer").GetComponent<Text>();
		timerText.text = "Timer: " + ((int)timeInLevel).ToString();
	}
}
