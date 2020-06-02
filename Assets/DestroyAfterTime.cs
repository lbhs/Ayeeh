using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(kaboom(time));
    }

    IEnumerator kaboom(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}
