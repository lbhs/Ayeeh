using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeLightling : MonoBehaviour
{
    public GameObject Lightning;
    public Collider collider;
    public AudioSource boom;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().Camera.GetComponent<CameraFollow>().LightningAnim.SetTrigger("Shake");
            other.GetComponent<PlayerController>().Camera.GetComponent<CameraFollow>().lightningScript.MoveUp();
            Lightning.SetActive(false);
            collider.enabled = false;
            StartCoroutine(ChargeUp());
        }
    }

    IEnumerator ChargeUp()
    {
        yield return new WaitForSeconds(Random.Range(15, 40));
        Lightning.SetActive(true);
        collider.enabled = true;
        boom.enabled = true;
    }
}
