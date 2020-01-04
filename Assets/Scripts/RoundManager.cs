using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MLBoxing
{
	public class RoundManager : MonoBehaviour
	{

		public int numRounds = 3;
		int currentRound;

		// Use this for initialization
		void Start()
		{
			currentRound = 1;
			GetComponent<Text>().text = "Round: " + currentRound.ToString();
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void RoundOver()
		{
			if (currentRound == numRounds)
			{
				// END GAME
			}
			else
			{
				++this.currentRound;
				GetComponent<Text>().text = "Round: " + currentRound.ToString();
			}
		}
	}
}

