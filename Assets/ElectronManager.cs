using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ElectronManager : MonoBehaviour
{
    public List<GameObject> E = new List<GameObject>();
    public float changeRate = 0.1f;
    //[HideInInspector]
    public int countOfCurrentElectrons;
    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        foreach (var item in E)
        {
            if (item.activeSelf == true)
            {
                countOfCurrentElectrons++;
            }
            item.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(0.1f,15f), Random.Range(0.1f, 10f), Random.Range(0.1f, 10f));
        }
        previousECount = countOfCurrentElectrons;
        StartCoroutine(Change());
    }

    int previousECount = 0;
    private void Update()
    {
        //TEMPORYFIX!!!!!!!!!!
        if(countOfCurrentElectrons > E.Count)
        {
            countOfCurrentElectrons = E.Count;
        }
        else if(countOfCurrentElectrons < 0)
        {
            countOfCurrentElectrons = 0;
        }
        //TEMPORYFIX!!!!!!!
        if(previousECount > countOfCurrentElectrons)
        {
            RemoveElectron();
            previousECount--;
        }
        else if(previousECount < countOfCurrentElectrons)
        {
            AddElectron();
            previousECount++;
        }
    }

    public void add()
    {
        PV.RPC("addRPC", RpcTarget.All);
    }

    [PunRPC]
    void addRPC()
    {
        countOfCurrentElectrons++;
    }

    public void subtract()
    {
        PV.RPC("subtractRPC", RpcTarget.All);
    }

    [PunRPC]
    void subtractRPC()
    {
        countOfCurrentElectrons--;
    }

    public Vector3 RemoveElectron()
    {
        //countOfCurrentElectrons--;
        List<GameObject> CurrentE = new List<GameObject>();
        foreach (var item in E)
        {
            if(item.activeSelf == true)
            {
                CurrentE.Add(item);
            }
        }
        CurrentE[CurrentE.Count - 1].SetActive(false);
        print("removed");
        return CurrentE[CurrentE.Count - 1].transform.position;
    }
   
    public void AddElectron()
    {
        //countOfCurrentElectrons++;
        List<GameObject> DisabledE = new List<GameObject>();
        foreach (var item in E)
        {
            if (item.activeSelf == false)
            {
                DisabledE.Add(item);
            }
        }
        DisabledE[0].SetActive(true);
        GetComponent<AudioSource>().Play();
        print("added");
        //return DisabledE[0].transform.position;
    }
    
    IEnumerator Change()
    {
        yield return new WaitForSeconds(changeRate);
        foreach (var item in E)
        {
            int num = Random.Range(0, 2);
            if (num == 0)
            {
                item.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(0.1f, 15f), Random.Range(0.1f, 10f), Random.Range(0.1f, 10f));
            }
            else
            {
                //item.transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            }
        }
        StartCoroutine(Change());
    }
}
