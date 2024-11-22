using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    
    public int reward_Petal_Count;
  
    public int reward_Dew_Count;
      
    public int reward_Beads_Count;

    public void getReward()
    {
        GameManager.Instance.petal += reward_Petal_Count;
        GameManager.Instance.dew += reward_Dew_Count;
        GameManager.Instance.bead += reward_Beads_Count;
    }
}    
