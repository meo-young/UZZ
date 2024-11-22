using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    private GameObject itemPanel; //메뉴 선택 완료시 활성화할 아이템 창
    private Transform saveLocation;
    private Transform currentLocation;

    void Start()
    {
        saveLocation = MyRoomController.instance.saveLocation.GetComponent<Transform>();
        currentLocation = MyRoomController.instance.currentLocation.GetComponent<Transform>();
        itemPanel = MyRoomController.instance.itemPanel;

        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

    public void DeleteAllItem()
    {
        // 부모 오브젝트에 포함된 자식 객체들을 모두 가져오기
        for (int i = 0; i < saveLocation.childCount; i++)
        {
            GameObject childTransform = saveLocation.GetChild(i).gameObject;
            Debug.Log("자식 객체 이름: " + childTransform.name);
            Destroy(childTransform);
        }

        // 현재 선택된 아이템 객체를 가져옴
        GameObject currentItem = currentLocation.GetChild(0).gameObject;
        Destroy(currentItem);

        GetBackToMain();
    }

    public void DeleteCurrentItem()
    {
        // 현재 선택된 아이템 객체를 가져옴
        GameObject currentItem = currentLocation.GetChild(0).gameObject;
        Destroy(currentItem);

        GetBackToMain();
    }

    public void LockCurrentItem()
    {
        GameObject currentItem = currentLocation.GetChild(0).gameObject;
        currentItem.GetComponent<ItemDrag>().lockState = !currentItem.GetComponent<ItemDrag>().lockState;
    }

    public void FlipCurrentItem()
    {
        // 현재 선택된 아이템 객체를 가져온후 x축 스케일에 -1을 곱해주는 형식
        GameObject currentItem = currentLocation.GetChild(0).gameObject;

        // 아이템이 잠긴 상태라면 작동하지 않음
        bool lockState = currentItem.GetComponent<ItemDrag>().lockState;

        if (lockState)
            return;

        Vector3 localScale = currentItem.transform.localScale;
        localScale.x *= -1;
        currentItem.transform.localScale = localScale;

    }

    public void SaveCurrentItem()
    {
        Transform currentItem = currentLocation.GetChild(0);
        currentItem.SetParent(saveLocation);

        GetBackToMain();
    }

    void GetBackToMain()
    {
        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);

        if (!itemPanel.activeSelf)
            itemPanel.SetActive(true);
    }
}
