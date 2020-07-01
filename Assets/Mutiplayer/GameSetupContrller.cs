using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameSetupContrller : MonoBehaviour
{ 
    private PhotonView PV;

    public int NextPlayersTeam;
    public Transform[] spawnPointsTeamOne;
    public Transform[] spawnPointsTeamTwo;

    public static GameSetupContrller GS;

    private void OnEnable()
    {
        if(GameSetupContrller.GS == null)
        {
            GameSetupContrller.GS = this;
        }
    }

    private void Awake()
    {
        CreatePlayer();
        PV = GetComponent<PhotonView>();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerPreafab"), Vector3.zero, Quaternion.identity);
    }

     void Update()
     {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
     }

    public void NetowrkSpawn(GameObject Prefab, Vector3 pos)
    {
        GameObject GO;
        GO = PhotonNetwork.Instantiate(Prefab.name, pos, Quaternion.identity);
        GO.GetComponent<PhotonView>().RequestOwnership();
    }
    
    public void UpdateTeam()
    {
        if (NextPlayersTeam == 1)
        {
            NextPlayersTeam = 2;
        }
        else
        {
            NextPlayersTeam = 1;
        }
    }
}