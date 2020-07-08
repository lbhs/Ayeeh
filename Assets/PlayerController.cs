using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 16;
    private float speedstore;
    public GameObject cam;
    [HideInInspector]
    public GameObject Camera;
    private CharacterController controller;
    private Vector3 targetDirection;
    public float gravityScale = 0.0025f;
    public PhotonView PV;
    public ElectronManager EM;
    public bool CanBullet1 = false;
    public bool CanBullet2 = false;
    public int TeamNumber;
    public Material TeamOneMaterial;
    public Material TeamTwoMaterial;
    private TimerControler TC;
    public Text UserNameText;
    public Text UserNameScore;
    public GameObject GameOverUI;
    private bool mobileSupport;

    public LeftJoystick LJ;
    // Start is called before the first frame update
    void Start()
    {
        mobileSupport = true;
        speedstore = speed;
        PV = GetComponent<PhotonView>();
        TC = GameObject.Find("GameCanvas").transform.GetChild(0).GetComponent<TimerControler>();
        if (PV.Owner.NickName != "")
        {
            UserNameText.text = PV.Owner.NickName;
        }
        else if(PV.IsMine)
        {
            UserNameText.text = "Player " + Random.Range(0, 1000);
            PV.RPC("SetNameRPC", RpcTarget.All, UserNameText.text);
        }

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
        //TC.addToTotal(5);
        LJ = GameObject.Find("Left Joystick").GetComponent<LeftJoystick>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        if(EM.countOfCurrentElectrons == 0)
        {
            Instantiate(GameOverUI, transform.position, Quaternion.identity);
            PhotonNetwork.Instantiate("Explode", transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
        }
        // rb.AddForce(new Vector3(Input.GetAxis("Vertical") * speed,0, Input.GetAxis("Horizontal") * speed));
        //rb.AddForce(Camera.main.transform.forward* Input.GetAxis("Vertical")*speed);
        //rb.AddForce(Camera.main.transform.forward * Input.GetAxis("Horizontal") * speed);
        float ystore = targetDirection.y;
        //targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (mobileSupport)
        {
            targetDirection = new Vector3(LJ.GetInputDirection().x, 0f, LJ.GetInputDirection().y);
        }
        else
        {
            targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        }
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
                    if (CanBullet1 == true && !Input.GetKey(KeyCode.Space))
                    {
                        GameObject b = PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity);
                        b.transform.GetChild(0).GetComponent<BulletScript>().hitPointRPC(hit.point);
                        
                        CanBullet1 = false;
                        CanBullet2 = false;
                        foreach (var item in co)
                        {
                            StopCoroutine(item);
                        }
                        co.Add(StartCoroutine(check()));
                    }
                    else if (CanBullet2 == true && Input.GetKey(KeyCode.Space))
                    {
                        GameObject b = PhotonNetwork.Instantiate("Bullet2", transform.position, Quaternion.identity);
                        b.transform.GetChild(0).GetComponent<BulletScript>().hitPointRPC(hit.point);

                        Camera.GetComponent<CameraFollow>().RemoveALightning();
                        Camera.GetComponent<CameraFollow>().LightningAnim.SetTrigger("Shake");
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
        bool yes = false;
        foreach (var item in Camera.GetComponent<CameraFollow>().lightningScripts)
        {
            if (item.CurrentNumOfPoints == 0)
            {
                yes = true;
            }
        }
        if (yes == true)
        {
            CanBullet2 = true;
        }
        else
        {
            CanBullet2 = false;
        }

    }

    public List<Coroutine> co = new List<Coroutine>();
    IEnumerator check()
    {
        speed = speed / 2f;
        yield return new WaitForSeconds(0.1f);
        CanBullet1 = true;

        yield return new WaitForSeconds(0.1f);
        speed = speedstore;
        //yield return new WaitForSeconds(1.6f);
        //CanBullet2 = true;
    }

    [PunRPC]
    private void SetNameRPC(string name)
    {
        UserNameText.text = name;
    }
    
    public void SetTeam(int team)
    {
        PV.RPC("SetTeamRPC", RpcTarget.AllBuffered, team);
    }
    [PunRPC]
    void SetTeamRPC(int team)
    {
        TeamNumber = team;
        if (team == 1)
        {
            GetComponent<MeshRenderer>().material = TeamOneMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = TeamTwoMaterial;
        }
    }

    public void updateScore(bool remove)
    {
        if(TeamNumber == 1)
        {
            TC.addEToBlue(remove);
        }
        else
        {
            TC.addEToRed(remove);
        }
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
                    CameraShaker.Instance.ShakeOnce(4, 2, 0.1f, 0.2f);
                    if (collision.tag == "Bullet")
                    {
                        PhotonNetwork.Instantiate("HitParticle", collision.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        PhotonNetwork.Instantiate("HitParticle4", collision.transform.position, Quaternion.identity);
                    }
                }
                if (EM.countOfCurrentElectrons > 2)
                {
                    //Vector3 EPos = EM.RemoveElectron();
                    if (PV.IsMine)
                    {
                        PhotonNetwork.Instantiate("Electron", gameObject.transform.position, Quaternion.identity);
                        EM.subtract();
                    }
                }
                else if(EM.countOfCurrentElectrons <= 2 && EM.countOfCurrentElectrons >0 && collision.tag == "Bullet2")
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
