using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingAgent : Agent {

	[Header("Specific to Boxing")]
	public BoxingAgent opponent;
	public bool invertX;
	public bool invertZ;
	public float invertMultX;
	public float invertMultZ;
	public Boxer self;


	public override List<float> CollectState()
	{
		List<float> state = new List<float>();
		// PosX
		state.Add (invertMultX * gameObject.transform.position.x);
		// PosZ
		state.Add (invertMultZ * gameObject.transform.position.z);
		// VelX
		state.Add (invertMultX * gameObject.GetComponent<Rigidbody>().velocity.x);
		// VelZ
		state.Add (invertMultZ * gameObject.GetComponent<Rigidbody>().velocity.z);
		// Are they attacking, blocking, idle
		state.Add (self.actionState);	
		// Distance to opponent
		state.Add (Vector3.Distance(transform.position, opponent.gameObject.transform.position)); 
		state.Add (self.life);
		state.Add (self.strength);
		state.Add (self.defense);
		return state;
	}

	public override void AgentStep(float[] act)
	{
		int action = Mathf.FloorToInt (act [0]);
		float moveX = 0.0f;
		float moveZ = 0.0f;

		switch (action)
		{
		case 0:
			moveZ = 1f;
			break;
		case 1:
			moveZ = -1f;
			break;
		case 2:
			moveX = 1f;
			break;
		case 3:
			moveX = -1f;
			break;
		case 4:
			break;
		case 5:
			break;
		case 6:
			break;
		case 7:
			break;
		case 8:
			break;
		case 9:
			break;
		case 10:
			break;
			
		}

		gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (moveX * self.moveSpeed, 0, moveZ * self.moveSpeed);

	}

	public override void AgentReset()
	{
		invertMultX = (invertX) ? -1f : 1f;
		invertMultZ = (invertZ) ? -1f : 1f;

		gameObject.transform.position = new Vector3 ( 0, 0.4f, -(invertMultZ) * 2.0f) + transform.parent.transform.position;
		gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
	}

	public override void AgentOnDone()
	{

	}
}
