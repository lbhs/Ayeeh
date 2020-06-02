using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ElectronScript : MonoBehaviour
{
    public Collider colider;
    public Collider trigger;
    private Rigidbody rb;
    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(5*(Random.Range(0,2)*2-1),Random.Range(6,8),5* (Random.Range(0, 2) * 2 - 1));
        PV = GetComponent<PhotonView>();
        StartCoroutine(Launch());
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);
            transform.position = new Vector3(transform.position.x, 0.1f, transform.position.y);
        }
    }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(00.1f);
        colider.enabled = true;
        trigger.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ElectronManager otherEM = other.gameObject.transform.GetChild(2).GetComponent<ElectronManager>();
            if (otherEM.countOfCurrentElectrons < otherEM.E.Count)
            {
                Destroy(gameObject);
            }
        }
    }
}
