using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private Jetpack jetpack;
    RocketLauncher rLauncher;

    TargetMouse targetMouse;
    GrapplingHook gHook;

    bool isLocal = true;

    private void Start()
    {
        Camera.main.gameObject.GetComponent<CameraBehavior>().target = this.transform.parent.Find("Waist").transform;
        targetMouse = GetComponent<TargetMouse>();
        gHook = GetComponent<GrapplingHook>();
        rLauncher = GetComponent<RocketLauncher>();

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

        if (Input.GetButtonUp("Fire1"))
        {
            //gHook.DestroyHook();
        }

        if (Input.GetButtonDown("Fire2") && !hasHook)
        {
            rLauncher.FireRocket();
        }

        if (Input.GetButton("Jump"))
        {
            jetpack.FireJetpack();
            jetpack.Active = true;
        }
        else
        {
            jetpack.Active = false;
        }

        if (!hasHook)
        {
            targetMouse.PhysicsMoveToMouse();
        }
    }


}
