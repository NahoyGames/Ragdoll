﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMouse : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private MultipleCollisions col;
    [SerializeField] private float strength;

    private Camera cam;
    private Rigidbody rb;

	void Start ()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {


	}

    public void PhysicsMoveToMouse()
    {
        if (col.HasACollision())
        {
            PhyiscsGoTo(Input.mousePosition, strength);
        }
        else
        {
            PhyiscsGoTo(Input.mousePosition, strength / 3);
        }
    }

    private void PhyiscsGoTo(Vector3 pos, float multiplier)
    {
        // Get the Mouse Position in world space
        pos = cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, -4.45f - cam.transform.position.z));
        pos.z = -4.45f;

        // Get the force to be applied and restrict it
        Vector3 deltaPos = pos - target.position;
        deltaPos = Vector3.ClampMagnitude(deltaPos, 1);
        deltaPos.x /= 2;

        // Strength Multiplier
        multiplier *= Mathf.Pow(deltaPos.magnitude, 2);

        rb.AddForce(deltaPos * multiplier * Time.deltaTime, ForceMode.Impulse);
    }

    public static Vector3 MouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -4.45f - Camera.main.transform.position.z));
        pos.z = -4.45f;

        return pos;
    }
}
