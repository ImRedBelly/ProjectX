using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;


    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    public void StopAudio(AudioClip audioClip)
    {
        audioSource.Stop();
    }
    

}