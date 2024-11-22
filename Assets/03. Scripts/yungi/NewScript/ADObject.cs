using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADObject : MonoBehaviour
{
    [SerializeField] private RewardManager rewardManager;
    [SerializeField] private int ADType;
    private void OnMouseDown()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == true)
            return;

        rewardManager.RewardRandom(reward_T.ad,ADType);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
