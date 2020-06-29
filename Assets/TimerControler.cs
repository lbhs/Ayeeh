using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TimerControler : MonoBehaviour
{
    private PhotonView PV;
    private Text textUI;
    private double startTIme;
    public float time = 120;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        textUI = GetComponent<Text>();
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("starTimer", RpcTarget.All, PhotonNetwork.Time);
        }
    }

    [PunRPC]
    void starTimer(double time)
    {
        startTIme = time;
    }
    // Update is called once per frame
    void Update()
    {
        if(startTIme + 1 <= PhotonNetwork.Time)
        {
            time -= Time.deltaTime;
            textUI.text = decimal.Truncate(decimal.Parse(time.ToString())).ToString();
        }
    }
}
