using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadPresentDatatable : MonoBehaviour
{
    [SerializeField] TextAsset presentDatatable;
    [SerializeField] Sprite[] presentImages;

    [HideInInspector] public List<PresentData> presentDatas;

    private void Awake()
    {
        presentDatas = new List<PresentData>();

        UpdatePresentDatatable();
    }
    void UpdatePresentDatatable()
    {
        StringReader reader= new StringReader(presentDatatable.text);
        bool head = false;

        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (!head)
            {
                head = true;
                continue;
            }

            string[] values = line.Split('\t');

            int imageIndex = (int)(PresentType)Enum.Parse(typeof(PresentType), values[1]);
            string presentName = values[2];
            string flavorText = values[3];
            string presentText = values[4];

            PresentData presentData = new PresentData(imageIndex, presentName, flavorText, presentText);
            presentDatas.Add(presentData);
        }
    }

    public Sprite GetPresentSprite(int index)
    {
        return presentImages[presentDatas[index].GetImageIndex()];
    }

    private enum PresentType
    {
        g_rock = 0,
        g_socks = 1,
        g_galpi = 2,
        g_sonsu = 3,
        g_mug = 4,
        g_banch = 5,
        g_sajun = 6,
        g_tulsil = 7,
        g_suchup = 8,
        g_ggot = 9
    }

    public class PresentData
    {
        private int imageIndex;
        private string name;
        private string flavorText;
        private string text;
        public PresentData(int imageIndex, string name, string flavorText, string text)
        {
            this.imageIndex = imageIndex;
            this.name = name;
            this.flavorText = flavorText;
            this.text = text;
        }

        public int GetImageIndex()
        {
            return imageIndex;
        }

        public string GetName()
        {
            return name;
        }

        public string GetFlavorText()
        {
            return flavorText;
        }

        public string GetText()
        {
            return text;
        }
    }
}
