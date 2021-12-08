using UnityEngine;

public class CameraRotateAround : MonoBehaviour
{
    public Transform cam;
    public float sens;
    public float zoomSens;

    float distansZoom;

    private Quaternion _newRotation;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Zoom();
        }
        else if (distansZoom != 0)
        {
            distansZoom = 0;
        }

        if (Input.touchCount == 1)
        {
            Swipe();
        }

        if (transform.rotation != _newRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,_newRotation, Time.deltaTime);
        }
    }

    void Zoom()
    {
        if (distansZoom == 0)
        {
            distansZoom = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        }

        float delta = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) - distansZoom;

        Vector3 pos = cam.localPosition;
        pos.z = Mathf.Clamp(pos.z + delta * zoomSens, -150, -60);
        cam.localPosition = pos;
        distansZoom = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
    }

    void Swipe()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        float x = Input.GetTouch(0).deltaPosition.x * sens;
        float y = Input.GetTouch(0).deltaPosition.y * sens;

        float x_rot = rot.x - y;
        x_rot = Mathf.Clamp(x_rot, 7, 90);

        _newRotation = Quaternion.Euler(new Vector3(x_rot, rot.y + x, rot.z));
    }
}