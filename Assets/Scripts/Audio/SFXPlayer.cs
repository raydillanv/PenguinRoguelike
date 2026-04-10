using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Single

    public void PlaySFX(AudioClip _clip)
    {
        _audioSource.PlayOneShot(_clip);
    }

    public void StopSFX()
    {
        _audioSource.Stop();
    }

    // Multiple (simultaneous)

    public void PlayMultiple(AudioClip _clipA, AudioClip _clipB)
    {
        _audioSource.PlayOneShot(_clipA);

        AudioSource _secondSource = gameObject.AddComponent<AudioSource>();
        _secondSource.playOnAwake = false;
        _secondSource.PlayOneShot(_clipB);

        StartCoroutine(RemoveSourceWhenDone(_secondSource, _clipB.length));
    }

    // At Position

    public void PlayAtTransform(AudioClip _clip, Transform _target)
    {
        AudioSource.PlayClipAtPoint(_clip, _target.position);
    }


    private System.Collections.IEnumerator RemoveSourceWhenDone(AudioSource _source, float _duration)
    {
        yield return new WaitForSeconds(_duration);
        Destroy(_source);
    }
}