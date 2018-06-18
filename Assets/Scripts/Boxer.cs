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

	// Maximum amount of damage that can be taken before a knockout
	public float MAX_LIFE = 10f;
    // Current amount of damage that can be taken before knockout
    public float life = 10f;
	// Damage that can be done to opponents
	public float strength = 0.5f;
	// Damage absorbed when blocking
	public float defense = 0.5f;
	// How fast boxer can turn towards opponent
	public float turnSpeed = 10f;
	// How fast can the boxer move forward, backwards, and laterally
	public float moveSpeed = 3f;
	// What action is the boxer currently performing
	public int actionState = (int)ActionState.IDLE;
	// Reference to opponent
	public Boxer opponent = null;
    // Start position of this boxer
    public Transform startPosition;
    // Starting rotation of the boxer
    private Quaternion startRotation;


	// Use this for initialization
	void Start ()
	{
        startRotation = transform.rotation;
        Reset();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (opponent != null)
        {
            transform.LookAt(opponent.transform);
        }
	}

    // Reset the current life points and the boxer
    // and sets the action state back to idle
    public void Reset()
    {
        life = MAX_LIFE;
        actionState = (int)ActionState.IDLE;
        ResetPositioning();
    }

    public void ResetPositioning()
    {
        transform.position = startPosition.position;
        transform.rotation = startRotation;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}

