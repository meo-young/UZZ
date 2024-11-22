using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkNoticeManager : MonoBehaviour
{
    [SerializeField] private WorkManager workManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void touchWorkNotice()
    {

        workManager.onWorkUI();
    }
}
