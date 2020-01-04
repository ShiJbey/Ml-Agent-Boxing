using UnityEngine;
using UnityEngine.UI;

namespace MLBoxing
{
    public class RoundStats : MonoBehaviour
    {

        public Text timerText;
        public Text roundText;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // Converts a number of seconds to "0:00" format 
        string SecondsToTimeString(int seconds)
        {
            int numMinutes = seconds / 60;
            int numSeconds = seconds % 60;

            string minString = numMinutes.ToString();
            string secondString = (numSeconds / 10 == 0) ? "0" + numSeconds.ToString() : numSeconds.ToString();
            return minString + ":" + secondString;
        }

        // Set the text for the round timer
        public void SetRoundTime(int timeLeft)
        {
            timerText.text = SecondsToTimeString(timeLeft);
        }

        // Set the round counter
        public void SetRoundNumber(int round)
        {
            roundText.text = "Round: " + round.ToString();
        }
    }
}

