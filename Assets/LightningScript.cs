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
    private List<float> yValues = new List<float>();
    private float intervals;

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
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        if (CurrentNumOfPoints < numOfPoints)
        {
            CurrentNumOfPoints++;
            mask.transform.position = mask.transform.position - Vector3.down * intervals;
            image.transform.position = image.transform.position + Vector3.down * intervals;
        }
    }

    private void MoveDown()
    {
        if (CurrentNumOfPoints > 0)
        {
            CurrentNumOfPoints--;
            mask.transform.position = mask.transform.position - Vector3.up * intervals;
            image.transform.position = image.transform.position + Vector3.up * intervals;
        }
    }

    private void CaculatePoints()
    {
        yValues.Clear();
        float distance = PointOne.transform.position.y - PointTwo.transform.position.y;
        intervals = distance / numOfPoints;
        yValues.Add(PointTwo.transform.position.y + intervals);
        for (int i = 1; i < numOfPoints; i++)
        {
            yValues.Add(yValues[yValues.Count - 1] + intervals);
        }
    }
}
