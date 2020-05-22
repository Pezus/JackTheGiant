using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

    public static MusicController instance;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        MakeSingleton();

        audioSource = GetComponent<AudioSource>();
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusic(bool play)
    {
        if (play)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
        /*       if (play)
               {
                   if (!audioSource.isPlaying)
                   {
                       audioSource.Play();
                   }
                   else
                   {
                       if (audioSource.isPlaying)
                       {
                           audioSource.Stop();
                       }
                   }
               }*/
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
