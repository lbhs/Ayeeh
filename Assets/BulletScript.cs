using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletScript : MonoBehaviour
{
    //public GameObject owner;
    private Rigidbody rb;
    public float speed = 35;
    public Vector3 lookPoint;
    public AudioSource AS;
    public AudioClip[] ac;
    public PhotonView PV;
    private void OnTriggerEnter(Collider other)
    {
        if (!PV.IsMine)
        {
            //return;
        }
        //print(owner.name);
        if (other.tag == "floor")
        {
            //world hit
            print("Play particle here");
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            if (PV.IsMine)
            {
                PhotonNetwork.Instantiate("HitParticle2", transform.position, Quaternion.identity);
            }
            //PhotonNetwork.Destroy(transform.root.gameObject);
        }
    }

    void Awake()
    {
        StartCoroutine(despawn());
        if (!PV.IsMine)
        {
            return;
        }
        AS.clip = ac[Random.Range(0, ac.Length)];
        AS.enabled = true;
        //Debug.Log(lookPoint);
        //transform.LookAt(lookPoint);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        rb.velocity = transform.forward * speed;
    }

    IEnumerator despawn()
    {
        yield return new WaitForSeconds(2);
        if (PV.IsMine)
        {
            PhotonNetwork.Destroy(transform.root.gameObject);
        }
    }

    public void hitPointRPC(Vector3 v3)
    {
        PV.RPC("RPCHitPount", RpcTarget.All, v3);
    }

    [PunRPC]
    private void RPCHitPount(Vector3 v3)
    {
        lookPoint = v3;
        transform.LookAt(lookPoint);
    }
}
