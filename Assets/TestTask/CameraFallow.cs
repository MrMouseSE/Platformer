using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform FallowTransform;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    void FixedUpdate()
    {
        var position = Vector3.Lerp(transform.position, FallowTransform.position, Time.deltaTime*8);
        transform.position = position;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit)) {
                hit.rigidbody.AddForce(hit.transform.rotation.eulerAngles * 15f);
            }
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0 )
        {
            var pos = _camera.transform.localPosition;
            pos.z = Mathf.Clamp(pos.z + Input.GetAxis("Mouse ScrollWheel")*10,-50, -15);
            _camera.transform.localPosition = pos;
        }
        
        if (Input.GetMouseButton(0))
        {
            var rot = transform.rotation.eulerAngles;
            
            var x = Input.GetAxis("Mouse X")*3;
            var y = -Input.GetAxis("Mouse Y")*3;

            var x_rot = Mathf.Clamp(rot.x + y, 0, 40);

            transform.rotation = Quaternion.Euler(new Vector3(x_rot,rot.y+x,rot.z));
        }
    }
}
