using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{

    [Tooltip("클릭하면 화면에 생성할 오브젝트 프리팹 (각 이미지마다 적절한 프리팹 할당 필요)")]
    [SerializeField] GameObject itemPrefab;

    private GameObject spawnedPrefab;
    private GameObject itemMenuPanel; //아이템 선택시 활성화할 메뉴창
    private RectTransform boundary; // 이미지가 배치될 수 있는 영역
    private Canvas canvas; //프리팹을 적절한 위치에 생성하기 위한 Canvas
    private Transform saveLocation;
    private Transform currentLocation;

    private void Start()
    {
        boundary = MyRoomController.instance.boundaryPanel.GetComponent<RectTransform>();
        canvas = MyRoomController.instance.gameObject.GetComponent<Canvas>();
        itemMenuPanel = MyRoomController.instance.itemMenuPanel;
        saveLocation = MyRoomController.instance.saveLocation.GetComponent<Transform>();
        currentLocation = MyRoomController.instance.currentLocation.GetComponent<Transform>();
    }

    public void PlaceSelfRandom()
    {
        ActiveItemMenuPanel();

        // 프리팹 생성
        if (spawnedPrefab == null)
        {
            spawnedPrefab = Instantiate(itemPrefab, currentLocation);
        }
        else
        {
            Destroy(spawnedPrefab);
            spawnedPrefab = Instantiate(itemPrefab, currentLocation);
        }

        // 프리팹을 RectTransform 안에 맞게 배치
        RectTransform prefabRect = spawnedPrefab.GetComponent<RectTransform>();
        if (prefabRect != null)
        {
            // 프리팹의 크기를 원래 크기로 유지
            prefabRect.sizeDelta = prefabRect.sizeDelta;
            GetRandomPositionWithinBounds(prefabRect);
        }

        MyRoomController.instance.itemPanel.SetActive(false);
    }

    // 바운드 내 랜덤 좌표를 생성하는 함수
    private void GetRandomPositionWithinBounds(RectTransform _prefabRect)
    {
        Vector2 newPosition;

        newPosition.x = Random.Range(-boundary.rect.size.x / 2, boundary.rect.size.x / 2);
        newPosition.y = Random.Range(-boundary.rect.size.y / 2, boundary.rect.size.y / 2);

        _prefabRect.anchoredPosition = newPosition;
    }

    void ActiveItemMenuPanel()
    {
        if (itemMenuPanel == null)
        {
            Debug.Log("ItemMenuPanel을 찾지 못 했습니다.");
            return;
        }
        if (!itemMenuPanel.activeSelf)
            itemMenuPanel.SetActive(true);
    }
}
