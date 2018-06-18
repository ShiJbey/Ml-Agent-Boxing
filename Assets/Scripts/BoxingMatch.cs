using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingMatch : MonoBehaviour {

    // References to the two boxers
	public GameObject AgentA;
	public GameObject AgentB;

    // Settings for the rounds
    public int totalRounds = 3;
    public int currentRound = 0;
    public int roundDuration = 30;
    public float timeLeftInRound;
    public bool roundInProgress;

    // References to UI objects
    public RoundStats roundStats;
    public FightCountdown fightCountdown;
    public EndMenu endMenu;

    // Use this for initialization
    void Start () {
        timeLeftInRound = roundDuration;
        roundInProgress = false;
        ResetRoundTime();
        ResetRoundNumber();
	}
	
	// Update is called once per frame
	void Update () {
		if (roundInProgress)
        {
            // Update the time left
            timeLeftInRound -= Time.deltaTime;
            // Update the time counter in the UI
            roundStats.SetRoundTime((int)timeLeftInRound);
            float boxerALife = AgentA.GetComponent<Boxer>().life;
            float boxerBLife = AgentB.GetComponent<Boxer>().life;
            if (boxerALife <= 0 || boxerBLife <= 0)
            {
                Debug.Log("Game should be over");
                // Declare a winner and show the end menu

                if (boxerALife > boxerBLife)
                {
                    endMenu.SetWinnerText("Red Wins!");
                }
                else if (boxerALife < boxerBLife)
                {
                    endMenu.SetWinnerText("Blue Wins!");
                }
                else
                {
                    endMenu.SetWinnerText("Its a tie!");
                }
                endMenu.gameObject.SetActive(true);
                roundStats.gameObject.SetActive(false);
                roundInProgress = false;
            }

            if (timeLeftInRound <= 0)
            {
                roundInProgress = false;
                // End the round
                RoundEnd();
                fightCountdown.Reset();
                
                if (currentRound >= totalRounds)
                {
                    Debug.Log("Game should be over");
                    // Declare a winner and show the end menu
                    
                    if (boxerALife > boxerBLife)
                    {
                        endMenu.SetWinnerText("Red Wins!");
                    }
                    else if (boxerALife < boxerBLife)
                    {
                        endMenu.SetWinnerText("Blue Wins!");
                    }
                    else
                    {
                        endMenu.SetWinnerText("Its a tie!");
                    }
                    endMenu.gameObject.SetActive(true);
                    roundStats.gameObject.SetActive(false);
                }
                else
                {
                    roundStats.gameObject.SetActive(false);
                    fightCountdown.SayRoundOver();
                    fightCountdown.gameObject.SetActive(true);
                }
            }
        }
	}

    // Set the match to begin
    public void RoundStart()
    {
        // Set the time back for full round
        ResetRoundTime();
        // Set the counter for a new round
        currentRound++;
        roundStats.SetRoundNumber(currentRound);
        // Enable the boxers to move, block, and attack
        AgentA.GetComponent<BoxingAgent>().enabled = true;
        AgentB.GetComponent<BoxingAgent>().enabled = true;

        roundInProgress = true;
    }

    public void ResetRoundTime()
    {
        timeLeftInRound = roundDuration;
    }

    public void ResetRoundNumber()
    {
        currentRound = 0;
    }

    public void RoundEnd()
    {
        // Enable the boxers to move, block, and attack
        AgentA.GetComponent<BoxingAgent>().enabled = false;
        AgentB.GetComponent<BoxingAgent>().enabled = false;
        // Reset Boxer positions, but not stats
        AgentA.GetComponent<Boxer>().ResetPositioning();
        AgentB.GetComponent<Boxer>().ResetPositioning();
        // Stops all timers
        roundInProgress = false;
    }

    public void MatchReset()
    {
        // Place agents back to their starting spots
        Boxer boxerA = AgentA.GetComponent<Boxer>();
        boxerA.Reset();
        Boxer boxerB = AgentB.GetComponent<Boxer>();
        boxerB.Reset();
        ResetRoundTime();
        ResetRoundNumber();
    }
}
