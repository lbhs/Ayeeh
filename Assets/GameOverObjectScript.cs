using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }
}
