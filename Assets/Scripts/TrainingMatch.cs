using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLBoxing
{
    /// <summary>
    /// Manages a match between two Boxer agents during training
    /// </summary>
    public class TrainingMatch : MonoBehaviour
    {

        // References to the two boxers
        public Boxer m_BoxerA;
        public Boxer m_BoxerB;

        public BoxingAcademy m_Academy;
        

        // Use this for initialization
        void Start()
        {
            m_BoxerA.AgentReset();
            m_BoxerB.AgentReset();
        }

        // Update is called once per frame
        void Update()
        {
            // Check for knock out
            if (m_BoxerA.IsKnockedOut() && m_BoxerB.IsKnockedOut())
            {
                // Tie
                // Punish both agents and reset the match
                m_BoxerA.AddReward(-m_Academy.m_KnockoutReward);
                m_BoxerA.AddReward(-m_Academy.m_KnockoutReward);

                m_BoxerA.Done();
                m_BoxerB.Done();

                MatchReset();
            }
            else if (!m_BoxerA.IsKnockedOut() && m_BoxerB.IsKnockedOut())
            {
                // Boxer A Win
                // Reward red, punish blue, reset
                m_BoxerA.AddReward(m_Academy.m_KnockoutReward);
                m_BoxerA.AddReward(-m_Academy.m_KnockoutReward);

                m_BoxerA.Done();
                m_BoxerB.Done();

                MatchReset();
            }
            else if (m_BoxerA.IsKnockedOut() && !m_BoxerB.IsKnockedOut())
            {
                // Boxer B Win
                // Reward blue, punish red, reset
                m_BoxerA.AddReward(-m_Academy.m_KnockoutReward);
                m_BoxerA.AddReward(m_Academy.m_KnockoutReward);

                m_BoxerA.Done();
                m_BoxerB.Done();

                MatchReset();
            }

            else if (m_BoxerA.transform.position.y > 2f || m_BoxerB.transform.position.y > 2f)
            {
                m_BoxerA.AddReward(-m_Academy.m_KnockoutReward);
                m_BoxerA.AddReward(-m_Academy.m_KnockoutReward);

                m_BoxerA.Done();
                m_BoxerB.Done();

                MatchReset();
            }

            else if (m_BoxerA.transform.position.y < -2f || m_BoxerB.transform.position.y < -2f)
            {
                m_BoxerA.AddReward(-m_Academy.m_KnockoutReward);
                m_BoxerA.AddReward(-m_Academy.m_KnockoutReward);

                m_BoxerA.Done();
                m_BoxerB.Done();

                MatchReset();
            }
        }
        
        public virtual void MatchReset()
        {
            // Place agents back to their starting spots
            m_BoxerA.AgentReset();
            m_BoxerB.AgentReset();
        }
    }
}
