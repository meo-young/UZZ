using Spine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition; // 원래 위치 -> 혹시 모를 추가 구현을 위해 선언. 아직 활용하진 않음
    private Vector2 lastMousePosition; // 이전 드래그 위치 저장
    private RectTransform backgroundPanel;
    private RectTransform boundary;  // 드래그할 수 있는 영역
    private PanelInfo panelInfo;
    private Transform currentItemLocation;
    private GameObject itemMenuPanel;
    private GameObject itemPanel;

    [HideInInspector] public bool lockState;

    private void Start()
    {
        boundary = MyRoomController.instance.boundaryPanel.GetComponent<RectTransform>();
        backgroundPanel = MyRoomController.instance.backgroundPanel.GetComponent<RectTransform>();
        currentItemLocation = MyRoomController.instance.currentLocation.GetComponent<Transform>();
        itemMenuPanel = MyRoomController.instance.itemMenuPanel;
        itemPanel = MyRoomController.instance.itemPanel;

        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        panelInfo = new PanelInfo();

        lockState = false;
        Debug.Log("ItemDrag Start");
        CalculateCornerPosition();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.gameObject.transform.SetParent(currentItemLocation);
        MoveToItemMenuPanel();
    }

    #region MouseHandler

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 원래 위치 저장
        originalPosition = rectTransform.anchoredPosition;
        lastMousePosition = eventData.position; // 마우스의 시작 위치 저장
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!lockState)
            CalculateBoundary(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 시 추가 작업을 수행
    }

    #endregion


    void MoveToItemMenuPanel()
    {
        if (!itemMenuPanel.activeSelf)
            itemMenuPanel.SetActive(true);

        if(itemPanel.activeSelf)
            itemPanel.SetActive(false);
    }

    void CalculateBoundary(PointerEventData _eventData)
    {
        Vector2 newPosition = rectTransform.anchoredPosition + _eventData.delta;

        //마우스 좌표가 PlacementBoundaryPanel의 좌표를 벗어났을 때, PlacementBoundaryPanel의 경계의 좌표로 아이템의 좌표를 설정
        if (panelInfo.Left > _eventData.position.x)
        {
            newPosition.x = -boundary.rect.size.x / 2;
        }

        if (panelInfo.Right < _eventData.position.x)
        {
            newPosition.x = boundary.rect.size.x / 2;
        }

        if (panelInfo.Top < _eventData.position.y)
        {
            newPosition.y = boundary.rect.size.y / 2;
        }

        if (panelInfo.Bottom > _eventData.position.y)
        {
            newPosition.y = -boundary.rect.size.y / 2;
        }

        // 새로 계산된 위치를 적용
        rectTransform.anchoredPosition = newPosition;
    }

    /// <summary>
    /// BackgroundPanel의 왼쪽 하단 좌표를 기준으로 PlacementBoundayPanel의 Left, Right, Top, Bottom의 좌표를 계산
    /// </summary>
    void CalculateCornerPosition()
    {
        // 흰색 패널의 좌측, 우측, 위, 아래의 절대 위치
        Vector3[] whiteCorners = new Vector3[4];
        backgroundPanel.GetWorldCorners(whiteCorners);

        // 핑크색 패널의 좌측, 우측, 위, 아래의 절대 위치
        Vector3[] pinkCorners = new Vector3[4];
        boundary.GetWorldCorners(pinkCorners);

        // 각 거리 계산
        panelInfo.Left = pinkCorners[0].x - whiteCorners[0].x;
        panelInfo.Right = pinkCorners[3].x;
        panelInfo.Top = pinkCorners[1].y;
        panelInfo.Bottom = pinkCorners[0].y - whiteCorners[0].y;

        // 결과 출력
        /*Debug.Log("왼쪽 거리: " + panelInfo.Left);
        Debug.Log("오른쪽 거리: " + panelInfo.Right);
        Debug.Log("위쪽 거리: " + panelInfo.Top);
        Debug.Log("아래쪽 거리: " + panelInfo.Bottom);*/
    }
}

public class PanelInfo
{
    public float Top;
    public float Bottom;
    public float Left;
    public float Right;
}
