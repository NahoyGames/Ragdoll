using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    TargetMouse targetMouse;
    GrapplingHook gHook;

    private void Start()
    {
        targetMouse = GetComponent<TargetMouse>();
        gHook = GetComponent<GrapplingHook>();
    }

    private void Update()
    {
        bool hasHook = gHook.HasHook;

        if (Input.GetButtonDown("Fire1"))
        {
            if (hasHook)
            {
                gHook.DestroyHook();
            }
            else
            {
                gHook.FireHook();
            }
        }

        if (!hasHook)
        {
            targetMouse.PhysicsMoveToMouse();
        }
    }
}
