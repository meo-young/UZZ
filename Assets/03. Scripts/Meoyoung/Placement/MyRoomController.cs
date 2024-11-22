using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// !!) 사용자가 가지고 있는 아이템을 하단 아이템 목록에 불러오는 과정 필요
// 아이템을 불러올 때, 각각의 아이템에 OnClick 이벤트 부착해줄 것 -> OnClick 이벤트가 부착된 Prefab을 Instantiate 하면 됨
/// <summary>
/// 1. 하단 아이템 목록에서 아이템 클릭하면 임의의 위치로 아이템이 배치
/// 2. 배치된 아이템을 드래그하면 원하는 위치에 아이템 배치 가능
///     - 드래그 할 때 일정 영역을 벗어나면 넘어가지 못 하도록 예외 처리 필요
/// 3. 체크 버튼을 누르면 해당 위치로 아이템 위치 고정
///     - 체크 버튼을 누르지 않고 아이템이 아닌 화면의 영역을 클릭한 경우 해당 아이템 배치 초기화
/// </summary>
public class MyRoomController : MonoBehaviour
{
    public static MyRoomController instance;

    [Header("# Panel")]
    [Tooltip("아이템 이미지들을 포함하고 있는 패널 Item Panel")]
    public GameObject itemPanel;

    [Tooltip("아이템들이 배치될 영역의 패널 Placement Boundary Panel")]
    public GameObject boundaryPanel;

    [Tooltip("아이템 선택 후 출력될 메뉴 패널 Item Menu Panel")]
    public GameObject itemMenuPanel;

    [Tooltip("저장된 아이템이 들어갈 오브젝트 Saved Object List")]
    public GameObject saveLocation;

    [Tooltip("현재 배치중인 아이템이 들어갈 오브젝트 Current Object")]
    public GameObject currentLocation;

    [Tooltip("뒷배경 패널 Background Panel")]
    public GameObject backgroundPanel;



    private void Awake()
    {
        instance = this;
    }

}
