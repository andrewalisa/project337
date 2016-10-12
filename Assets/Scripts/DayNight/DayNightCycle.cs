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

	private Int32 secondsInDay = 86400;
	private UInt16 secondsInHalfDay = 43200;
	private Int16 secondsInQuarterDay = 21600;

	
	// Update is called once per frame
	void Update () {
		changeTime ();
	}

	public void changeTime() {
		time += Time.deltaTime * speed;
		if (time > secondsInDay) {
			days += 1;
			time = 0;
		}
		currentTime = TimeSpan.FromSeconds (time);
		string[] tempTime = currentTime.ToString ().Split (":"[0]);
		timeText.text = tempTime [0] + ":" + tempTime [1];

		sunTransform.rotation = Quaternion.Euler (new Vector3((time - secondsInQuarterDay)/secondsInDay * 360, 0, 0));
		intensity = (time < secondsInHalfDay) ? 1 - (secondsInHalfDay - time)/secondsInHalfDay : 1 - (secondsInHalfDay - time)/secondsInHalfDay * -1;
		RenderSettings.fogColor = Color.Lerp (fogNight, fogDay, Mathf.Pow(intensity, 2));

		Sun.intensity = intensity;

	}
}
