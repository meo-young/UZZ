using UnityEngine;

public class StorySound : MonoBehaviour
{
    public static StorySound instance;

    [Header("# Audio Source")]
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource seAudioSource;

    [Header("# Audio Clip")]
    [SerializeField] AudioClip[] bgmClips;
    [SerializeField] AudioClip[] sfxClips;
    [SerializeField] AudioClip[] seClips;


    private void Awake()
    {
        if(instance == null)
            instance = this;

        InitAudioSource();
    }

    #region Initialize
    void InitAudioSource()
    {
        InitBGMAudioSource();
        InitSFXAudioSource();
        InitSEAudioSource();
    }

    void InitBGMAudioSource()
    {
        bgmAudioSource.reverbZoneMix = 0f;
        bgmAudioSource.dopplerLevel = 0f;
    }

    void InitSFXAudioSource()
    {
        sfxAudioSource.reverbZoneMix = 0f;
        sfxAudioSource.dopplerLevel = 0f;
    }

    void InitSEAudioSource()
    {
        seAudioSource.reverbZoneMix = 0f;
        seAudioSource.dopplerLevel = 0f;
    }
    #endregion
    public void PlayBGM(int _index)
    {
        if (_index < 0)
            return;

        bgmAudioSource.clip = bgmClips[_index];
        bgmAudioSource.Play();
    }

    public void PlaySFX(int _index)
    {
        if (_index < 0)
            return;

        sfxAudioSource.clip = sfxClips[_index];
        sfxAudioSource.Play();
    }

    public void PlaySE(int _index)
    {
        if (_index < 0)
            return;

        seAudioSource.clip = seClips[_index];
        seAudioSource.Play();
    }
}
