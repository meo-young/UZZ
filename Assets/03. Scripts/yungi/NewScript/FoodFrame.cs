using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFrame : MonoBehaviour
{
    public bool correct;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            correct = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            correct = false;
        }
    }
}
