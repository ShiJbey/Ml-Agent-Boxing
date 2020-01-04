using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLBoxing
{
    public class VersusMenu : MonoBehaviour
    {

        public BoxingMatch m_BoxingMatch;
        public Transform m_Academy;
        public Camera m_OrbitingCamera;
        public Camera m_MatchCamera;

        public void PlayerVsCpu()
        {
            // Set controls for boxers
            m_BoxingMatch.m_BoxerA.m_PlayerControlled = true;
            m_BoxingMatch.m_BoxerA.m_PlayerControlled = false;

            // Disable controls for the boxers
            m_BoxingMatch.m_BoxerA.enabled = false;
            m_BoxingMatch.m_BoxerB.enabled = false;

            // Swap to the match Camera
            ChangeCameras();

            // Reset the match
            m_BoxingMatch.MatchReset();
        }

        public void CpuVsCpu()
        {
            // Set controls for boxers
            m_BoxingMatch.m_BoxerA.m_PlayerControlled = false;
            m_BoxingMatch.m_BoxerA.m_PlayerControlled = false;

            // Disable controls for the boxers
            m_BoxingMatch.m_BoxerA.enabled = false;
            m_BoxingMatch.m_BoxerB.enabled = false;

            // Swap to the match Camera
            ChangeCameras();

            // Reset the match
            m_BoxingMatch.MatchReset();
        }

        public void StartFight()
        {
            // start the countdown script that enables the agents to
            // start moving (enable their agent scripts)
        }

        void ChangeCameras()
        {
            // Swaps out the orbiting camera for the match camera
            m_OrbitingCamera.enabled = false;
            m_MatchCamera.enabled = true;
        }
    }
}

