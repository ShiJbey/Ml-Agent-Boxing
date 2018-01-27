using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxer : MonoBehaviour
{
	// Enumeration of actions the boxer can take
	public enum ActionState
	{
		PUNCH_L = 0,
		PUNCH_R,
		PUNCH_HIGH_R,
		PUNCH_HIGH_L,
		BLOCK,
		BLOCK_HIGH,
		MOVING,
		IDLE
	};

	// Total amount of damage that can be taken before knockout
	public float life = 10f;
	// Damage that can be done to opponents
	public float strength = 2f;
	// Damage absorbed when blocking
	public float defense = 0.5f;
	// How fast boxer can turn towards opponent
	public float turnSpeed = 10f;
	// How fast can the boxer move forward, backwards, and laterally
	public float moveSpeed;
	// What action is the boxer currently performing
	public int actionState;
	// Reference to opponent
	public Boxer opponent;

	GameObject leftGlove;
	GameObject rightGlove;

	// Use this for initialization
	void Start ()
	{
		leftGlove = this.gameObject.transform.GetChild (3).gameObject;
		rightGlove = this.gameObject.transform.GetChild (2).gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		TurnTowardsOpponent ();
	}
		
	void TurnTowardsOpponent()
	{
		Vector3 opponentDir = opponent.gameObject.transform.position - transform.position;
		float step = turnSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, opponentDir, step, 0.0f);
		Debug.DrawRay (transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation (newDir);
	}
}

