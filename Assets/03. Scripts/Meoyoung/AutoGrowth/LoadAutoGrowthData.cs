using System.IO;
using UnityEngine;

public struct AutoGrowTable
{
    public float growthProduced;
    public float outGrowthProduced;
    public int price;
    public int imageIndex;
}

public class LoadAutoGrowthData : MonoBehaviour
{
    [SerializeField] TextAsset autoGrowDataTable;
    [HideInInspector] public AutoGrowTable[] autoGrowData;


    private void Awake()
    {
        InitAutoGrowData();
        UpdateAutoWorkData();
    }


    void InitAutoGrowData()
    {
        autoGrowData = new AutoGrowTable[100];
    }

    public void UpdateAutoWorkData()
    {
        StringReader reader = new StringReader(autoGrowDataTable.text);
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

            autoGrowData[int.Parse(values[0]) - 1].growthProduced = float.Parse(values[1]);
            autoGrowData[int.Parse(values[0]) - 1].outGrowthProduced = float.Parse(values[2]);
            autoGrowData[int.Parse(values[0]) - 1].price = int.Parse(values[3]);
            autoGrowData[int.Parse(values[0]) - 1].imageIndex = int.Parse(values[4]);

        }
    }
}
