﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartWaitRoomController : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;
    [SerializeField]
    private int mutiplayerRoomSceneIndex;
    [SerializeField]
    private int menuSceneIndex;
    private int playerCount;
    private int roomSize;
    public Text PlayerCountText;
    public int miniumStartPlayCount;
    public GameObject StartNowButtonParent;
    public GameObject StartNowButton;

    private bool StartingGame;
    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        PlayerCountUpdate();
        if (PhotonNetwork.IsMasterClient)
        {
            StartNowButtonParent.SetActive(true);
        }
        else
        {
            StartNowButtonParent.SetActive(false);
        }
    }
    void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        PlayerCountText.text = "(" + playerCount + "/" + roomSize + ")";
        if(playerCount >= miniumStartPlayCount && playerCount != roomSize)
        {
            StartNowButton.SetActive(true);
        }
        else
        {
            StartNowButton.SetActive(false);
        }
        if(playerCount == roomSize)
        {
            if (StartingGame)
                return;
            StartGame();
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();
    }
    void StartGame()
    {
        StartingGame = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(mutiplayerRoomSceneIndex);
    }
    public void CancelButton()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
    public void startNowButton()
    {
        if (StartingGame)
            return;
        StartGame();
    }
}
