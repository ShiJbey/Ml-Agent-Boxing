using UnityEngine;
using TMPro;

public class EndMenu : MonoBehaviour {

    public Transform boxingMatch;
    public Transform academy;
    public Camera orbitingCamera;
    public Camera matchCamera;

    public TextMeshProUGUI winnerText;

    public void SetWinnerText(string text)
    {
        winnerText.text = text;
    }

    public void PlayAgain()
    {
        Brain aiBrain = academy.GetChild(0).GetComponent<Brain>();
        BoxingAgent boxerA = boxingMatch.transform.GetChild(0).GetComponent<BoxingAgent>();
        boxerA.GiveBrain(aiBrain);
        boxerA.enabled = true;
        BoxingAgent boxerB = boxingMatch.transform.GetChild(1).GetComponent<BoxingAgent>();
        boxerB.GiveBrain(aiBrain);
        boxerB.enabled = true;
        ChangeCameras();
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    void ChangeCameras()
    {
        // Swaps out the orbiting camera for the match camera
        orbitingCamera.enabled = true;
        matchCamera.enabled = false;
    }
}
