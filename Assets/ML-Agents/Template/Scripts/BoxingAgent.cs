using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingAgent : Agent {

	[Header("Specific to Boxing")]
	// Movement inversion
	public bool invertX;
	public bool invertZ;
	public float invertMultX;
	public float invertMultZ;
	// Private reference to its own Boxer script
	Boxer self;
	Boxer opponent;

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
		// Multiply the normalized vector by this value to
		// move forward and backward
		float moveForward = 0.0f;
		float moveLeft = 0.0f;
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;

		// Moving forward and backward
		if (action == 0 || action == 1) {
			self.actionState = (int)Boxer.ActionState.MOVING;
			moveForward = (action == 0) ? 1f : -1f;
			gameObject.GetComponent<Rigidbody> ().velocity = transform.forward * moveForward * self.moveSpeed;
		}
		// Move left and right
		if (action == 2 || action == 3) {
			self.actionState = (int)Boxer.ActionState.MOVING;
			moveLeft = (action == 2) ? 1f : -1f;
			gameObject.GetComponent<Rigidbody> ().velocity = Quaternion.AngleAxis (-90, Vector3.up) * transform.forward * moveLeft * 2;
		}

			
		switch (action)
		{
		case 4:
			// Left high punch
			self.actionState = (int)Boxer.ActionState.PUNCH_HIGH_L;
			self.GetComponent<Animator>().SetTrigger("LeftHighPunch");
			break;
		case 5:
			// high block
			self.actionState = (int)Boxer.ActionState.BLOCK_HIGH;
			self.GetComponent<Animator>().SetTrigger("HighBlock");
			break;
		case 6:
			self.actionState = (int)Boxer.ActionState.PUNCH_HIGH_R;
			self.GetComponent<Animator>().SetTrigger("RightHighPunch");
			break;
		case 7:
			self.actionState = (int)Boxer.ActionState.PUNCH_L;
			self.GetComponent<Animator>().SetTrigger("LeftPunch");
			break;
		case 8:
			self.actionState = (int)Boxer.ActionState.BLOCK;
			self.GetComponent<Animator>().SetTrigger("Block");
			break;
		case 9:
			self.actionState = (int)Boxer.ActionState.PUNCH_R;
			self.GetComponent<Animator>().SetTrigger("RightPunch");
			break;
		default:
			self.actionState = (int)Boxer.ActionState.IDLE;
			break;
		}
	}

	public override void AgentReset()
	{
		self = GetComponent<Boxer> ();
		opponent = self.opponent;

		invertMultX = (invertX) ? -1f : 1f;
		invertMultZ = (invertZ) ? -1f : 1f;

		gameObject.transform.position = new Vector3 ( 0, 0.4f, -(invertMultZ) * 2.0f) + transform.parent.transform.position;
		gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
	}

	public override void AgentOnDone()
	{

	}
}
