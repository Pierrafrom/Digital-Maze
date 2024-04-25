using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    
    void Start()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
        
    }

    
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            audioSource.clip = audioClips[0];
        }
        else
        {
            audioSource.clip = audioClips[1];
        }
        if(!audioSource.isPlaying)
            audioSource.Play();
        
    }
}
