using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour {
	// Player level == 0 means player starts at main menu.
	public int playerLevel;
	private int numCollectibles;
	// This is the total score across all levels.
	private int totalScore;
	// This will be the current score for the level.
	private int currentScore;
	private float timeInLevel;

	void Start() {
		// TODO: Change numCollectibles to read from saved data.
		// TODO: Add saving and loading probably, lol.
		numCollectibles = PlayerPrefs.GetInt("Collectibles");
		timeInLevel = 0.0f;
		timeInLevel = PlayerPrefs.GetFloat ("Timer");

		// TODO: Can load scenes with player prefs using something like this.
		//playerLevel = PlayerPrefs.GetInt ("Level");
		//SceneManager.LoadScene (playerLevel);
	}

	void Update() {
		UpdateTime ();
		// TODO: Don't want to call these every frame, level should update on success
		// Collectibles should update on collision with collectible.
		PlayerPrefs.SetFloat ("Timer", timeInLevel);
		PlayerPrefs.SetInt ("Level", playerLevel);
		PlayerPrefs.SetInt ("Collectibles", numCollectibles);
	}
	// Method used to update the timer in the top left corner of the in game screen.
	private void UpdateTime() {
		timeInLevel += Time.deltaTime;
		Text timerText = GameObject.Find ("Timer").GetComponent<Text>();
		timerText.text = "Timer: " + ((int)timeInLevel).ToString();
	}
}
