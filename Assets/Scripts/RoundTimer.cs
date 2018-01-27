using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour {

	public int roundTimeSeconds = 120 ;
	float timeLeftSeconds;
	public Text timerText;
	public RoundManager roundManager;

	string SecondsToTimeString(int seconds) {
		int numMinutes = seconds / 60;
		int numSeconds = seconds % 60;

		string minString = numMinutes.ToString ();
		string secondString = (numSeconds / 10 == 0) ? "0" + numSeconds.ToString () : numSeconds.ToString ();
		return minString + ":" + secondString;
	}

	// Use this for initialization
	void Start () {
		timeLeftSeconds = roundTimeSeconds;
		timerText.text = SecondsToTimeString ((int)timeLeftSeconds);
	}
	
	// Update is called once per frame
	void Update () {
		timeLeftSeconds -= Time.deltaTime;
		timerText.text = SecondsToTimeString ((int)timeLeftSeconds);
		if (timeLeftSeconds <= 0) {
			roundManager.RoundOver ();
			Reset ();
		}
	}

	public void Reset() {
		timeLeftSeconds = roundTimeSeconds;
		timerText.text = SecondsToTimeString ((int)timeLeftSeconds);
	}
}
