using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PureMove : MonoBehaviour
{
    public bool isWalkState;

    Vector2 targetPosition;

    [SerializeField] PureController pc;
    [SerializeField] BoxCollider objectCollider;

    void Update()
    {
        if (!isWalkState)
            return;

        // 오브젝트를 목표 위치로 이동
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, pc.pureStat.walkSpeed * Time.deltaTime);

        // 목표 위치에 도착하면 새로운 랜덤 위치 설정
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomPosition();
        }
    }

    public void SetRandomPosition()
    {
        // Box Collider2D의 경계 범위 가져오기
        Bounds areaBounds = pc.walkBound.bounds;
        Vector2 objectSize = objectCollider.bounds.size;

        // x와 y의 이동 가능한 최소, 최대 범위를 계산
        float minX = areaBounds.min.x + objectSize.x / 2;
        float maxX = areaBounds.max.x - objectSize.x / 2;
        float minY = areaBounds.min.y + objectSize.y / 2;
        float maxY = areaBounds.max.y - objectSize.y / 2;

        // 해당 범위 내에서 랜덤 위치 계산
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        // 목표 위치 설정
        targetPosition = new Vector2(randomX, randomY);
    }
}
