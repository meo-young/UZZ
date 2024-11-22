using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinManager : MonoBehaviour
{
    public GameObject[] dokkaebies; // 창가, 다락방, 작업실의 Spawn Points
   
    public UZZ_DataTable uZZ_DataTable; // 보상 테이블

    private void Start()
    {
        // 게임 시작 시, 5분마다 도깨비들을 스폰하는 코루틴을 시작합니다.
        StartCoroutine(SpawnDokkaebiesCoroutine());
    }

    IEnumerator SpawnDokkaebiesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(300f); // 5분(300초) 대기

            // 비활성화된 도깨비들 중에서 랜덤으로 하나를 활성화합니다.
            SpawnDokkaebi();
        }
    }

    void SpawnDokkaebi()
    {
        // 비활성화된 도깨비들을 찾습니다.
        List<GameObject> inactiveDokkaebies = new List<GameObject>();

        foreach (GameObject dokkaebi in dokkaebies)
        {
            if (!dokkaebi.activeSelf) // 비활성화된 도깨비만 리스트에 추가
            {
                inactiveDokkaebies.Add(dokkaebi);
            }
        }

        // 비활성화된 도깨비가 존재할 경우, 랜덤으로 하나를 활성화합니다.
        if (inactiveDokkaebies.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, inactiveDokkaebies.Count);
            inactiveDokkaebies[randomIndex].SetActive(true);
        }
    }

}
