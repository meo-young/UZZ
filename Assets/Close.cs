using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    [SerializeField] private GameObject button_close;
    [SerializeField] private GameObject button_open;
    [SerializeField] private GameObject button_work;
    [SerializeField] private GameObject button_bookshelf;
    [SerializeField] private GameObject button_garret;
    [SerializeField] private GameObject button_shop;


    private void Start()
    {
        button_close.SetActive(true);
        button_open.SetActive(false);
        button_work.SetActive(true);
        button_bookshelf.SetActive(true);
        button_garret.SetActive(true);
        button_shop.SetActive(true);

    }

    public void close()
    {
        button_close.SetActive(false);
        button_open.SetActive(true);
        button_work.SetActive(false);
        button_bookshelf.SetActive(false);
        button_garret.SetActive(false);
        button_shop.SetActive(false);
    }

    public void open()
    {
        button_close.SetActive(true);
        button_open.SetActive(false);
        button_work.SetActive(true);
        button_bookshelf.SetActive(true);
        button_garret.SetActive(true);
        button_shop.SetActive(true);
    }
}
