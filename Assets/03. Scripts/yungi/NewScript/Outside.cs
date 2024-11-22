using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outside : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;
    private void OnMouseDown()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == true)
            return;

        cameraController.goGarret_Outside();
        
    }
}
