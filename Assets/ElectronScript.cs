using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ElectronScript : MonoBehaviour
{
    public Collider colider;
    public Collider trigger;
    private Rigidbody rb;
    private PhotonView PV;
    private bool pleaseDesrtoryMe = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(5*((Random.Range(0,2)*2-1)),Random.Range(6,8),5* ((Random.Range(0, 2) * 2 - 1)));
        PV = GetComponent<PhotonView>();
        StartCoroutine(Launch());
    }

    // Update is called once per frame
    void Update()
    {
        if (pleaseDesrtoryMe)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        /*if(transform.position.y < 0 && PV.IsMine)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);
            transform.position = new Vector3(transform.position.x, 0.1f, transform.position.y);
        }*/
        //rb.velocity = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }

    IEnumerator Launch()
    {
        colider.enabled = false;
        trigger.enabled = false;
        yield return new WaitForSeconds(0.4f);
        colider.enabled = true;
        trigger.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ElectronManager otherEM = other.gameObject.transform.GetChild(2).GetComponent<ElectronManager>();
            if (otherEM.countOfCurrentElectrons < otherEM.E.Count)
            {
                GetComponent<MeshRenderer>().enabled = false;
                trigger.enabled = false;
                if (other.GetComponent<PlayerController>().Camera != null)
                {
                    other.GetComponent<PlayerController>().Camera.GetComponent<CameraFollow>().MoveUpLightning();
                }
                otherEM.add();
                if (PV.IsMine)
                {
                    pleaseDesrtoryMe = true;
                    //PhotonNetwork.Destroy(gameObject);
                    //other.GetComponent<PlayerController>().Camera.GetComponent<CameraFollow>().lightningScript.MoveUp();
                    //StartCoroutine(selfDesctruct());
                }
                else
                {
                    PV.RPC("SendDestroyRequest", RpcTarget.All);
                }
            }
        }
    }

    [PunRPC]
    private void SendDestroyRequest()
    {
        if (PV.IsMine)
        {
            pleaseDesrtoryMe = true;
        }
    }
    /*int wait = 0;
     IEnumerator selfDesctruct()
    {
        if (wait == 0)
        {
            yield return new WaitForSeconds(0.01f);
            wait++;
        }
        else
        {
            yield return new WaitForSeconds(2);
            PhotonNetwork.Destroy(gameObject);
            print("kaboom");
        }
    }*/

}
