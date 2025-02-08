using UnityEngine;

public class DragItem : MonoBehaviour
{
    public string placementId;          // 배치 고유 ID
    public string furnitureInstanceId;  // 가구 인스턴스 ID
    public bool isLocekd = false;

    public Furniture furnitureData;
    public int themeIndex;

    [SerializeField] private GameObject cornerPrefab;  // 모서리 프리팹
    private GameObject[] corners = new GameObject[4];  // 네 모서리를 저장할 배열

    private static DragItem currentSelectedItem;
    private Vector3 originalPosition;
    private bool isDragging = false;

    public void StartDrag()
    {
        originalPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;
    }

    public void ResetPosition()
    {
        if(isDragging)
        {
            transform.position = originalPosition;
            isDragging = false;
        }
    }

    public void Select()
    {
        if(currentSelectedItem != null && currentSelectedItem != this)
            currentSelectedItem.HideCorners();

        currentSelectedItem = this;
        ShowCorners();
    }

    public void Deselect()
    {
        HideCorners();
        currentSelectedItem = null;
    }

    public void ShowCorners()
    {
        if (corners[0] == null)
        {
            CreateCorners();
        }

        foreach (var corner in corners)
        {
            corner.SetActive(true);
        }
        UpdateCornerPositions();
    }

    public void HideCorners()
    {
        if (corners[0] == null) return;

        foreach (var corner in corners)
        {
            corner.SetActive(false);
        }
    }

    private void CreateCorners()
    {
        for (int i = 0; i < 4; i++)
        {
            corners[i] = Instantiate(cornerPrefab, transform);
        }
        UpdateCornerPositions();
    }

    private void UpdateCornerPositions()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 size = spriteRenderer.bounds.size;
        float halfWidth = size.x / 2f;
        float halfHeight = size.y / 2f;

        // 위치와 회전값을 함께 저장하는 배열
        (Vector3 position, float rotation)[] cornerData = new (Vector3, float)[]
        {
        (new Vector3(-halfWidth, halfHeight, 0), 0f),      // 왼쪽 위: 기본 회전
        (new Vector3(halfWidth, halfHeight, 0), 270f),     // 오른쪽 위: 270도
        (new Vector3(-halfWidth, -halfHeight, 0), 90f),    // 왼쪽 아래: 90도
        (new Vector3(halfWidth, -halfHeight, 0), 180f)     // 오른쪽 아래: 180도
        };

        for (int i = 0; i < 4; i++)
        {
            corners[i].transform.localPosition = cornerData[i].position;
            corners[i].transform.localRotation = Quaternion.Euler(0, 0, cornerData[i].rotation);
        }
    }
}