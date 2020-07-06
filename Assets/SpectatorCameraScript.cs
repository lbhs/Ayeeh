using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorCameraScript : MonoBehaviour
{
    private bool isNowMain;
    private void Start()
    {
        StartCoroutine(waiting());
    }
    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null && isNowMain == true)
        {
            gameObject.tag = "MainCamera";
        }
        if (Camera.main == gameObject.GetComponent<Camera>() && GetComponent<AudioListener>() == null)
        {
            gameObject.AddComponent<AudioListener>();
        }
        else if (Camera.main != gameObject.GetComponent<Camera>() && GetComponent<AudioListener>() != null)
        {
            Destroy(gameObject.GetComponent<AudioListener>());
        }
    }
    IEnumerator waiting()
    {
        yield return new WaitForSecondsRealtime(8);
        isNowMain = true;
    }
}
