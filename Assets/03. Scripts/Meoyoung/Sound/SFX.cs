using UnityEngine;
using AYellowpaper.SerializedCollections;

public class SFX : MonoBehaviour
{
    public static SFX instance;

    [Header("# SFX Clips")]
    public SerializedDictionary<PureSound, AudioClip> PureSoundDictionary = new();
    public SerializedDictionary<Ambience, AudioClip> AmbienceDictionary = new();
    public SerializedDictionary<Flower, AudioClip> FlowerDictionary = new();
    public SerializedDictionary<Dew, AudioClip> DewDictionary = new();
    public SerializedDictionary<Diary, AudioClip> DiaryDictionary = new();

    [System.Serializable]
    public enum PureSound       // Ǫ���� ���õ� ȿ���� ����
    {
        // Pure
        IDLE = 0,           // �⺻
        SLEEP = 1,          // ����
        WALK = 2,           // �ȱ�
        TELEPORT = 3,       // �ڷ���Ʈ
        DOWN = 4,           // ������
        SING = 5,           // �뷡�θ���
        WATCH = 6,          // �� �Ĵٺ���
        WINDOW = 7,         // â�� �� �ٶ󺸱�
        STARR = 8,          // �տ� ���� �Ĵٺ���
        HI = 9,             // �λ�
        MUNG = 10,          // �۶�����
        SLIME = 11,         // ������ ���� ġ��
        DANCE = 12,         // ���߱�
        FLY = 13,           // �սǵս� ���ٴϱ�
        BHI = 14,           // ���� �λ�
        GROOMING = 15,      // �׷��
        BUBI = 16,          // �κ�κ�
        LIGHT = 17,         // �տ��� �� ??...
        HAND = 18,          // ����ڰ� Ǫ���� ��� ����� ��
        SCREEN = 19,        // ???
        WATERING = 20,      // �۾� : ���Ѹ���
        SPADE = 21,         // �۾� : ��
        FERTILIZER = 22,    // �۾� : ���
        SCISSOR = 23,       // �۾� : ����
        NUTRITIONAL = 24,   // �۾� : ������
        READYGIFT = 25,     // ���� �غ�
        GIVEGIFT = 26,      // ���� �ֱ�
        BIGEVENT = 27,      // �� �� �̺�Ʈ
        MINIEVENT = 28,     // �� �̴� �̺�Ʈ
        TOUCH = 29,         // Ǫ�� ��ġ
    }       
    public enum Ambience
    {
        TOUCH = 0,          // GUI ��ġ
        TEXT = 1,           // UI, �ؽ�Ʈ ��½�
        TROUBLE = 2,        // �۾�����
        SOLVE = 3,          // �۾����� �ذ�
        LIKE = 4,           // ȣ����
        LEVELUP = 5         // Ǫ�� ������
    }

    public enum Flower
    {  
        TOUCH = 0,          // �� ��ġ
        GLOW = 1,           // �� ��Ȯ
        STEPUP = 2,         // ���ܾ�
        LEVELUP = 3,        // ������
        GROW = 4,           // ����ġ ȹ��
        DUST = 5            // ���� �̺�Ʈ ���
    }

    public enum Dew
    {
        DROP = 0            // �̽� ȹ��
    }

    public enum Diary
    {
        TAP = 0,
        BUTTON = 1
    }
}
