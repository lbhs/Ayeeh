using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverObjectScript : MonoBehaviour
{
    public string winner;

    private static bool GameOverObjectScriptExits;
    void Awake()
    {
        if (!GameOverObjectScriptExits)
        {
            GameOverObjectScriptExits = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
