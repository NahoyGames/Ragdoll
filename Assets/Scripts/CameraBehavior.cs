using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    public Transform target;

    public Transform hook;

    private Vector3 zero = Vector3.zero;
    private float fov;

    private void Start()
    {
        fov = Mathf.Cos(Camera.main.fieldOfView) / Mathf.Sin(Camera.main.fieldOfView);
    }

    private void Update()
    {
        float zPos = 50;

        if (hook != null)
        {
            zPos -= (fov * Vector3.Distance(target.position, hook.position)) / 2;
        }

        Vector3 targetPos = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y, -zPos), ref zero, 0.3f);
        transform.position = new Vector3(targetPos.x, Mathf.Clamp(targetPos.y, 1, 100000), targetPos.z);

    }
}
