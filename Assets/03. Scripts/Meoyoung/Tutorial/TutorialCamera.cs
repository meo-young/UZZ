using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    private Camera cam;
    private Vector3 originPosition;
    private float originSize;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void SetCameraPosition()
    {
        originPosition = this.transform.localPosition;
        originSize = cam.orthographicSize;
        this.transform.localPosition = new Vector3(24.2700005f, -1.25f, -22.4459877f);
        cam.orthographicSize = 7.35f;
    }

    public void InitCamera()
    {
        this.transform.localPosition = originPosition;
        cam.orthographicSize = originSize;
    }
}
