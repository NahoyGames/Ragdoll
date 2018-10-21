using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour {

    [SerializeField] private GameObject RocketPrefab;

    [SerializeField] private Transform target;

    public void FireRocket()
    {
        Vector3 dir = (TargetMouse.MouseWorldPos() - transform.position);

        GameObject rocket = Instantiate(RocketPrefab, target.position, Quaternion.Euler(dir));
        rocket.transform.LookAt(TargetMouse.MouseWorldPos());
        rocket.GetComponent<Rigidbody>().velocity = dir.normalized * 25;
    }

}
