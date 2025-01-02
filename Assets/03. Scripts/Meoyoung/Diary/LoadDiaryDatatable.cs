using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadDiaryDatatable : MonoBehaviour
{
    [SerializeField] TextAsset storyDataTable;
    [HideInInspector] public List<StoryData> storyDatas;

    private void Awake()
    {
        storyDatas = new List<StoryData>();

        UpdateDiaryDataTable();
    }

    void UpdateDiaryDataTable()
    {
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

            string storyTitle = values[1];
            int storyImageIndex = int.Parse(values[2]);
            string storyDescription = values[3];
            int storyRequiredDew = int.Parse(values[4]);
            int storyResult = int.Parse(values[5]);

            StoryData storyData = new StoryData(storyTitle, storyImageIndex, storyDescription, storyRequiredDew, storyResult);
            storyDatas.Add(storyData);
        }
    }
    public class StoryData            // 스토리 데이터테이블
    {
        private string title;                // 스토리 제목
        private int imageIndex;              // 스토리 미리보기 이미지
        private string description;          // 스토리 설명 텍스트
        private int requiredDew;             // 스토리 필요재화
        private int result;                  // 스토리 보상호감도

        public StoryData(string title, int imageIndex, string description, int requiredDew, int result)
        {
            this.title = title;
            this.imageIndex = imageIndex;
            this.description = description;
            this.requiredDew = requiredDew;
            this.result = result;
        }

        public string GetTitle()
        {
            return title;
        }

        public int GetImageIndex()
        {
            return imageIndex;
        }

        public string GetDescription()
        {
            return description;
        }

        public int GetRequiredDew()
        {
            return requiredDew;
        }

        public int GetResult()
        {
            return result;
        }
    }
}



