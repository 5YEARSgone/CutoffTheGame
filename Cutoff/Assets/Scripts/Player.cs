using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player: MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float legPenalty = 0.5f;

    private float gravityValue = -9.81f;

    public Transform cameraHolder;
    public Transform bodyTilt;
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;


    BodyInfo bodyInfo;

    public GameObject eyeLobj, eyeRobj;

    private void Awake()
    {
        bodyInfo.Reset();
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        bodyInfo.legTotal = 0;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * Time.deltaTime * (playerSpeed + (bodyInfo.legTotal - 2) * legPenalty));
        legHandler();
        eyeHandler();

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        Rotate();
    }


    public void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");

        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
    }

    private void legHandler()
    {
        if (bodyInfo.legTotal == 1)
        {
            bodyTilt.localPosition = new Vector3(0, -0.25f, 0);
            bodyTilt.localRotation = Quaternion.Euler(new Vector3(0, 0, 10));
        } else if (bodyInfo.legTotal == 0)
        {
            bodyTilt.localPosition = new Vector3(0, -0.65f, 0);
            bodyTilt.localRotation = Quaternion.Euler(new Vector3(0, 0, 20));
        }
    }

    private void eyeHandler()
    {
        if (!bodyInfo.eyeL)
        {
             eyeLobj.SetActive(true);
        }
        if (!bodyInfo.eyeR)
        {
            eyeRobj.SetActive(true);
        }
    }

    public struct BodyInfo
    {
        bool earL, earR;
        public bool eyeL, eyeR;
        bool handL, handR;
        bool legL, legR;

        public int earTotal, eyeTotal, handTotal, legTotal;

        public void UpdateBody()
        {
            earTotal = addBoth(earL, earR);
            eyeTotal = addBoth(eyeL, eyeR);
            handTotal = addBoth(handL, handR);
            legTotal = addBoth(legR, legL);
        }

        int addBoth(bool p1, bool p2)
        {
            return Convert.ToInt32(p1) + Convert.ToInt32(p2);
        }

        public void Reset()
        {
            earL = earR = eyeL = eyeR = handL = handR = legL = legR = true;
            UpdateBody();
        }
    }
}

