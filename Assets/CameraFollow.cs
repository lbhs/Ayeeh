using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour {

    public GameObject CameraFollowObj;
    public float yaw = 0;
    public float pitch = 0;
    public Animator LightningAnim;
    public Text ElectronCountText;
    public List<LightningScript> lightningScripts = new List<LightningScript>();

    private bool startedDestory;
    public bool mobileSupport;
    //public GameObject LJ;
    //public GameObject RJ;

    public RightJoystick RJ;

    private void Start()
    {
        RJ = GameObject.Find("Right Joystick").GetComponent<RightJoystick>();
        mobileSupport = mobileSupportScript.mobileSupport;
    }
    private void Update()
    {
        
        if (CameraFollowObj == null)
        {
            if (startedDestory == false)
            {
                startedDestory = true;
                Destroy(gameObject, 4);
            }
        }
        transform.position = CameraFollowObj.transform.position;
        transform.rotation = CameraFollowObj.transform.rotation;
        if (mobileSupport)
        {
            yaw += 160 * RJ.GetInputDirection().x * Time.deltaTime;
            pitch -= 120 * RJ.GetInputDirection().y * Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                yaw += 190 * Input.GetAxisRaw("Mouse X") * Time.deltaTime;
                pitch -= 150 * Input.GetAxis("Mouse Y") * Time.deltaTime;


            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        if(pitch > 40)
        {
            pitch = 40;
        }else if (pitch < -30)
        {
            pitch = -30;
        }
        transform.eulerAngles = new Vector3(pitch, yaw, 0);
        print(transform.eulerAngles.z);
        /* if (transform.eulerAngles.x > 40)
         {
             transform.eulerAngles = new Vector3(40, transform.eulerAngles.y, 0);
         }
         else if (transform.eulerAngles.x < 330 && transform.eulerAngles.x > 180)
         {
             transform.eulerAngles = new Vector3(330, transform.eulerAngles.y, 0);
         }*/
        ElectronCountText.text = CameraFollowObj.transform.root.GetComponent<PlayerController>().EM.countOfCurrentElectrons.ToString();

    }

    public void MoveUpLightning()
    {
        bool found = false;
        print("up");
        /*foreach (var item in lightningScripts)
        {
            if(item.numOfPoints > item.CurrentNumOfPoints && found == false)
            {
                item.MoveUp();
                found = true;
            }
        }*/
        if (0 < lightningScripts[0].CurrentNumOfPoints)
        {
            lightningScripts[0].MoveUp();
        }
        else if (0 < lightningScripts[1].CurrentNumOfPoints)
        {
            lightningScripts[1].MoveUp();
        }
        else if (0 < lightningScripts[2].CurrentNumOfPoints)
        {
            lightningScripts[2].MoveUp();
        }
        else if (0 < lightningScripts[3].CurrentNumOfPoints)
        {
            lightningScripts[3].MoveUp();
        }
    }

    public void RemoveALightning()
    {
        print("zero");
        List<LightningScript> ls = lightningScripts;
        ls.Reverse();
        bool found = false;
        int i = 0;
        foreach (var item in ls)
        {
            if (item.numOfPoints > item.CurrentNumOfPoints && found == false)
            {
                found = true;
                item.BackToZero();
                /*if (i == 0)
                {
                    item.BackToZero();
                }
                else
                {
                    if (ls[i - 1].numOfPoints == ls[i - 1].CurrentNumOfPoints)
                    {
                        item.BackToZero();
                    }
                    else
                    {
                        item.BackToZero();
                        int currStore;
                        if (item.CurrentNumOfPoints == 1)
                        {
                            currStore = 1;
                        }
                        else if (item.CurrentNumOfPoints == 2)
                        {
                            currStore = 2;
                        }
                        else
                        {
                            currStore = 0;
                        }
                        ls[i - 1].BackToZero();
                        if(currStore == 1)
                        {
                            ls[i - 1].MoveUp();
                            ls[i - 1].MoveUp();
                        }
                        else if (currStore == 2)
                        {
                            ls[i - 1].MoveUp();
                        }
                    }
                }
            }*/
            }
            i++;
        }
    }

}
