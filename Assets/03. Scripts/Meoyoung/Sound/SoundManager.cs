using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager instance;

    private void Awake()
    {
        bgm = GetComponent<BGM>();
        sfx = GetComponent<SFX>();
        if (instance == null)
            instance = this;


        Init();
    }
    #endregion

    [Header("# BGM")]
    [Range(0, 1)] public float bgmVolume;

    private BGM bgm;
    private AudioSource bgmPlayer;

    [Header("# SFX")]
    public int channels;
    [Range(0, 1)] public float sfxVolume;

    private SFX sfx;
    private Queue<AudioSource> sfxQueue;

    #region Initalize
    void Init()
    {
        InitBGMPlayer();
        InitSFXPlayer();
    }

    void InitBGMPlayer()
    {
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.parent = this.transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();

        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.dopplerLevel = 0.0f;
        bgmPlayer.reverbZoneMix = 0.0f;
    }

    void InitSFXPlayer()
    {
        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = this.transform;
        AudioSource[] sfxPlayers = new AudioSource[channels];
        sfxQueue = new Queue<AudioSource>();

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
            sfxPlayers[i].volume = sfxVolume;
            sfxPlayers[i].dopplerLevel = 0.0f;      // 모바일 환경이므로 입체효과 비활성화
            sfxPlayers[i].reverbZoneMix = 0.0f;     // 모바일 환경이므로 동굴과 같은 입체환경 반영 비활성화
            sfxQueue.Enqueue(sfxPlayers[i]);
        }

    }
    #endregion

    #region BGM
    public void PlayBGM<T>(T _bgm) where T : Enum
    {
        if (bgmPlayer == null)
        {
            Debug.Log("bgmPlayer가 초기화 되지 않았습니다");
            return;
        }

        if (typeof(T) == typeof(BGM.Title))
        {
            bgmPlayer.clip = bgm.titleClips[Convert.ToInt32(_bgm)];
        }

        bgmPlayer.Play();
    }

    public void StopBGM()
    {
        if (bgmPlayer == null)
        {
            Debug.Log("bgmPlayer가 초기화 되지 않았습니다");
            return;
        }

        bgmPlayer.Stop();
    }
    #endregion

    #region SFX
    public void PlaySFX<T>(T _sfx) where T : Enum
    {
        if (sfxQueue.Count == 0)
        {
            Debug.LogWarning("사용 가능한 AudioSource가 없습니다.");
            return;
        }

        // SFX 클래스에서 해당 타입의 딕셔너리를 가져옴
        string fieldName = typeof(T).Name + "Dictionary";
        var field = typeof(SFX).GetField(fieldName);
        var dictionary = field.GetValue(sfx) as SerializedDictionary<T, AudioClip>;

        // 사운드 오브젝트풀에서 사용중이지 않은 AudioSource 1개를 꺼냄
        AudioSource player = sfxQueue.Dequeue();

        // 꺼낸 AudioSource에 사운드 할당 후 재생
        player.clip = dictionary[_sfx];
        player.Play();

        // 사운드 재생 후 오브젝트풀에 반환
        StartCoroutine(ReturnToQueueAfterPlay(player));
    }

    private IEnumerator ReturnToQueueAfterPlay(AudioSource player)
    {
        yield return new WaitForSeconds(player.clip.length);

        sfxQueue.Enqueue(player);
    }
    #endregion
}
