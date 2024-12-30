using System.IO;
using UnityEngine;

public class LoadDiaryDatatable : MonoBehaviour
{
    [SerializeField] TextAsset storyDataTable;
    [HideInInspector] public StoryData[] storyData;

    private void Awake()
    {
        UpdateDiaryDataTable();
    }

    void UpdateDiaryDataTable()
    {
        storyData = new StoryData[100];
        StringReader reader = new StringReader(storyDataTable.text);
        bool head = false;
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (!head)
            {
                head = true;
                continue;
            }
            string[] values = line.Split('\t');
            storyData[int.Parse(values[0]) - 1].title = values[1];
            storyData[int.Parse(values[0]) - 1].imageIndex = int.Parse(values[2]);
            storyData[int.Parse(values[0]) - 1].description = values[3];
            storyData[int.Parse(values[0]) - 1].requiredDew = int.Parse(values[4]);
            storyData[int.Parse(values[0]) - 1].result = int.Parse(values[5]);
        }
    }
}

public struct StoryData            // 스토리 데이터테이블
{
    public string title;                // 스토리 제목
    public int imageIndex;              // 스토리 미리보기 이미지
    public string description;          // 스토리 설명 텍스트
    public int requiredDew;             // 스토리 필요재화
    public int result;                  // 스토리 보상호감도
}

