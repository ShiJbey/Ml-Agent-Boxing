using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOpponent : MonoBehaviour {

	public GameObject boxingArea;


	// Use this for initialization
	void Start () {
		
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		/*
		 * Cases to check for
		 * a) Agent collides with opponent gloves (if blocking => puncish attacker, reward defender)
		 * b) Agent collides with opponent head (reward attacker)
		 * c) Agent collides with opponent body (reward attacker)
		 */ 

		BoxingMatch area = boxingArea.GetComponent<BoxingMatch> ();
		Boxer agentA = area.AgentA.GetComponent<Boxer> ();
		Boxer agentB = area.AgentB.GetComponent<Boxer> ();
		if (collision.gameObject.tag == "agent") {
			if (collision.gameObject.name == "BoxerA") {

			} else {

			}
		}
	}
}
