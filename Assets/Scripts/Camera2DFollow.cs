using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        public float yPosBoundary = -1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
        private bool lookingDown = false;

        float nextTimeToSearch = 0;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            if (target == null)
            {
                FindPlayer();
                return;
            }

            if (SceneManager.GetActiveScene().name == "Level_wzrok")
            {
                if (CrossPlatformInputManager.GetButtonDown("down"))
                    lookingDown = true;
                if (CrossPlatformInputManager.GetButtonUp("down"))
                    lookingDown = false;
            }


            Vector3 dest = target.position;

            if (lookingDown)
                dest = new Vector3(dest.x, dest.y - 5.0f, dest.z);

            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (dest - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = dest + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, yPosBoundary, Mathf.Infinity), newPos.z);

            transform.position = newPos;

            m_LastTargetPosition = dest;
        }

        void FindPlayer()
        {
            if (nextTimeToSearch <= Time.time)
            {
                GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
                if (searchResult != null)
                    target = searchResult.transform;
                nextTimeToSearch = Time.time + 0.5f;
            }
        }
    }
}
