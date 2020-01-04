using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace MLBoxing
{
    public class BoxingAcademy : Academy
    {

        // Reward amounts
        public float m_MoveToOppReward = 0.1f;
        public float m_SuccessfulPunch = 2.0f;
        public float m_FailedBlock = -0.05f;
        public float m_SuccessfulBlock = 0.05f;
        public float m_PunchBlocked = -0.05f;
        public float m_KnockoutReward = 1.5f;


        //
        public override void AcademyReset()
        {
            base.AcademyReset();
        }

        public override void AcademyStep()
        {
            base.AcademyStep();
        }
    }
}

