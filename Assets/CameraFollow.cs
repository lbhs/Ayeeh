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
        mobileSupport = true;
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
            yaw += 1.75f * RJ.GetInputDirection().x;
            pitch -= 1f * RJ.GetInputDirection().y;
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                yaw += 1.75f * Input.GetAxisRaw("Mouse X");
                pitch -= 1.75f * Input.GetAxis("Mouse Y");


            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        transform.eulerAngles = new Vector3(pitch, yaw, 0);

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
        foreach (var item in ls)
        {
            if (item.numOfPoints > item.CurrentNumOfPoints && found == false)
            {
                item.BackToZero();
                found = true;
            }
        }
    }

}
