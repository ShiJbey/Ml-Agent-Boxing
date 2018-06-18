using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingAgent : Agent
{

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
        state.Add(invertMultX * gameObject.transform.position.x);
        // PosZ
        state.Add(invertMultZ * gameObject.transform.position.z);
        // VelX
        state.Add(invertMultX * gameObject.GetComponent<Rigidbody>().velocity.x);
        // VelZ
        state.Add(invertMultZ * gameObject.GetComponent<Rigidbody>().velocity.z);
        // Are they attacking, blocking, idle
        state.Add(self.actionState);
        // Distance to opponent
        state.Add(Vector3.Distance(transform.position, opponent.gameObject.transform.position));
        state.Add(self.life);
        state.Add(self.strength);
        state.Add(self.defense);
        return state;
    }

    public override void AgentStep(float[] act)
    {
        int action = Mathf.FloorToInt(act[0]);

        // Reset movement multipliers
        float moveForward = 0.0f;
        float moveLeft = 0.0f;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 vectToOpponent = opponent.transform.position - transform.position;
        vectToOpponent.Normalize();

        // Moving forward and backward
        if (action == 0 || action == 1)
        {
            moveForward = (action == 0) ? 1f : -1f;
            //transform.Translate(vectToOpponent * moveForward * Time.deltaTime * self.moveSpeed);
            //gameObject.GetComponent<Rigidbody>().velocity = transform.forward * moveForward * self.moveSpeed;
            gameObject.GetComponent<Rigidbody>().velocity = vectToOpponent * moveForward * self.moveSpeed;
            if (action == 0)
            {
                self.actionState = (int)Boxer.ActionState.ADVANCING;
            }
            else
            {
                self.actionState = (int)Boxer.ActionState.RETREATING;
            }
        }
        // Move left and right
        if (action == 2 || action == 3)
        {
            moveLeft = (action == 2) ? 1f : -1f;
            //gameObject.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * moveLeft * 2;
            gameObject.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(-90, Vector3.up) * vectToOpponent * moveLeft * 3;
            self.actionState = (int)Boxer.ActionState.SIDESTEP;
        }

        // Punching and Blocking
        switch (action)
        {
            case 4:
                self.actionState = (int)Boxer.ActionState.PUNCH_LEFT;
                self.GetComponent<Animator>().SetBool("Blocking", false);
                self.GetComponent<Animator>().SetTrigger("LeftPunch");
                break;
            case 5:
                self.actionState = (int)Boxer.ActionState.BLOCK;
                self.GetComponent<Animator>().SetBool("Blocking", true);
                self.GetComponent<Animator>().SetTrigger("Block");
                break;
            case 6:

                self.actionState = (int)Boxer.ActionState.PUNCH_RIGHT;
                self.GetComponent<Animator>().SetBool("Blocking", false);
                self.GetComponent<Animator>().SetTrigger("RightPunch");
                break;
            default:
                self.actionState = (int)Boxer.ActionState.IDLE;
                break;
        }

        self.GetComponent<Animator>().SetInteger("ActionState", self.actionState);


        // Modify rewards

        // Advancing on opponent
        float distanceToOpponent = Vector3.Distance(transform.position, opponent.gameObject.transform.position);
        if (moveForward > 0 && distanceToOpponent > .75)
        {
            this.reward = 0.1f;
        }
        // Punching outside of range
        if ((action == 4 || action == 5) && distanceToOpponent > 1)
        {
            this.reward = -0.05f;
        }
        else if ((action == 4 || action == 5) && distanceToOpponent <= 1)
        {
            this.reward = 0.05f;
        }
        // Punish for standing still
        if (action == 7)
        {
            this.reward = -0.05f;
        }
    }

    public override void AgentReset()
    {
        self = GetComponent<Boxer>();
        opponent = self.opponent;
        self.Reset();

        invertMultX = (invertX) ? -1f : 1f;
        invertMultZ = (invertZ) ? -1f : 1f;

        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }

    public override void AgentOnDone()
    {

    }
}
