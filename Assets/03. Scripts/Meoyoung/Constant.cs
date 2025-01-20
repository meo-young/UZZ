using UnityEngine;

public static class Constant
{
    [Header("# 꽃 관련 수치")]
    public const float  DEW_MIN_INTERVAL = 600;                 // 최소 수확 시간 (초 단위)
    public const float  FLOWER_EVENT_PROBABILITY = 30;          // 꽃 이벤트 발생 확률 (%)
    public const float  FLOWER_BIGEVENT_PROBABILITY = 50;       // 꽃 빅이벤트 발생 확률 (%)
    public const int    BIGEVENT_LIKEABILITY = 2;               // 꽃 빅이벤트가 주는 호감도
    public const float  BIGEVENT_GROWTH = 100;                  // 꽃 빅이벤트가 주는 성장치
    public const float  MINIEVENT_GROWTH = 100;                 // 꽃 미니이벤트가 주는 성장치
    public const float  MINIEVENT_FINISH_TIME = 15;             // 꽃 미니이벤트 대기시간 (초 단위)
    public const float  FLOWER_MAX_SHAKE_TIME = 5;              // 꽃 최대 흔들기 시간 (초 단위)
    public const float  FLOWER_DEW_ACQUIRE_INTERVAL = 1;        // 꽃 흔들고 있을 때 이펙트 나오는 간격

    [Header("# 작업 관련 수치")]
    public const float  FIELDWORK_REQUIRED_TIME = 10;           // 작업 소요 시간
    public const int    FIELDWORK_LIKEABILITY = 2;              // 작업 끝났을 때 주는 호감도
    public const float  FIELDWORK_HELP_PROBABILITY = 10;        // 작업 도움 확률
    public const float  FIELDWORK_AUTO_PROBABILITY = 5;         // 자동작업 걸릴 확률
    public const float  FIELDWORK_AUTO_CHECK_TIME = 5;          // 자동작업 확인 시간
    public const int    FIELDWORK_WATERING_COOLTIME = 60;       // 물뿌리개 작업 쿨타임
    public const int    FIELDWORK_SPADE_COOLTIME = 180;         // 삽질 작업 쿨타임
    public const int    FIELDWORK_FERTILIZER_COOLTIME = 900;    // 비료주기 작업 쿨타임
    public const int    FIELDWORK_SCISSOR_COOLTIME = 3600;      // 가위 작업 쿨타임
    public const int    FIELDWORK_NUTRITIONAL_COOLTIME = 18000; // 영양제 작업 쿨타임

    [Header("# 선물 관련 수치")]
    public const int    PRESENT_PROBABILITY = 5;                // 선물 걸릴 확률
    public const float  PRESENT_INTERVAL = 120;                 // 선물 확률 체크 간격
    public const float  PRESENT_AFTER_ANIMATION_SECONDS = 5;    // 선물 준 후에 애니메이션 출력 시간

    [Header("# 상호작용 관련 수치")]
    public const int    INTERACTION_LIKEABILITY = 2;            // 푸르와 상호작용시 주는 호감도
    public const float  INTERACTION_LIKEABILITY_COOLTIME = 600; // 상호작용 호감도 쿨타임

    [Header("# 게임시간 관련 수치")]
    public const float  INGAME_CYCLE_TIME = 75;                 // 게임 한사이클 시간
    public const float  INGAME_ONEDAYSECONDS = 86400;           // 인게임 총 시간
    public const float  INGAME_TIME_MULTIFLIER = 4;             // 시간 배속

    [Header("# 뽑기 관련 수치")]
    public const int    DRAW_THEME_COUNT = 1;                   // 뽑기 테마 개수
    public const string DRAW_THEME1_NAME = "푸르의 공방";        // 테마 1번 이름
    public const int    DRAW_ONE_DEW_COST = 5;                  // 1회 뽑기 이슬 비용
    public const int    DRAW_ONE_GLASS_COST = 5;                // 1회 뽑기 유리구슬 비용
    public const int    DRAW_FIVE_DEW_COST = 15;                // 5회 뽑기 이슬 비용
    public const int    DRAW_FIVE_GLASS_COST = 15;              // 5회 뽑기 유리구슬 비용

    [Header("# 푸르 관련 데이터테이블")]
    public const float  PURE_WALK_RANDOM_TIME = 7;              // 푸르 걷기 랜덤 시간
    public const float  PURE_ANIMATION_TIME = 10;               // 푸르 애니메이션 시간
    public const float  PURE_AUTO_TEXT_TIME = 10;               // 푸르 자동 대사 시간
    
}
