using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
    
}
