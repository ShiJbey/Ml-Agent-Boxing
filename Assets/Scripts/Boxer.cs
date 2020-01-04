using MLAgents;
using UnityEngine;

namespace MLBoxing
{
    public class Boxer : Agent
	{
        public enum AgentColor
        {
            RED = 0,
            BLUE
        };

        // Enumeration of actions the boxer can take
        public enum ActionState
        {
            IDLE = 0,
            BLOCK,
            PUNCH_LEFT,
            PUNCH_RIGHT            
        };

        public AgentColor m_Color;
        private string m_ColorTag;

		// Maximum amount of damage that can be taken before a knockout
		public const float MAX_LIFE = 10f;
		// Current amount of damage that can be taken before knockout
		public float m_Life = 10f;
		// Damage that can be done to opponents
		public float m_Strength = 0.5f;
		// Damage absorbed when blocking
		public float m_Defense = 0.5f;
		// How fast can the boxer move forward, backwards, and laterally
		public float m_MoveSpeed = 5f;


		// What action is the boxer currently performing
        [HideInInspector]
		public ActionState m_ActionState = ActionState.IDLE;
		// Reference to opponent
		public Boxer m_Opponent = null;
		// Start position of this boxer
		public Transform m_StartPosition;
		// Is this agent controlled by the player?
		public bool m_PlayerControlled = false;

        public SphereCollider m_HeadCollider;
        public SphereCollider m_LeftHandHitbox;
        public SphereCollider m_RightHandHitbox;

        private Animator m_Anim;
        private Rigidbody m_Rbody; 
        private float[] m_ActionVector;

        public TrainingMatch m_Match;

		public void Start()
		{
            m_ActionState = ActionState.IDLE;
            m_Anim = GetComponent<Animator>();
            m_Rbody = GetComponent<Rigidbody>();

            string[] tags = { "Red", "Blue"  };

            m_ColorTag = tags[(int)m_Color];
        }

        public void FixedUpdate()
        {
            MoveAgent(m_ActionVector);
            AttackDefend(m_ActionVector);

            if (m_ActionState == ActionState.BLOCK)
            {
                Block();
                float animationCompletePerc = m_Anim.GetCurrentAnimatorStateInfo(0).normalizedTime /
                        m_Anim.GetCurrentAnimatorStateInfo(0).length;
                if (animationCompletePerc >= 0.7f)
                {
                    m_HeadCollider.enabled = false;
                }
            }else
            {
                m_HeadCollider.enabled = true;
            }
        }

        public override void InitializeAgent()
		{
			base.InitializeAgent();
			
		}

        public void MoveAgent(float[] vectorAction)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 moveDir = Vector3.zero;
            Vector3 rotateDir = Vector3.zero;

            if (vectorAction[1] > 0)
            {
                AddReward(0.45f);
            }

            moveDir += transform.forward * vectorAction[1];
            moveDir += transform.right * vectorAction[0];

            transform.LookAt(m_Opponent.transform);

            transform.position += moveDir * m_MoveSpeed * Time.deltaTime;
        }

        public void AttackDefend(float[] vectorAction)
        {
            int action = Mathf.FloorToInt(vectorAction[2]);
            if (m_ActionState == ActionState.BLOCK)
            {
                if (action != (int)ActionState.IDLE && action != (int)ActionState.BLOCK)
                {
                    m_ActionState = (ActionState)action;
                    m_Anim.SetInteger("ActionState", action);
                }
            }
            else
            {
                m_ActionState = (ActionState)action;
                m_Anim.SetInteger("ActionState", action);
            }


            switch (action)
            {
                case 1:
                    m_Anim.SetBool("Blocking", true);
                    m_Anim.SetBool("Block", true);
                    break;
                case 2:
                    PunchOpponent(m_LeftHandHitbox);
                    break;
                case 3:
                    PunchOpponent(m_RightHandHitbox);
                    break;
            }
        }

		public override void AgentAction(float[] vectorAction, string textAction)
		{
            m_ActionVector = vectorAction;
            m_ActionVector[2] *= 4f;
            AddReward(-0.01f);
		}

		public override void AgentReset()
		{
            m_ActionState = ActionState.IDLE;
            m_Anim = GetComponent<Animator>();
            m_Rbody = GetComponent<Rigidbody>();

            string[] tags = { "Red", "Blue" };

            m_ColorTag = tags[(int)m_Color];

            m_Life = MAX_LIFE;
			
            m_ActionVector = new float[3];
            m_Anim.SetInteger("ActionState", (int)ActionState.IDLE);

            ReturnToCorner();
        }

