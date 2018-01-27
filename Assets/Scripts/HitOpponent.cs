using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOpponent : MonoBehaviour {

	public GameObject boxingArea;


	// Use this for initialization
	void Start () {
		
	}
	
	void OnCollisionEnter(Collision collision)
	{
        BoxingMatch area = boxingArea.GetComponent<BoxingMatch>();
        BoxingAgent agentA = area.AgentA.GetComponent<BoxingAgent>();
        BoxingAgent agentB = area.AgentB.GetComponent<BoxingAgent>();

		if (collision.gameObject.tag == "AgentA") {
            if(collision.gameObject.GetComponent<Boxer>().actionState != (int)Boxer.ActionState.BLOCK)
            {
                // Intentional punch made contact with other boxer
                if (this.gameObject.GetComponent<Boxer>().actionState == (int)Boxer.ActionState.BLOCK)
                {
                    // This punch has been blocked
                    // Slightly punish Agent A
                    agentA.reward = -0.05f;
                    // Reward Agent B
                    agentB.reward = 0.1f;
                    
                }
                else
                {
                    Debug.Log(collision.gameObject.name + " landed a punch on " + this.gameObject.name);
                    // Punch was not blocked
                    // Reward A
                    agentA.reward = 0.1f;
                    // Punish B
                    agentB.reward = -0.1f;
                }
            }
		} else if (collision.gameObject.tag == "AgentB")
        {
            if (collision.gameObject.GetComponent<Boxer>().actionState != (int)Boxer.ActionState.BLOCK)
            {
                // Intentional punch made contact with other boxer
                if (this.gameObject.GetComponent<Boxer>().actionState == (int)Boxer.ActionState.BLOCK)
                {
                    // This punch has been blocked
                    // Slightly punish Agent B
                    agentB.reward = -0.05f;
                    // Reward Agent A
                    agentA.reward = 0.1f;

                }
                else
                {
                    Debug.Log(collision.gameObject.name + " landed a punch on " + this.gameObject.name);
                    // Punch was not blocked
                    // Reward B
                    agentB.reward = 0.1f;
                    // Punish A
                    agentA.reward = -0.1f;
                }
            }
        }
	}
}
