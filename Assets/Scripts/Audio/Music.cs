using UnityEngine;

public class Music : MonoBehaviour
{
    public enum MusicContext
    {
        None,
        Home,
        Stage,
        Boss
    }

    [Header("Music Context")]
    public MusicContext playOnStart = MusicContext.None;

    [Header("Music Lists")]
    public AudioClip[] homeMusic;
    public AudioClip[] stageMusic;
    public AudioClip[] bossMusic;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        switch (playOnStart)
        {
            case MusicContext.Home: PlayHome(); break;
            case MusicContext.Stage: PlayStage(); break;
            case MusicContext.Boss: PlayBoss(); break;
        }
    }

    public void PlayHome() => PlayFrom(homeMusic);
    public void PlayStage() => PlayFrom(stageMusic);
    public void PlayBoss() => PlayFrom(bossMusic);

    // pause button
    public void StopMusic()
    {
        if (_audioSource.isPlaying)
            _audioSource.Stop();
    }

    private void PlayFrom(AudioClip[] _clips)
    {
        if (_clips == null || _clips.Length == 0)
        {
            Debug.LogWarning($"[Music] The selected music list is empty.", this);
            return;
        }

        int _index = (_clips.Length == 1) ? 0 : Random.Range(0, _clips.Length);

        _audioSource.clip = _clips[_index];
        _audioSource.Play();
    }
}