		public float[] PlayerControl()
		{
            var action = new float[3];
            action[0] = Input.GetAxis("Horizontal");
            action[1] = Input.GetAxis("Vertical");
            // Only one attack/defensive action may be passed at a time
            if (Input.GetAxis("Block") != 0f)
            {
                action[2] = (float)ActionState.BLOCK;
            }
            else
            {
                if (Input.GetAxis("PunchLeft") != 0f)
                {
                    action[2] = (float)ActionState.PUNCH_LEFT;
                }
                else if (Input.GetAxis("PunchRight") != 0f)
                {
                    action[2] = (float)ActionState.PUNCH_RIGHT;
                }
                else
                {
                    action[2] = (float)ActionState.IDLE;
                }
            }
            return action;
        }

        public float[] IdleCPUControl()
        {
            float[] action = new float[3];
            //action[2] = (float)ActionState.BLOCK;
            return action;

        }

		public override float[] Heuristic()
		{
            // Note: Action vector should have size (3)
			if (m_PlayerControlled)
			{
				return PlayerControl();
			}
            else
            {
                return IdleCPUControl();
            }
		}

		public override void CollectObservations()
		{
            // Note: Observation vector should have size (10).

            // Boxer Position
            AddVectorObs(transform.position);
            // Boxer Stats
            AddVectorObs(m_Life);
            AddVectorObs(m_Strength);
            AddVectorObs(m_Defense);
            // Opponent Position
            AddVectorObs(m_Opponent.transform.position);
            // Opponent Stats (Partially observable)
            AddVectorObs(m_Opponent.m_Life);
		}

		public bool IsKnockedOut()
		{
			return m_Life <= 0;
		}

		public void ReturnToCorner()
		{
			// Reset Positioning
			transform.position = m_StartPosition.position;
			transform.LookAt(m_Opponent.transform);
		}

        private void Block()
        {
            // Check if the opponent hands are punching this boxer
            Collider[] cols = Physics.OverlapSphere(m_RightHandHitbox.bounds.center, m_RightHandHitbox.radius, LayerMask.GetMask(m_Opponent.m_ColorTag));
            foreach (Collider c in cols)
            {
                if (c.transform.parent == transform)
                    continue;
                if (m_Opponent.m_ActionState == ActionState.PUNCH_LEFT || 
                    m_Opponent.m_ActionState == ActionState.PUNCH_RIGHT)
                {
                    Debug.Log(transform.name + " blocked punch.");
                    AddReward(m_Match.m_Academy.m_SuccessfulBlock);

                }
            }
        }

        private void PunchOpponent(SphereCollider col)
        {
            Collider[] cols = Physics.OverlapSphere(col.bounds.center, col.radius, LayerMask.GetMask(m_Opponent.m_ColorTag));

            foreach(Collider c in cols)
            {
                if (c.transform.parent == transform)
                    continue;

                // Check the name of the bodypart hit and the action state of the opponent
                if (m_Opponent.m_ActionState == ActionState.BLOCK)
                {
                    // Get the percantage of block animation that has elapsed
                    float animationCompletePerc = m_Anim.GetCurrentAnimatorStateInfo(0).normalizedTime /
                        m_Anim.GetCurrentAnimatorStateInfo(0).length;

                    if (c.tag == "Hand" && animationCompletePerc >= 0.7f)
                    {
                        Debug.Log(transform.name + "'s punch was blocked.");
                        AddReward(m_Match.m_Academy.m_PunchBlocked);
                    }
                    else if (c.tag == "Head")
                    {
                        Debug.Log(transform.name + " landed punch.");
                        AddReward(m_Match.m_Academy.m_SuccessfulPunch);
                        // Apply damage to opponent
                        m_Opponent.m_Life -= Mathf.Max(1, (m_Strength - m_Opponent.m_Defense));
                        m_Opponent.AddReward(-m_Match.m_Academy.m_SuccessfulPunch);
                    }
                }
                else
                {
                    if (c.tag == "Head")
                    {
                        Debug.Log(transform.name + " landed punch.");
                        AddReward(m_Match.m_Academy.m_SuccessfulPunch);
                        // Apply damage to opponent
                        m_Opponent.m_Life -= Mathf.Max(1, (m_Strength - m_Opponent.m_Defense));
                        m_Opponent.AddReward(-m_Match.m_Academy.m_SuccessfulPunch);
                    }
                }
            }
        }
	}
}