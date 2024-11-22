using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIManager2 : MonoBehaviour
{
    public GameObject UI_rest_panel, UI_rest_background, UI_rest_background2;
    public GameObject UI_work_panel, UI_work_background, UI_work_background2;
    public GameObject UI_memo_panel;

    public void OnUIRestPanel()
    {
        UI_rest_panel.SetActive(true);
    }

    public void OnUIWorkPanel()
    {
        UI_work_panel.SetActive(true);
    }
    public void OffUIRestPanel()
    {
        UI_rest_panel.SetActive(false);
    }

    public void OffUIWorkPanel()
    {
        UI_work_panel.SetActive(false);
    }
    public void OnUIRestBackground()
    {
        UI_rest_background.SetActive(true);
    }
    public void OnUIRestBackground2()
    {
        UI_rest_background2.SetActive(true);
    }
    public void OnUIWorkBackground()
    {
        UI_work_background.SetActive(true);
    }
    public void OnUIWorkBackground2()
    {
        UI_work_background2.SetActive(true);
    }
    public void OffUIRestBackground()
    {
        UI_rest_background.SetActive(false);
    }
    
    public void OffUIRestBackground2()
    {
        UI_rest_background2.SetActive(false);
    }

    public void OffUIWorkBackground()
    {
        UI_work_background.SetActive(false);
    }
    public void OffUIWorkBackground2()
    {
        UI_work_background2.SetActive(false);
    }

    public void BackSpace()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            EventSystem eventSystem = EventSystem.current;

            // 마우스 위치에 Raycast
            PointerEventData eventData = new PointerEventData(eventSystem);
            eventData.position = mousePosition;
            
            List<RaycastResult> results = new List<RaycastResult>();
            eventSystem.RaycastAll(eventData,results);
            if (results.Count>0)
            {
                // 클릭된 UI가 있을 경우
                GameObject clickedObject = results[0].gameObject;
                Debug.Log("UI Clicked: " + clickedObject.name);
                
                if (clickedObject == UI_rest_background2)
                    UI_rest_background2.SetActive(false);
                if (clickedObject == UI_work_background2)
                    UI_work_background2.SetActive(false);
            }
            else
            {
                // 클릭된 UI가 없을 경우
                Debug.Log("UI Not Clicked");
            }
           
        }
    }
    
    public void OnUIMemoPanel()
    {
        UI_memo_panel.SetActive(true);
    }

    public void OffUIMemoPanel()
    {
        UI_memo_panel.SetActive(false);
    }
    
    
    
}
