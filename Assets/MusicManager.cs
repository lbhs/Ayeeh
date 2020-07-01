using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static bool MusicManagerExits;
    public List<AudioClip> Music = new List<AudioClip>();
    private AudioSource ASource;
    // Start is called before the first frame update
    void Start()
    {
        if (!MusicManagerExits)
        {
            MusicManagerExits = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        ASource = GetComponent<AudioSource>();
        int num = Random.Range(0, Music.Count);
        ASource.clip = Music[num];
        ASource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            Destroy(gameObject);
        }
    }
}
