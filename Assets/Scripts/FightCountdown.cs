using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FightCountdown : MonoBehaviour {

    // Max number of seconds before the start of the round
    public int countdownDuration = 6;
    // Seconds remaining until the start of the round
    private float timeRemaining;
    // Reference to the text object
    public Transform countdownText;
    // Reference to the boxing match
    public BoxingMatch boxingMatch;
    // Reference to the round timer
    public Transform roundStats;

	// Use this for initialization
	void Start () {
        timeRemaining = countdownDuration;
        SetTimerText();
	}
	
	// Update is called once per frame
	void Update () {
        timeRemaining -= Time.deltaTime;
        SetTimerText();
		if (timeRemaining <= 0)
        {
            // Start the Round
            boxingMatch.RoundStart();
            // Turn on the round timer
            roundStats.gameObject.SetActive(true);
            // Turn itself off
            gameObject.SetActive(false);
        }
	}



    // Sets the countdown text
    void SetTimerText()
    {
        int truncatedTime = (int)timeRemaining;
        if (truncatedTime < 1)
        {
            countdownText.GetComponent<TextMeshProUGUI>().text = "Fight!!";
        }
        else
        {
            countdownText.GetComponent<TextMeshProUGUI>().text = ((int)timeRemaining).ToString();
        }
    }

    public void SayRoundOver()
    {
        countdownText.GetComponent<TextMeshProUGUI>().text = "Round over!";
    }

    public void Reset()
    {
        timeRemaining = countdownDuration;
        SetTimerText();
    }
}
