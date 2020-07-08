using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobileSupportScript : MonoBehaviour
{
    public static bool mobileSupport = false;
    // Start is called before the first frame update
    void Start()
    {
        if(mobileSupport == false)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
