using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private Canvas canvas;

    Vector2 parentSize, objectSize;
    private float minX, maxX, minY, maxY;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Start() {
        parentSize = parentRectTransform.rect.size;
        objectSize = rectTransform.rect.size;

        minX = -parentSize.x / 2 + objectSize.x / 2;
        maxX = parentSize.x / 2 - objectSize.x / 2;
        minY = -parentSize.y / 2 + objectSize.y / 2;
        maxY = parentSize.y / 2 - objectSize.y / 2;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 마우스/터치 위치와 오브젝트 위치의 차이를 저장
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,                  // 대상 UI 요소의 RectTransform
            eventData.position,             // 변환할 스크린 좌표
            eventData.pressEventCamera,     // UI를 렌더링 하는 카메라
            out Vector2 localPoint);        // 결과값 (로컬 좌표)
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 현재 마우스/터치 위치를 로컬 좌표로 변환
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint))
        {

            // 최소, 최대 범위 적용
            localPoint.x = Mathf.Clamp(localPoint.x, minX, maxX);
            localPoint.y = Mathf.Clamp(localPoint.y, minY, maxY);

            // offset을 적용하여 위치 업데이트
            rectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 시 필요한 처리가 있다면 여기에 구현
    }
}