using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour {

	public float time;
	public TimeSpan currentTime;
	public Transform sunTransform;
	public Light Sun;
	public Text timeText;
	public int days;

	public float intensity;
	public Color fogDay = Color.grey;
	public Color fogNight = Color.black;

	public int speed;

	
	// Update is called once per frame
	void Update () {
		changeTime ();
	}

	public void changeTime() {
		time += Time.deltaTime * speed;
		if (time > 864000) {
			days += 1;
			time = 0;
		}
	}
}
