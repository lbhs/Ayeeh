using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public GameObject mask;
    public GameObject image;
    public GameObject PointOne;
    public GameObject PointTwo;
    public int numOfPoints;
    public int CurrentNumOfPoints;
    private Vector2 resolution;
    // Start is called before the first frame update
    void Start()
    {
        resolution = new Vector2(Screen.width, Screen.height);
        CaculatePoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {
            CaculatePoints();
            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            mask.transform.position = mask.transform.position - Vector3.up *20;
            image.transform.position = image.transform.position + Vector3.up * 20;
        }
    }
    private void CaculatePoints()
    {

    }
}
