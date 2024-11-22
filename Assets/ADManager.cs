using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADManager : MonoBehaviour
{
    [SerializeField] private GameObject uiAD;
    [SerializeField] private GameObject rami;
    [SerializeField] private GameObject rena;
    [SerializeField] private GameObject rani;


    float timer = 0;
    [SerializeField] private GameObject ADObject;
    void Start()
    {
        uiAD.SetActive(false);
        rami.SetActive(false);
        rena.SetActive(false);  
        rani.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=300)
        {
            timer = 0;

        }
    }

    public void OnUIAdvertisement()
    {
        int num = Random.Range(0, 3);

        uiAD.SetActive(true);
        switch (num) 
        {

            case 0:
                rami.SetActive(true); break;
            case 1:
                rena.SetActive(true); break;
            case 2:
                rani.SetActive(true); break;
        }
    }

    public void OffUIAdvertisement()
    {
        uiAD.SetActive(false);
        rami.SetActive(false);
        rena.SetActive(false);
        rani.SetActive(false);
    }
}
