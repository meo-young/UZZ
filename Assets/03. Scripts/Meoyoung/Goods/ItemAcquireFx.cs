using UnityEngine;
using DG.Tweening;

public class ItemAcquireFx : MonoBehaviour
{
    public Transform centerTransform; // 회전 중심이 될 Transform
    public float rotationSpeed = 50f; // 회전 속도
    public float radius = 1f; // 회전 반경

    private Vector2 startPosition;

    private bool isMoving = false;
    private int randomDir;
    private void Awake()
    {
        centerTransform = GameObject.FindWithTag("CenterPos").GetComponent<Transform>();
    }

    void Start()
    {
        // 시작 위치를 중심 기준 반경 위치로 설정
        Vector2 offset = (transform.position - centerTransform.position).normalized * radius;
        startPosition = (Vector2)centerTransform.position + offset;
        transform.position = startPosition;
        randomDir = Random.Range(0, 2);
    }

    void Update()
    {
        if (!isMoving)
            return;

        // centerTransform을 기준으로 회전
        if(randomDir == 0)
            transform.RotateAround(centerTransform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        else
            transform.RotateAround(centerTransform.position, Vector3.down, rotationSpeed * Time.deltaTime);

    }
    public void Explosion(Vector2 from, float explo_range)
    {
        isMoving = true;
        transform.position = from;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(from + Random.insideUnitCircle * explo_range, 0.1f).SetEase(Ease.OutCubic));
    }

    public void Move(Vector2 to)
    {
        isMoving = false;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(to, 1.5f).SetEase(Ease.InCubic));
        sequence.AppendCallback(() => { gameObject.SetActive(false); });
    }
}