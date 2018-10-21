using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour {

    float fuel;

    Rigidbody torso;

    private void Start()
    {
        torso = transform.GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        fuel = Mathf.Clamp(fuel + (Time.deltaTime * 5), 0, 100);
    }

    public void FireJetpack()
    {
        if (fuel <= 10) { return; }

        torso.AddForce((transform.forward + ((TargetMouse.MouseWorldPos() - torso.position).normalized * 2f)) * 67500 * Time.deltaTime, ForceMode.Force);
        fuel = Mathf.Clamp(fuel - (Time.deltaTime * 10), 0, 100);

    }

}
