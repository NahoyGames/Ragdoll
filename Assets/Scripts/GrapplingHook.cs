using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class GrapplingHook : MonoBehaviour {

    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private Transform target;
    private Animator activateAnim;
    private Image rocketActive;
    private Image active;

    private GameObject hook;
    private SpringJoint hookJoint;
    private HasCollision hookCollision;

    private void Start()
    {
        activateAnim = GameObject.Find("Canvas").transform.Find("hookActivate").GetComponent<Animator>();
        rocketActive = GameObject.Find("Canvas").transform.Find("rocketCancel").GetComponent<Image>();
        active = GameObject.Find("Canvas").transform.Find("HookFill").GetComponent<Image>();
    }

    private void Update()
    {
        // TODO temporary hook fire input


        // Hook update
        if (hook != null)
        {
            UpdateHook();
        }
    }

    public void FireHook()
    {
        rocketActive.enabled = true;
        active.enabled = false;
        activateAnim.SetTrigger("AbilityActivated");
        hook = Instantiate(hookPrefab, this.transform.GetChild(1).position, Quaternion.identity);
        hook.GetComponent<Rigidbody>().AddForce((TargetMouse.MouseWorldPos() - hook.transform.position).normalized * 15, ForceMode.Impulse);
        hookCollision = hook.GetComponent<HasCollision>();
    }

    public void DestroyHook()
    {
        active.enabled = true;
        rocketActive.enabled = false;
        Destroy(hook);
    }

    public bool HasHook
    {
        get
        {
            return hook != null;
        }
    }

    private void FinishHook(Transform t)
    {
        hookJoint = hook.AddComponent<SpringJoint>();

        hookJoint.connectedBody = this.GetComponent<Rigidbody>();
        hookJoint.autoConfigureConnectedAnchor = false;
        hookJoint.spring = 1000;
        hookJoint.damper = 100;

        hook.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        if (t == null)
        {
            hook.transform.SetParent(hookCollision.LastCollidedObject.transform);
        } else
        {
            hook.transform.SetParent(t);
        }
    }

    private void UpdateHook()
    {
        if (hookJoint != null) // Pull the hook up
        {
            Vector3 anchor = hookJoint.connectedAnchor;
            hookJoint.connectedAnchor = anchor.normalized * Mathf.Clamp((float)(anchor.magnitude - 1), 3, 1000);
        }
        else if (hookCollision.IsColliding) // Hook Collision
        {
            FinishHook(null);
        }
        else // Hook in-between Collision
        {
            RaycastHit hit;
            if (Physics.Raycast(target.position, hook.transform.position - target.position, out hit))
            {
                if (hit.collider.tag == "Environment")
                {
                    hook.transform.position = hit.point;

                    FinishHook(hit.transform);
                }
            }
        }

        // Line Renderer
        hook.GetComponent<LineRenderer>().SetPositions(new Vector3[] {
                this.transform.GetChild(1).position,
                hook.transform.position
            });
    }
}
