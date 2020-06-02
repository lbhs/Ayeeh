using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsidesMover : MonoBehaviour
{
    public List<Transform> insides = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var GO in insides)
        {
            GO.transform.localPosition = new Vector3(Random.Range(-1.49f, 1.49f), Random.Range(-1.49f, 1.49f), Random.Range(-1.49f, 1.49f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<PlayerController>().GotHit(other.gameObject);
    }
}
