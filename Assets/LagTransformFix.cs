using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LagTransformFix : MonoBehaviour, IPunObservable
{
    protected Vector3 remotePlayerPosition;
    private PhotonView PV;
    public float threshold = 5f;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
            return;
        var LagDistance = remotePlayerPosition - transform.position;

        if(LagDistance.magnitude > threshold)
        {
            transform.position = remotePlayerPosition;
            LagDistance = Vector3.zero;
        }

        if (LagDistance.magnitude < 0.11f)
        {

        }
        else
        {

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            remotePlayerPosition = (Vector3)stream.ReceiveNext();
        }
       // throw new System.NotImplementedException();
    }
}
