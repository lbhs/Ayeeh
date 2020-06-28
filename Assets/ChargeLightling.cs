using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeLightling : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().Camera.GetComponent<CameraFollow>().LightningAnim.SetTrigger("Shake");
            other.GetComponent<PlayerController>().Camera.GetComponent<CameraFollow>().lightningScript.MoveUp();
            gameObject.SetActive(false);
        }
    }
}
