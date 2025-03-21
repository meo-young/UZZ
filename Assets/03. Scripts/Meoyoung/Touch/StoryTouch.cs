using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTouch : MonoBehaviour
{
    public GameObject defaultTouchVFX;

    private GameObject firstTouchedObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector3 effectPos = new(touchPosition.x, touchPosition.y, touchPosition.z + 10);
            if (touch.phase == TouchPhase.Began)
            {
                Physics.Raycast(touchPosition, transform.forward, out RaycastHit hit, 100f);
                if (hit.collider != null)
                {
                    firstTouchedObject = hit.transform.gameObject;
                    Debug.Log(firstTouchedObject.gameObject.tag);
                }

                Instantiate(defaultTouchVFX, effectPos, Quaternion.identity);
            }
        }
    }
}
