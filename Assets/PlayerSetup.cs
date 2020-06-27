using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
    public PhotonView PV;
    public GameObject PlayerAtom;
    public int teamNumber;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("GetTeamRPC", RpcTarget.MasterClient);
        }
        
    }
    void Update()
    {
        if (PlayerAtom == null && teamNumber != 0)
        {
            if (teamNumber == 1)
            {
                int spawnpicker = Random.Range(0, GameSetupContrller.GS.spawnPointsTeamOne.Length);
                if (PV.IsMine)
                {
                    PlayerAtom = PhotonNetwork.Instantiate("Player", GameSetupContrller.GS.spawnPointsTeamOne[spawnpicker].position, GameSetupContrller.GS.spawnPointsTeamOne[spawnpicker].rotation);
                    PlayerAtom.GetComponent<PlayerController>().SetTeam(teamNumber);
                }
            }
            else
            {
                int spawnpicker = Random.Range(0, GameSetupContrller.GS.spawnPointsTeamTwo.Length);
                if (PV.IsMine)
                {
                    PlayerAtom = PhotonNetwork.Instantiate("Player", GameSetupContrller.GS.spawnPointsTeamTwo[spawnpicker].position, GameSetupContrller.GS.spawnPointsTeamTwo[spawnpicker].rotation);
                    PlayerAtom.GetComponent<PlayerController>().SetTeam(teamNumber);
                }
            }
        }
    }
    [PunRPC]
    void GetTeamRPC()
    {
        teamNumber = GameSetupContrller.GS.NextPlayersTeam;
        GameSetupContrller.GS.UpdateTeam();
        PV.RPC("SendTeamRPC", RpcTarget.OthersBuffered, teamNumber);
    }
    [PunRPC]
    void SendTeamRPC(int TeamNum)
    {
        teamNumber = TeamNum;
    }

}
