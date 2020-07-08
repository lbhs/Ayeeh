﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerControler : MonoBehaviour
{
    private PhotonView PV;
    private Text textUI;
    private double startTIme;
    public float time = 120;
    public Slider slider;
    public Text tRed;
    public Text tBlue;
    private int totalElectronCount;
    private int bluecount;
    private int redcount;
    public GameObject GameOverObject;
    private Animator Anim;
    bool HasLoaded = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndGameTrue());
        PV = GetComponent<PhotonView>();
        textUI = GetComponent<Text>();
        Anim = GetComponent<Animator>();
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("starTimer", RpcTarget.All, PhotonNetwork.Time);
        }
    }
    
   public void addEToRed(bool remove)
    {
        PV.RPC("addEToRedRPC", RpcTarget.All,remove);
    }
    [PunRPC]
    void addEToRedRPC(bool remove)
    {
        redcount++;
        if (remove == true)
        {
            bluecount--;
        }
        tRed.text = redcount.ToString();
        tBlue.text = bluecount.ToString();
    }

    public void addEToBlue(bool remove)
    {
        PV.RPC("addEToBlueRPC", RpcTarget.All, remove);
    }
    [PunRPC]
    void addEToBlueRPC(bool remove)
    {
        if (remove == true)
        {
            redcount--;
        }
        bluecount++;
        tRed.text = redcount.ToString();
        tBlue.text = bluecount.ToString();
    }

    public void addToTotal(int value)
    {
        PV.RPC("addToTotoalRPC", RpcTarget.All, value);
    }
    [PunRPC]
    void addToTotoalRPC(int value)
    {
        totalElectronCount = totalElectronCount + value;
    }

    [PunRPC]
    void starTimer(double time)
    {
        startTIme = time;
    }

    
    IEnumerator EndGameTrue()
    {
        HasLoaded = false;
        yield return new WaitForSecondsRealtime(5);
        HasLoaded = true;
    }
    // Update is called once per frame
    void Update()
    {
        totalElectronCount = 0;
        bluecount = 0;
        redcount = 0;
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            ElectronManager EM = item.transform.GetChild(2).GetComponent<ElectronManager>();
            totalElectronCount = totalElectronCount + EM.E.Count;
            if(item.GetComponent<PlayerController>().TeamNumber == 1)
            {
                bluecount = bluecount + EM.countOfCurrentElectrons;
            }
            else if (item.GetComponent<PlayerController>().TeamNumber == 2)
            {
                redcount = redcount + EM.countOfCurrentElectrons;
            }
        }
        slider.maxValue = redcount+bluecount;
        slider.value = redcount;
        tBlue.text = bluecount.ToString();
        tRed.text = redcount.ToString();

        if (startTIme + 1 <= PhotonNetwork.Time)
        {
            time -= Time.deltaTime;
            textUI.text = decimal.Truncate(decimal.Parse(time.ToString())).ToString();
            if(time <= 5 && Anim.GetBool("End") == false)
            {
                Anim.SetBool("End", true);
            }
            if (tBlue.text == "0" || tRed.text == "0" && HasLoaded == true)
            {
                time = -0.1f;
            }
            if (time < 0)
            {
                if (hasrunthis == false)
                {
                    hasrunthis = true;
                    StartCoroutine(End());
                }
            }
        }

       
    }
    bool hasrunthis = false;
    IEnumerator End()
    {
        yield return new WaitForSecondsRealtime(4);
        GameObject GOO = Instantiate(GameOverObject, Vector3.zero, Quaternion.identity);
        if (redcount > bluecount)
        {
            GOO.GetComponent<GameOverObjectScript>().callWin(0);
        }
        else if (redcount < bluecount)
        {
            GOO.GetComponent<GameOverObjectScript>().callWin(1);
        }
        else
        {
            GOO.GetComponent<GameOverObjectScript>().callWin(2);
        }
        SceneManager.LoadScene(3);
    }
}
