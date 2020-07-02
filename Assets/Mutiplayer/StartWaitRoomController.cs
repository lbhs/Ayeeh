using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartWaitRoomController : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;
    [HideInInspector]
    private int mutiplayerRoomSceneIndex=2;
    [SerializeField]
    private int menuSceneIndex;
    private int playerCount;
    private int roomSize;
    public Text PlayerCountText;
    public int miniumStartPlayCount;
    public GameObject StartNowButtonParent;
    public GameObject StartNowButton;
    public InputField IF;
    private bool StartingGame;
    public Text LeftMapVoteCount;
    private int leftCount;
    public Text MiddleMapVoteCount;
    private int middleCount;
    public Text RightMapVoteCount;
    private int rightCount;
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
        int v = Mathf.Max(leftCount, middleCount, rightCount);
        if(leftCount>middleCount &&leftCount>rightCount)
        {
            mutiplayerRoomSceneIndex = 2;
        }
        else if (middleCount>leftCount&&middleCount>rightCount)
        {
            mutiplayerRoomSceneIndex = 4;
        }
        else if(rightCount>leftCount&&rightCount>middleCount)
        {
            mutiplayerRoomSceneIndex = 5;
        }
        else if(leftCount == middleCount)
        {
            int x = Random.Range(0, 2);
            if (x == 0)
            {
                mutiplayerRoomSceneIndex = 2;
            }
            else
            {
                mutiplayerRoomSceneIndex = 4;
            }
        }
        else if (leftCount == rightCount)
        {
            int x = Random.Range(0, 2);
            if (x == 0)
            {
                mutiplayerRoomSceneIndex = 2;
            }
            else
            {
                mutiplayerRoomSceneIndex = 5;
            }
        }
        else if(middleCount == rightCount)
        {
            int x = Random.Range(0, 2);
            if (x == 0)
            {
                mutiplayerRoomSceneIndex = 4;
            }
            else
            {
                mutiplayerRoomSceneIndex = 5;
            }
        }
        else
        {
            int x = Random.Range(0, 3);
            if (x == 0)
            {
                mutiplayerRoomSceneIndex = 2;
            }
            else if(x ==1)
            {
                mutiplayerRoomSceneIndex = 4;
            }
            else
            {
                mutiplayerRoomSceneIndex = 5;
            }
        }
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
    public void SetUsername()
    {
        if (IF.text != null)
        {
            PhotonNetwork.NickName = IF.text;
        }
    }
    public void VoteForMap(int Vote)
    {
        myPhotonView.RPC("AddToCount", RpcTarget.MasterClient, Vote);
    }

    [PunRPC]
    void AddToCount(int Vote)
    {
        if(Vote == 1)
        {
            leftCount++;
        }else if(Vote == 2)
        {
            middleCount++;
        }
        else
        {
            rightCount++;
        }
        myPhotonView.RPC("SetCount", RpcTarget.AllBuffered,leftCount,middleCount,rightCount);
    }

    [PunRPC]
    void SetCount(int l, int m,int r)
    {
        leftCount = l;
        middleCount = m;
        rightCount = r;
        LeftMapVoteCount.text = leftCount.ToString();
        MiddleMapVoteCount.text = middleCount.ToString();
        RightMapVoteCount.text = rightCount.ToString();
    }
}
