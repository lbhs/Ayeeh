using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTextScript : MonoBehaviour
{
    public Color redColor;
    public Color blueColor;
    private bool done = false;
    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("GameOverObject(Clone)") != null && done == false)
        {
            GetComponent<Text>().text = GameObject.Find("GameOverObject(Clone)").GetComponent<GameOverObjectScript>().winner;
            if(GetComponent<Text>().text == "Blue Wins!")
            {
                GetComponent<Text>().color = blueColor;
            }
            else if (GetComponent<Text>().text == "Red Wins!")
            {
                GetComponent<Text>().color = redColor;
            }
            done = true;
        }
        else
        {
            GetComponent<Text>().text = "Loading...";
        }
    }
}
