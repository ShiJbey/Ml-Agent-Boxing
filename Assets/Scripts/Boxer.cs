using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxer : MonoBehaviour
{
	// Enumeration of actions the boxer can take
	public enum ActionState
	{
		PUNCH_RIGHT,
		PUNCH_LEFT,
		BLOCK,
        ADVANCING,
        RETREATING,
        SIDESTEP,
		IDLE
	};

	// Total amount of damage that can be taken before knockout
	public float life = 10f;
    public float currentLife;
	// Damage that can be done to opponents
	public float strength = 2f;
	// Damage absorbed when blocking
	public float defense = 0.5f;
	// How fast boxer can turn towards opponent
	public float turnSpeed = 10f;
	// How fast can the boxer move forward, backwards, and laterally
	public float moveSpeed = 1f;
	// What action is the boxer currently performing
	public int actionState = (int)ActionState.IDLE;
	// Reference to opponent
	public Boxer opponent = null;

	// Use this for initialization
	void Start ()
	{
        ResetLife();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (opponent != null)
        {
            TurnTowardsOpponent();
        }
	}
		
	void TurnTowardsOpponent()
	{
		Vector3 opponentDir = opponent.gameObject.transform.position - transform.position;
		float step = turnSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, opponentDir, step, 0.0f);
		Debug.DrawRay (transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation (newDir);
	}

    public void ResetLife()
    {
        this.currentLife = life;
    }
}

