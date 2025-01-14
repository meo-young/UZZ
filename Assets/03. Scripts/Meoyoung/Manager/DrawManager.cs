using System.IO;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public struct DrawData
    {
        public string title;                // 테마 이름
        public Furniture[] furnitures;      // 테마에 속한 가구 정보
        public int length;                // 테마에 속한 각 가구들 개수
    }

    public struct Furniture
    {
        public string theme;                // 테마 이름
        public string name;                 // 가구 이름
        public int index;                   // 가구 번호
        public string flavorText;           // 가구의 FlavorText
        public int rank;                    // 가구 등급
        public int layer;                   // 가구 레이어   
        public string icon;                 // 가구 아이콘
        public string image;                // 가구 이미지
        public float probability;           // 가구가 뜰 확률
        public int power;                   // 가구 팔았을 때 받는 가루
        public int requiredPower;           // 가구 조합시 필요한 가루
    }

    [Header("# Draw Info")]
    [SerializeField] TextAsset drawDataTable;
    public DrawData[] drawdatas;

    private void Awake()
    {
        UpdateDrawDataTable();
    }

    void UpdateDrawDataTable()
    {
        InitDrawDatas();
        StringReader reader = new StringReader(drawDataTable.text);
        bool head = false;
        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (!head)
            {
                head = true;
                continue;
            }

            string[] value = line.Split('\t');

            drawdatas[int.Parse(value[0])].title = value[1];
            drawdatas[int.Parse(value[0])].furnitures[int.Parse(value[2])].theme = value[1];
            drawdatas[int.Parse(value[0])].furnitures[int.Parse(value[2])].index = int.Parse(value[2]);
            drawdatas[int.Parse(value[0])].furnitures[int.Parse(value[2])].name = value[3];
            drawdatas[int.Parse(value[0])].furnitures[int.Parse(value[2])].flavorText = value[4];
            drawdatas[int.Parse(value[0])].furnitures[int.Parse(value[2])].rank = int.Parse(value[5]);
            drawdatas[int.Parse(value[0])].furnitures[int.Parse(value[2])].layer = int.Parse(value[6]);
            drawdatas[int.Parse(value[0])].furnitures[int.Parse(value[2])].icon = value[7];                     // string -> int 로 수정필요
            drawdatas[int.Parse(value[0])].furnitures[int.Parse(value[2])].image = value[8];                    // string -> int 로 수정필요
            drawdatas[int.Parse(value[0])].furnitures[int.Parse(value[2])].probability = float.Parse(value[9]);
            drawdatas[int.Parse(value[0])].furnitures[int.Parse(value[2])].power = int.Parse(value[10]);
            drawdatas[int.Parse(value[0])].furnitures[int.Parse(value[2])].requiredPower = int.Parse(value[11]);
            drawdatas[int.Parse(value[0])].length++;
        }
    }

    void InitDrawDatas()
    {
        drawdatas = new DrawData[10];
        for(int i=0; i<drawdatas.Length; i++)
        {
            drawdatas[i].furnitures = new Furniture[20];
        }
    }
}
