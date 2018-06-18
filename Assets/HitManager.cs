using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        string boxerTag = this.gameObject.tag;
        string otherObjectTag = other.gameObject.tag;
        //Debug.Log(boxerTag + " collided with " + otherObjectTag);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string boxerTag = this.gameObject.tag;
        string otherObjectTag = collision.gameObject.tag;
        if (otherObjectTag.CompareTo("AgentB") == 0 &&
            boxerTag.CompareTo("AgentA") == 0)
        {
            if (collision.gameObject.GetComponent<Boxer>().actionState != (int)Boxer.ActionState.BLOCK &&
                !collision.gameObject.GetComponent<Animator>().GetBool("Block") &&
                (this.gameObject.GetComponent<Boxer>().actionState == (int)Boxer.ActionState.PUNCH_RIGHT ||
                this.gameObject.GetComponent<Boxer>().actionState == (int)Boxer.ActionState.PUNCH_LEFT))
            {
                Debug.Log(boxerTag + " Punched " + boxerTag);
            }
        }
           /*
            else if (collision.gameObject.GetComponent<Boxer>().actionState == (int)Boxer.ActionState.PUNCH_LEFT ||
                collision.gameObject.GetComponent<Boxer>().actionState == (int)Boxer.ActionState.PUNCH_RIGHT)
            {
                // Punch Landed
                if (this.gameObject.GetComponent<Boxer>().actionState != (int)Boxer.ActionState.BLOCK)
                {
                    Debug.Log(otherObjectTag + " Punched " + boxerTag);
                }
            }
        }
        else if (otherObjectTag.CompareTo("AgentA") == 0 &&
            boxerTag.CompareTo("AgentB") == 0)
        {
            Debug.Log(boxerTag + " collided with " + otherObjectTag);
        }
        */
    }
}
