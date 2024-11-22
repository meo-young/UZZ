using UnityEngine;

[System.Serializable]
public class RewardTableEntity
{
    public int index;

    public reward_T reward_Type;
    public bool reward_Petal;
    public int reward_Petal_Count;
    public bool reward_Dew;
    public int reward_Dew_Count;
    public bool reward_Beads;
    public int reward_Beads_Count;
   

}

public enum reward_T
{
    rest,mini,goblin,ad
    
}
