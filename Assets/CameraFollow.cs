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

    private void Update()
    {
        transform.position = CameraFollowObj.transform.position;
        transform.rotation = CameraFollowObj.transform.rotation;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            yaw += 2 * Input.GetAxisRaw("Mouse X");
            pitch -= 2 * Input.GetAxis("Mouse Y");

        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        transform.eulerAngles = new Vector3(pitch, yaw, 0);

        ElectronCountText.text = CameraFollowObj.transform.root.GetComponent<PlayerController>().EM.countOfCurrentElectrons.ToString();
    }
}
