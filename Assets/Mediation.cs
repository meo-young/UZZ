using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mediation : MonoBehaviour
{
    [SerializeField] private GardenerManager gardenerManager;
    
    //public void connectGardener()
    //{
    //    gardenerManager.touchGardener();
    //}

    private void OnMouseDown()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == true)
            return;

        
        gardenerManager.touchGardener();
    }

}
