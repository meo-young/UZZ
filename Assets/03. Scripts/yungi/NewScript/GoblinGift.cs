using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinGift : MonoBehaviour
{
    private void OnMouseDown()
    {
        OnGiftTouched();
    }

   
    public void OnGiftTouched()
    {
        GameManager.Instance.RewardManager.
            RewardRandom(reward_T.goblin);



        Destroy(gameObject);
    }
}
