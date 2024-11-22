using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AutoGrowInfo
{
    public int level;
}

public class AutoGrowManager : MonoBehaviour
{
    struct AutoGrowTable
    {
        public int level;
        public float growthProduced;
        public float outGrowthProduced;
    }

    [SerializeField] TextAsset autoGrowDataTable;

    public void UpdateFieldWorkData()
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
        }
    }
}
