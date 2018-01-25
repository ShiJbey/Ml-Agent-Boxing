using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxer : MonoBehaviour
{
	// Enumeration of actions the boxer can take
	enum ActionState
	{
		PUNCH_L = 0,
		PUNCH_R,
		PUNCH_HIGH_R,
		PUNCH_HIGH_L,
		BLOCK,
		BLOCK_HIGH,
		IDLE

	};

	// Total amount of damage that can be taken before knockout
	public int life;
	// Damage that can be done to opponents
	public int strength;
	// Damage absorbed when blocking
	public int defense;
	// Time between consecutive punches
	public float reactionTime;
	// How fast boxer can turn towards opponent
	public float turnSpeed;
	// How fast can the boxer move forward, backwards, and laterally
	public float moveSpeed;
	public int actionState;
	// Reference to opponent
	public Boxer opponent;

	// Reference to Animator component
	Animator anim;
	int actionStateHash = Animator.StringToHash ("ActionState");
	GameObject leftGlove;
	GameObject rightGlove;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
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

