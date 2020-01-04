using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLBoxing
{
    /// <summary>
    /// Manages a match between two Boxer agents. This class
    /// specifically manages the state of the round and 
    /// reseting agents at the end of the round.
    /// </summary>
    public class BoxingMatch : TrainingMatch
    {
        // Settings for the rounds
        public int m_TotalRounds = 3;
        public int m_CurrentRound = 0;
        public float m_RoundDuration = 30f;
        private float m_TimeLeftInRound;
        private bool m_RoundInProgress = false;

        // References to UI objects
        public RoundStats m_RoundStatsUI;
        public FightCountdown m_FightCountdownUI;
        public EndMenu m_EndMenu;


        // Use this for initialization
        void Start()
        {
            m_BoxerA.AgentReset();
            m_BoxerB.AgentReset();
            ResetRoundTime();
            ResetRoundNumber();
        }

        // Update is called once per frame
        void Update()
        {
            if (m_RoundInProgress)
            {
                // Update the time left
                m_TimeLeftInRound -= Time.deltaTime;

                // Update the time counter in the UI
                if (m_RoundStatsUI != null)
                    m_RoundStatsUI.SetRoundTime((int)m_TimeLeftInRound);

                // Check for knock out
                if (m_BoxerA.IsKnockedOut() && m_BoxerB.IsKnockedOut())
                {
                    // Tie
                    Debug.Log("Game over. Tie by double knockout.");
                    if (m_EndMenu != null)
                    {
                        m_EndMenu.SetWinnerText("Tie! Double Knockout.");
                        m_EndMenu.gameObject.SetActive(true);
                    }
                    if (m_RoundStatsUI !=  null)
                    {
                        m_RoundStatsUI.gameObject.SetActive(false);
                    }
                    
               
                    m_RoundInProgress = false;
                }
                else if (!m_BoxerA.IsKnockedOut() && m_BoxerB.IsKnockedOut())
                {
                    // Boxer A Win
                    Debug.Log("Game over. Red wins by knockout.");
                    if (m_EndMenu != null)
                    {
                        m_EndMenu.SetWinnerText("Knockout! Red wins!");
                        m_EndMenu.gameObject.SetActive(true);
                    }
                    if (m_RoundStatsUI != null)
                    {
                        m_RoundStatsUI.gameObject.SetActive(false);
                    }
                    m_RoundInProgress = false;
                }
                else if (m_BoxerA.IsKnockedOut() && !m_BoxerB.IsKnockedOut())
                {
                    // Boxer B Win
                    Debug.Log("Game over. Blue wins by knockout.");
                    if (m_EndMenu != null)
                    {
                        m_EndMenu.SetWinnerText("Knockout! Blue Wins!");
                        m_EndMenu.gameObject.SetActive(true);
                    }
                    if (m_RoundStatsUI != null)
                    {
                        m_RoundStatsUI.gameObject.SetActive(false);
                    }
                    m_RoundInProgress = false;
                }

                 // Check if the round is over
                if (m_TimeLeftInRound <= 0)
                {
                    m_RoundInProgress = false;
                    // End the round
                    RoundEnd();
                    if (m_RoundStatsUI != null)
                    {
                        m_FightCountdownUI.Reset();
                    }

                    if (m_CurrentRound >= m_TotalRounds)
                    {
                        if (m_BoxerA.m_Life > m_BoxerB.m_Life)
                        {
                            Debug.Log("Game over. Red wins by points.");
                            if (m_EndMenu != null)
                                m_EndMenu.SetWinnerText("Match Over. Red Wins!");
                        }
                        else if (m_BoxerA.m_Life < m_BoxerB.m_Life)
                        {
                            Debug.Log("Game over. Blue wins by points.");
                            if (m_EndMenu != null)
                                m_EndMenu.SetWinnerText("Match Over. Blue Wins!");
                        }
                        else
                        {
                            Debug.Log("Game over. Tie by points.");
                            if (m_EndMenu != null)
                                m_EndMenu.SetWinnerText("Match Over. Tie!");
                        }
                        if (m_EndMenu != null)
                            m_EndMenu.gameObject.SetActive(true);
                        if (m_RoundStatsUI != null)
                            m_RoundStatsUI.gameObject.SetActive(false);
                    }
                    else
                    {
                        if (m_RoundStatsUI != null)
                            m_RoundStatsUI.gameObject.SetActive(false);
                        if (m_FightCountdownUI != null)
                        {
                            m_FightCountdownUI.SayRoundOver();
                            m_FightCountdownUI.gameObject.SetActive(true);
                        }
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
            m_CurrentRound++;
            if (m_RoundStatsUI)
                m_RoundStatsUI.SetRoundNumber(m_CurrentRound);
            // Enable the boxers to move, block, and attack
            m_BoxerA.enabled = true;
            m_BoxerB.enabled = true;

            m_RoundInProgress = true;
        }

        public void ResetRoundTime()
        {
            m_TimeLeftInRound = m_RoundDuration;
        }

        public void ResetRoundNumber()
        {
            m_CurrentRound = 0;
        }

        public void RoundEnd()
        {
            // Enable the boxers to move, block, and attack
            m_BoxerA.enabled = true;
            m_BoxerB.enabled = true;
            // Reset Boxer positions, but not stats
            m_BoxerA.ReturnToCorner();
            m_BoxerB.ReturnToCorner();
            // Stops all timers
            m_RoundInProgress = false;
        }

        public override void MatchReset()
        {
            // Place agents back to their starting spots
            m_BoxerA.AgentReset();
            m_BoxerB.AgentReset();
            ResetRoundTime();
            ResetRoundNumber();
        }
    }
}
