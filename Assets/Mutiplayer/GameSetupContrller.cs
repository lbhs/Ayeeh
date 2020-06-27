using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameSetupContrller : MonoBehaviour
{ 
    private PhotonView PV;

    private void Awake()
    {
        CreatePlayer();
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        print(PhotonNetwork.PlayerList[0]);
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
    
}