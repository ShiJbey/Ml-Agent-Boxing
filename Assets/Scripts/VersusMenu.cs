using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusMenu : MonoBehaviour {

    public Transform boxingMatch;
    public Transform academy;
    public Camera orbitingCamera;
    public Camera matchCamera;

    public void PlayerVsCpu()
    {
        Brain playerBrain = academy.GetChild(1).GetComponent<Brain>();
        Brain aiBrain = academy.GetChild(0).GetComponent<Brain>();
        // Sets one of the boxers to use a player brain
        // while the other uses the trained model
        BoxingAgent boxerA = boxingMatch.transform.GetChild(0).GetComponent<BoxingAgent>();
        boxerA.GiveBrain(playerBrain);
        boxerA.enabled = false;
        BoxingAgent boxerB = boxingMatch.transform.GetChild(1).GetComponent<BoxingAgent>();
        boxerB.GiveBrain(aiBrain);
        boxerB.enabled = false;

        ChangeCameras();

        boxingMatch.GetComponent<BoxingMatch>().MatchReset();
    }

    public void CpuVsCpu()
    {
        Brain aiBrain = academy.GetChild(0).GetComponent<Brain>();
        // (should be default) Set the boxers to both
        // operate using the trained model
        BoxingAgent boxerA = boxingMatch.transform.GetChild(0).GetComponent<BoxingAgent>();
        boxerA.GiveBrain(aiBrain);
        boxerA.enabled = false;
        BoxingAgent boxerB = boxingMatch.transform.GetChild(1).GetComponent<BoxingAgent>();
        boxerB.GiveBrain(aiBrain);
        boxerB.enabled = false;
        

        ChangeCameras();

        boxingMatch.GetComponent<BoxingMatch>().MatchReset();
    }

    public void StartFight()
    {
        // start the countdown script that enables the agents to
        // start moving (enable their agent scripts)
    }

    void ChangeCameras()
    {
        // Swaps out the orbiting camera for the match camera
        orbitingCamera.enabled = false;
        matchCamera.enabled = true;
    }
}
