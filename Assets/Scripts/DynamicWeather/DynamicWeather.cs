using UnityEngine;
using System.Collections;

public class DynamicWeather : MonoBehaviour {

	public WeatherStates weatherState;
	public float weatherDuration = 0f;
	public float resetWeatherDuration = 20f;

	public enum WeatherStates {
		PickWeather,
		Sunny,
		Thunderstorm,
		Mist,
		Overcast,
		Snow
	}

	private int switchWeather;
	private static DynamicWeather dynamicWeather;

	// Use this for initialization
	void Start () {
		dynamicWeather = new DynamicWeather ();
	}
	
	// Update is called once per frame
	void Update () {
		tickTime ();
	}

	private void tickTime() {
		weatherDuration += Time.deltaTime;
		if (weatherDuration >= resetWeatherDuration) {
			weatherDuration = 0f;
			resetWeatherDuration = Random.Range (20f, 61f);
			weatherState = WeatherStates.PickWeather;
		} else {
			return;
		}
	}

	private IEnumerator WeatherFSM() {
		while (true) {					// While finite state machine is active
			switch (weatherState) {
			case WeatherStates.PickWeather:
				PickWeather ();
				break;
			case WeatherStates.Sunny:
				Sunny ();
				break;
			case WeatherStates.Thunderstorm:
				Thunderstorm ();
				break;
			case WeatherStates.Mist:
				Mist ();
				break;
			case WeatherStates.Overcast:
				Overcast ();
				break;
			case WeatherStates.Snow:
				Snow ();
				break;
			}
			yield return null;
		}
	}

	private void PickWeather() {
		switchWeather = Random.Range (0, 5);
		switch (switchWeather) {
		case 1:
			weatherState = WeatherStates.Sunny;
			break;
		case 2:
			weatherState = WeatherStates.Thunderstorm;
			break;
		case 3:
			weatherState = WeatherStates.Mist;
			break;
		case 4:
			weatherState = WeatherStates.Overcast;
			break;
		case 5:
			weatherState = WeatherStates.Snow;
			break;
		}
	}
	private void Sunny() {

	}
	private void Thunderstorm() {

	}
	private void Mist() {

	}
	private void Overcast() {

	}
	private void Snow() {

	}
}
