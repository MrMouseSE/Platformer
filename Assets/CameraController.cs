using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cam;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                transform.position = hit.point;
            }
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0 )
        {
            var pos = cam.localPosition;
            pos.z = Mathf.Clamp(pos.z + Input.GetAxis("Mouse ScrollWheel"), -15,0);
            cam.localPosition = pos;
        }
        
        if (Input.GetMouseButton(0))
        {
            var rot = transform.rotation.eulerAngles;
            
            var x = Input.GetAxis("Mouse X");
            var y = -Input.GetAxis("Mouse Y");

            var x_rot = Mathf.Clamp(rot.x + y, 0, 40);

            transform.rotation = Quaternion.Euler(new Vector3(x_rot,rot.y+x,rot.z));
        }
    }
}