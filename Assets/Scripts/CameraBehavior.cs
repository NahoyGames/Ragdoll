using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    public Transform target;

    private Vector3 zero = Vector3.zero;

    private void Update()
    {
        Vector3 targetPos = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), ref zero, 0.3f);
        transform.position = new Vector3(targetPos.x, Mathf.Clamp(targetPos.y, 1, 100000), targetPos.z);
    }
}
