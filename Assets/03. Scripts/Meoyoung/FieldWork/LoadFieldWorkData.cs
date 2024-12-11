using System.IO;
using UnityEngine;

public class LoadFieldWorkData : MonoBehaviour
{
    public static LoadFieldWorkData instance;

    [SerializeField] TextAsset[] fieldWorkTable;
    public FieldWork[][] fieldWorkData;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        InitVariable();
        UpdateFieldWorkData();
    }

    void InitVariable()
    {
        fieldWorkData = new FieldWork[5][];
        for(int i=0; i<fieldWorkData.Length; i++)
        {
            fieldWorkData[i] = new FieldWork[60];
            for (int j = 0; j < fieldWorkData[i].Length; j++)
                fieldWorkData[i][j] = new FieldWork();
        }
    }

    void UpdateFieldWorkData()
    {
        for(int i=0; i<fieldWorkTable.Length; i++)
        {
            StringReader reader = new StringReader(fieldWorkTable[i].text);
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

                fieldWorkData[i][int.Parse(values[0]) - 1].type = (FieldWorkType)i;
                fieldWorkData[i][int.Parse(values[0]) - 1].helpText = values[2];
                fieldWorkData[i][int.Parse(values[0]) - 1].helpSuccessText = values[3];
                fieldWorkData[i][int.Parse(values[0]) - 1].step = int.Parse(values[4]);
                fieldWorkData[i][int.Parse(values[0]) - 1].level = int.Parse(values[5]);
                fieldWorkData[i][int.Parse(values[0]) - 1].growPoint = float.Parse(values[6]);
                fieldWorkData[i][int.Parse(values[0]) - 1].dewPoint = float.Parse(values[7]);
                fieldWorkData[i][int.Parse(values[0]) - 1].price = int.Parse(values[8]);
                fieldWorkData[i][int.Parse(values[0]) - 1].icon = int.Parse(values[9]);
                fieldWorkData[i][int.Parse(values[0]) - 1].anim = int.Parse(values[10]);
                fieldWorkData[i][int.Parse(values[0]) - 1].helpAnim = int.Parse(values[11]);
            }
        }
    }
}

#region Enum, Class
[System.Serializable]
public class FieldWorkInfo
{
    public int[] level;
    public float[] coolTimeList;

    public FieldWorkInfo()
    {
        level = new int[5];
        coolTimeList = new float[5];
    }
}

[System.Serializable]
public enum FieldWorkType
{
    Watering = 0,
    Spade = 1,
    Fertilizer = 2,
    Scissor = 3,
    Nutritional = 4,
    None = 5
}

public enum ResultType
{
    Growth = 0,
    Dew = 1
}
[System.Serializable]
public class FieldWork
{
    public FieldWorkType type;
    public ResultType result;
    public int step, level;
    public int coolTime;
    public string helpText, helpSuccessText;
    public float growPoint, dewPoint;
    public int price;
    public int icon;
    public int anim, helpAnim;
    public bool available = false;
}
#endregion
