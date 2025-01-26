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
            sfxPlayers[i].dopplerLevel = 0.0f;      // ����� ȯ���̹Ƿ� ��üȿ�� ��Ȱ��ȭ
            sfxPlayers[i].reverbZoneMix = 0.0f;     // ����� ȯ���̹Ƿ� ������ ���� ��üȯ�� �ݿ� ��Ȱ��ȭ
            sfxQueue.Enqueue(sfxPlayers[i]);
        }

    }
    #endregion

    #region BGM
    public void PlayBGM<T>(T _bgm) where T : Enum
    {
        if (bgmPlayer == null)
        {
            Debug.Log("bgmPlayer�� �ʱ�ȭ ���� �ʾҽ��ϴ�");
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
            Debug.Log("bgmPlayer�� �ʱ�ȭ ���� �ʾҽ��ϴ�");
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
            Debug.LogWarning("��� ������ AudioSource�� �����ϴ�.");
            return;
        }

        // SFX Ŭ�������� �ش� Ÿ���� ��ųʸ��� ������
        string fieldName = typeof(T).Name + "Dictionary";
        var field = typeof(SFX).GetField(fieldName);
        var dictionary = field.GetValue(sfx) as SerializedDictionary<T, AudioClip>;

        // ���� ������ƮǮ���� ��������� ���� AudioSource 1���� ����
        AudioSource player = sfxQueue.Dequeue();

        // ���� AudioSource�� ���� �Ҵ� �� ���
        player.clip = dictionary[_sfx];
        player.Play();

        // ���� ��� �� ������ƮǮ�� ��ȯ
        StartCoroutine(ReturnToQueueAfterPlay(player));
    }

    private IEnumerator ReturnToQueueAfterPlay(AudioSource player)
    {
        yield return new WaitForSeconds(player.clip.length);

        sfxQueue.Enqueue(player);
    }
    #endregion
}
