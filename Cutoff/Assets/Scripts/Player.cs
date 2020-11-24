using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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

    public DismemberUI dismemberUI;

    public BodyInfo bodyInfo;

    public Inventory pInv;

    public bool inMenu;
    public bool inInv;

    public GameObject eyeLobj, eyeRobj;

    public GameObject flashlightObj;

    [Header("Body Parts")]
    public Button bFootL, bFootR, bHandL, bHandR, bEyeL, bEyeR;
    public Image iFootL, iFootR, iHandL, iHandR, iEyeL, iEyeR;
    public Sprite sFootL, sFootR, sHandL, sHandR, sEyeL, sEyeR;
    public Sprite xFootL, xFootR, xHandL, xHandR, xEyeL, xEyeR;
    private void Awake()
    {
        bodyInfo.Reset();
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
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

        if (!inMenu)
        {
            if (!inInv)
            {
                Cursor.visible = false;
                Vector3 move = transform.right * x + transform.forward * z;
                controller.Move(move * Time.deltaTime * (playerSpeed + (bodyInfo.legTotal - 2) * legPenalty));

                playerVelocity.y += gravityValue * Time.deltaTime;
                controller.Move(playerVelocity * Time.deltaTime);

                Rotate();
            } else
            {
                Cursor.visible = true;
            }

            if (Input.GetKeyDown("e"))
            {
                Interact();
                Debug.DrawRay(cameraHolder.position, cameraHolder.TransformDirection(Vector3.forward), Color.red, 2);
            }
        } else
        {
            Cursor.visible = true;
        } 

        if (Input.GetKeyDown("i"))
        {
            if (!inInv)
            {
                inInv = true;
            } else
            {
                inInv = false;
            }
        }
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

    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraHolder.position, cameraHolder.TransformDirection(Vector3.forward), out hit, 2))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.tag.Equals("Machine"))
            {
                dismemberUI.gameObject.SetActive(true);
                inMenu = true;
                Debug.Log("Interact");
                //hit.collider.SendMessageUpwards("YourScriptFunctionHere");
            }

            if (hit.collider.tag.Equals("Item"))
            {
                string iName = hit.collider.gameObject.name;
                int id;

                print("HIT: " + iName);

                if (iName == "Flashlight")
                {
                    id = 0;
                    flashlightObj.SetActive(true);
                }
                else if (iName == "Key1(Clone)")
                {
                    id = 1;
                } 
                else
                {
                    id = -1;
                }

                if (id != -1)
                {
                    inInv = true;
                    pInv.GiveItem(id);
                    Destroy(hit.collider.gameObject);
                }
            }
        }

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

    void handHandler()
    {
        if (!bodyInfo.handL)
        {
            GameObject.Find("/Canvas/InventoryPanel/HandLSlotPanel").SetActive(false);
        }
        if (!bodyInfo.handR)
        {
            GameObject.Find("/Canvas/InventoryPanel/HandRSlotPanel").SetActive(false);
        }
    }

    public struct BodyInfo
    {
        bool earL, earR;
        public bool eyeL, eyeR;
        public bool handL, handR;
        public bool legL, legR;

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
    
    public void UpdateBodyImage()
    {
        if (!bodyInfo.eyeL)
        {
            SetBodyImage(bEyeL, iEyeL, xEyeL);
        }
        if (!bodyInfo.eyeR)
        {
            SetBodyImage(bEyeR, iEyeR, xEyeR);
        }
        if (!bodyInfo.handR)
        {
            SetBodyImage(bHandR, iHandR, xHandR);
        }
        if (!bodyInfo.handL)
        {
            SetBodyImage(bHandL, iHandL, xHandL);
        }
        if (!bodyInfo.legL)
        {
            SetBodyImage(bFootL, iFootL, xFootL);
        }
        if (!bodyInfo.legR)
        {
            SetBodyImage(bFootR, iFootR, xFootR);
        }

        legHandler();
        eyeHandler();
        handHandler();
    }
     void SetBodyImage(Button butt, Image im, Sprite spr)
    {
        butt.image.sprite = spr;
        butt.interactable = false;
        im.sprite = spr;
    }

}

