using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameOverObjectScript : MonoBehaviour
{
    public string winner;

    private static bool GameOverObjectScriptExits;
    void Awake()
    {
        if (!GameOverObjectScriptExits)
        {
            GameOverObjectScriptExits = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }

    public void callWin(int num)
    {
       
        if (num == 0)
        {
            GetComponent<PhotonView>().RPC("redWins", RpcTarget.All);
        }
        else if(num == 1)
        {
            GetComponent<PhotonView>().RPC("blueWins", RpcTarget.All);
        }
        else
        {
            GetComponent<PhotonView>().RPC("Tie", RpcTarget.All);
        }
    }

    [PunRPC]
    public void redWins()
    {
        winner = "Red Wins!";
    }
    [PunRPC]
    public void blueWins()
    {
        winner = "Blue Wins!";
    }
    [PunRPC]
    public void Tie()
    {
        winner = "It was a Tie";
    }
    
}
