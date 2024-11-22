using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")] 
    public AudioClip[] bgmClips;
    public float bgmVolume;
    public AudioSource bgmPlayer;
    
    [Header("#SFX")] 
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    private AudioSource[] sfxPlayers;
    private int channelIndex;


    [Header("#기타")]
    [SerializeField] Slider sliderBGM;
    [SerializeField] TMP_Text bgmValueText;

    [SerializeField] Slider sliderSFX;
    [SerializeField] TMP_Text sfxValueText;
    public enum Bgm
    {
        morning,
        afternoon,
        evening,
        night,
        store
    }

    public enum Sfx
    {
        Dew,
        Touch
    }
    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        //배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        //bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        
        

        //효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
        
        
        
    }
    
    //배경하나였을때 기준임

    public void PlayBgm(Bgm bgm)
    {


        
        StartCoroutine(FadeIn(bgmPlayer, 2f));
        bgmPlayer.clip = bgmClips[(int)bgm];
        bgmPlayer.Play();
            
        
    }
    public void FadeOutBgm()
    {
        StartCoroutine(FadeOut(bgmPlayer, 2f));
    }

    IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float startTime = Time.time;
        float startVolume = 0f;

        while (Time.time < startTime + duration)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / duration;
            audioSource.volume = Mathf.Lerp(startVolume, bgmVolume, t); // 시작 볼륨에서 1까지 선형 보간

            yield return null;
        }

        // 페이드 인이 완료되면 볼륨을 1로 설정
        audioSource.volume = bgmVolume;
    }

    IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startTime = Time.time;
        float startVolume = audioSource.volume;

        while (Time.time < startTime + duration)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / duration;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t); // 시작 볼륨에서 0까지 선형 보간

            yield return null;
        }

        // 페이드 아웃이 완료되면 볼륨을 0으로 설정하고 오디오를 중지합니다.
        audioSource.volume = 0f;
        
    }




    // 효과음 재생 방법 다른 스크립트에서 AudiManager.instance.Playsfx(AudioManager.sfx.Select);사용
    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
        
    }

    public void SetBgmVolume(float volume)
    {
        bgmVolume = 0.2f * volume;
        bgmPlayer.volume = bgmVolume;
    }
    
    public void SetSfxVolume(float volume)
    {
        sfxVolume = 0.5f * volume;
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    //오디오 컨트롤
    public void AudioControl()
    {
        SetBgmVolume(sliderBGM.value);
        bgmValueText.text = (sliderBGM.value * 100).ToString("F0") + "%";
        SetSfxVolume(sliderSFX.value);
        sfxValueText.text = (sliderSFX.value * 100).ToString("F0") + "%";
    }
}
