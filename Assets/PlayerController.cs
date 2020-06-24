using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 8;
    public GameObject cam;
    private GameObject Camera;
    private CharacterController controller;
    private Vector3 targetDirection;
    public float gravityScale = 0.0025f;
    public PhotonView PV;
    public ElectronManager EM;
    public bool CanBullet1 = false;
    public bool CanBullet2 = false;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            Destroy(GetComponent<Rigidbody>());
            return;
        }
        Camera = Instantiate(cam);
        Camera.GetComponent<CameraFollow>().CameraFollowObj = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        co.Add(StartCoroutine(check()));
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        // rb.AddForce(new Vector3(Input.GetAxis("Vertical") * speed,0, Input.GetAxis("Horizontal") * speed));
        //rb.AddForce(Camera.main.transform.forward* Input.GetAxis("Vertical")*speed);
        //rb.AddForce(Camera.main.transform.forward * Input.GetAxis("Horizontal") * speed);
        float ystore = targetDirection.y;
        targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        targetDirection = Camera.transform.GetChild(0).TransformDirection(targetDirection);
        targetDirection.y = ystore;
        if (controller.isGrounded)
        {
            targetDirection.y = 0f;
        }
        targetDirection.y = targetDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(targetDirection* speed * Time.deltaTime);
       // rb.AddForce(Input.GetAxis("Vertical") * speed, 0, Input.GetAxis("Vertical") * speed);
        //rb.AddForce(Input.GetAxis("Horizontal") * speed, 0, -Input.GetAxis("Horizontal") * speed);
       // print(Camera.main.transform.forward);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.transform.GetChild(0).GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //draw invisible ray cast/vector
                Debug.DrawLine(ray.origin, hit.point);
                //log hit area to the console
                //Debug.Log(hit.point);
                if (!Input.GetKey(KeyCode.Mouse1))
                {
                    if (CanBullet1 == true && CanBullet2 == false)
                    {
                        GameObject b = PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity);
                        b.transform.GetChild(0).GetComponent<BulletScript>().hitPointRPC(hit.point);

                        Camera.GetComponent<CameraFollow>().LightningAnim.SetTrigger("Down");
                        CanBullet1 = false;
                        CanBullet2 = false;
                        foreach (var item in co)
                        {
                            StopCoroutine(item);
                        }
                        co.Add(StartCoroutine(check()));
                    }
                    else if (CanBullet2 == true)
                    {
                        GameObject b = PhotonNetwork.Instantiate("Bullet2", transform.position, Quaternion.identity);
                        b.transform.GetChild(0).GetComponent<BulletScript>().hitPointRPC(hit.point);

                        Camera.GetComponent<CameraFollow>().LightningAnim.SetTrigger("Down");
                        CanBullet1 = false;
                        CanBullet2 = false;
                        foreach (var item in co)
                        {
                            StopCoroutine(item);
                        }
                        co.Add(StartCoroutine(check()));
                    }
                    //PV.RPC("RPCBulletVars", RpcTarget.All, b, gameObject, hit.point);
                    //b.transform.GetChild(0).GetComponent<BulletScript>().owner = gameObject;
                    //b.transform.GetChild(0).GetComponent<BulletScript>().lookPoint = hit.point;
                    //rb.AddForce(hit.point * bulletSpeed);
                    // b.transform.GetChild(0).GetComponent<Rigidbody>().velocity = hit.point;
                }
            }
        }
    }
    public List<Coroutine> co = new List<Coroutine>();
    IEnumerator check()
    {
        yield return new WaitForSeconds(0.2f);
        CanBullet1 = true;
        yield return new WaitForSeconds(1.6f);
        CanBullet2 = true;
    }
    /*[PunRPC]
    private void RPCBulletVars(GameObject b, GameObject owner, Vector3 hitPoint)
    {
        b.transform.GetChild(0).GetComponent<BulletScript>().owner = owner;
        b.transform.GetChild(0).GetComponent<BulletScript>().lookPoint = hitPoint;
    }*/

   /* [PunRPC]
    public void Hit()
    {
        CameraShaker.Instance.ShakeOnce(2, 2, 0.1f, 0.2f);
    }

    public void HitPlayer(GameObject player)
    {
        PV.RPC("Hit", player.GetComponent<PhotonView>().Owner);
    }*/

    public void GotHit(GameObject collision)
    {

        //print("enter");
        if (collision.tag == "Bullet" || collision.tag == "Bullet2")
        {
            //print("I got hit?");
            if (PV.Owner != collision.GetComponent<PhotonView>().Owner)
            {
                //play particle
                //print("I got hit");
                collision.GetComponent<MeshRenderer>().enabled = false;
                collision.GetComponent<Collider>().enabled = false;
                if (PV.IsMine)
                {
                    CameraShaker.Instance.ShakeOnce(3, 2, 0.1f, 0.2f);
                    if (collision.tag == "Bullet")
                    {
                        PhotonNetwork.Instantiate("HitParticle", collision.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        PhotonNetwork.Instantiate("HitParticle4", collision.transform.position, Quaternion.identity);
                    }
                }
                if (EM.countOfCurrentElectrons > 0)
                {
                    //Vector3 EPos = EM.RemoveElectron();
                    if (PV.IsMine)
                    {
                        PhotonNetwork.Instantiate("Electron", gameObject.transform.position, Quaternion.identity);
                        EM.subtract();
                    }
                }
            }
        }
        
    }
}
