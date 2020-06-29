using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChargeLightling : MonoBehaviour
{
    public GameObject Lightning;
    public Collider collider;
    public AudioSource boom;
    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().Camera.GetComponent<CameraFollow>().LightningAnim.SetTrigger("Shake");
            other.GetComponent<PlayerController>().Camera.GetComponent<CameraFollow>().lightningScript.MoveUp();
            PV.RPC("Disable", RpcTarget.MasterClient);
            
        }
    }

    [PunRPC]
    private void Disable()
    {
        int t = Random.Range(15, 40);
        PV.RPC("Recharge", RpcTarget.All, t);
    }
    
    [PunRPC]
    void Recharge(int t)
    {
        Lightning.SetActive(false);
        collider.enabled = false;
        StartCoroutine(ChargeUp(t));
    }


    IEnumerator ChargeUp(int time)
    {
        yield return new WaitForSeconds(time);
        Lightning.SetActive(true);
        collider.enabled = true;
        boom.enabled = true;
    }
}
