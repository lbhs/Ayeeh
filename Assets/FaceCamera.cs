using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
            transform.GetChild(1).GetComponent<Text>().text = transform.parent.GetChild(2).GetComponent<ElectronManager>().countOfCurrentElectrons.ToString();
        }
    }
}
