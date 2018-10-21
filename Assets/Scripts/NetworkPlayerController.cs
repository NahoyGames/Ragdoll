using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerController : NetworkBehaviour {

    [SerializeField] private Jetpack jetpack;
    RocketLauncher rLauncher;

    TargetMouse targetMouse;
    GrapplingHook gHook;

    bool isLocal = true;

    private void Start()
    {
        if (!this.transform.GetComponentInParent<NetworkIdentity>().isLocalPlayer)
        {
            isLocal = false;
            return;
        }

        Camera.main.gameObject.GetComponent<CameraBehavior>().target = this.transform.parent.Find("Waist").transform;
        targetMouse = GetComponent<TargetMouse>();
        gHook = GetComponent<GrapplingHook>();
        rLauncher = GetComponent<RocketLauncher>();

    }

    private void Update()
    {
        if (!isLocal) { return; }

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
