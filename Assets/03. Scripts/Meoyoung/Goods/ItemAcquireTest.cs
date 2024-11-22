using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemAcquireTest : MonoBehaviour
{
    public ItemAcquireFx prefabItem;
    public Transform target;
    public Canvas uiCanvas; // UI 캔버스를 연결해야 합니다.
    public List<ItemAcquireFx> items;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RectTransform canvasRect = uiCanvas.GetComponent<RectTransform>();
            // 마우스 클릭 위치를 UI 캔버스 로컬 좌표로 변환
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, uiCanvas.worldCamera, out localPoint);
            int randCount = Random.Range(1, 10);
            for (int i = 0; i < randCount; ++i)
            {
                var itemFx = GameObject.Instantiate<ItemAcquireFx>(prefabItem, this.transform);
                // 변환된 로컬 좌표를 worldPosition으로 변환
                Vector3 worldPosition = uiCanvas.transform.TransformPoint(localPoint);
                itemFx.Explosion(worldPosition, 10.0f);
                items.Add(itemFx);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for(int i=0; i<items.Count; i ++)
                items[i].Move(target.position);
        }
       

    }
}
