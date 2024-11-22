using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestNoticeManager : MonoBehaviour
{
    [SerializeField] private GardenerManager gardenerManager;
    [SerializeField] private RestManager restManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void touchRestNotice()
    {
        //restManager.set_RestSystem(gardenerManager);
       
        gardenerManager.touchGardener();
    }
}
