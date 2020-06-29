using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontdestoyoneload : MonoBehaviour
{
    void Awake()
    {


        DontDestroyOnLoad(this.gameObject);
    }
}
