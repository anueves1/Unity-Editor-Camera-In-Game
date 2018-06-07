using UnityEngine;
using Cinemachine;

namespace Anueves1.EditorCamera
{
    public class EditorCameraMovement : MonoBehaviour
    {
        [Header("Movement")]
        [Space(5f)]

        [SerializeField]
        private float m_Speed = 1;

        [SerializeField]
        private float m_ShiftMultiplier = 2;

        [Header("View")]
        [Space(5f)]

        [SerializeField]
        private bool m_RightClickForRotation = true;

        [SerializeField]
        private bool m_HideCursor = true;

        private CinemachineBrain m_Camera;

        private void Start()
        {
            //Get the camera's brain.
            m_Camera = transform.GetComponentInChildren<CinemachineBrain>();

            //If we need to hide the cursor.
            if(m_HideCursor)
            {
                //Lock the cursor.
                Cursor.lockState = CursorLockMode.Locked;

                //Make it invisible.
                Cursor.visible = false;
            }
        }

        private void FixedUpdate()
        {
            //Get the speed.
            var speed = m_Speed;

            //Check if we need to enable look.
            var lookMode = Input.GetKey(KeyCode.Mouse1) || !m_RightClickForRotation;
            //Only enable the brain when we need to look around.
            m_Camera.enabled = lookMode;

            //Check if we need to increse the speed.
            var sprintMode = Input.GetKey(KeyCode.LeftShift);
            //Multiply the speed by 2 when sprinting.
            speed *= sprintMode ? m_ShiftMultiplier : 1;

            //Get the horizontal input.
            var horizontalInput = Input.GetAxis("Horizontal");
            //Get the vertical input.
            var verticalInput = Input.GetAxis("Vertical");
            //Get the upDown input.
            var upDownInput = Input.GetAxis("UpDown");

            //Compute the forwards movement.
            Vector3 forward = m_Camera.transform.forward * verticalInput;
            //Compute the sideways movement.
            Vector3 sideways = m_Camera.transform.right * horizontalInput;
            //Compute the up and down movement.
            Vector3 upDown = Vector3.up * upDownInput;

            //Add them to get the movement direction.
            Vector3 dir = (forward + sideways + upDown) * (speed / 10);
               
            //Move towards that direction.
            transform.localPosition += dir;
        }
    }
}