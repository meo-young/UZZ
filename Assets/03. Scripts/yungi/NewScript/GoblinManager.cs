using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinManager : MonoBehaviour
{
    public GameObject[] dokkaebies; // â��, �ٶ���, �۾����� Spawn Points
   
    public UZZ_DataTable uZZ_DataTable; // ���� ���̺�

    private void Start()
    {
        // ���� ���� ��, 5�и��� ��������� �����ϴ� �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(SpawnDokkaebiesCoroutine());
    }

    IEnumerator SpawnDokkaebiesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(300f); // 5��(300��) ���

            // ��Ȱ��ȭ�� ������� �߿��� �������� �ϳ��� Ȱ��ȭ�մϴ�.
            SpawnDokkaebi();
        }
    }

    void SpawnDokkaebi()
    {
        // ��Ȱ��ȭ�� ��������� ã���ϴ�.
        List<GameObject> inactiveDokkaebies = new List<GameObject>();

        foreach (GameObject dokkaebi in dokkaebies)
        {
            if (!dokkaebi.activeSelf) // ��Ȱ��ȭ�� ������ ����Ʈ�� �߰�
            {
                inactiveDokkaebies.Add(dokkaebi);
            }
        }

        // ��Ȱ��ȭ�� ������ ������ ���, �������� �ϳ��� Ȱ��ȭ�մϴ�.
        if (inactiveDokkaebies.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, inactiveDokkaebies.Count);
            inactiveDokkaebies[randomIndex].SetActive(true);
        }
    }

}
