using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPosition.z = 0;
        transform.position = mPosition;
    }
}
