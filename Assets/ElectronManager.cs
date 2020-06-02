using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronManager : MonoBehaviour
{
    public List<GameObject> E = new List<GameObject>();
    public float changeRate = 0.1f;
    [HideInInspector]
    public int countOfCurrentElectrons;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in E)
        {
            countOfCurrentElectrons++;
            item.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(0.1f,15f), Random.Range(0.1f, 10f), Random.Range(0.1f, 10f));
        }
        StartCoroutine(Change());
    }

    public Vector3 RemoveElectron()
    {
        countOfCurrentElectrons--;
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

    public Vector3 AddElectron()
    {
        countOfCurrentElectrons++;
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
        return DisabledE[0].transform.position;
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

                item.transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

            }
        }
        StartCoroutine(Change());
    }
}
