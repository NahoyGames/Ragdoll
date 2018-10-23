using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class GrapplingHook : MonoBehaviour {

    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private Transform target; // Mouse pos
    private Animator activateAnim;
    private Image rocketActive;
    private Image active;

    private GameObject hook;
    private SpringJoint hookJoint;
    private HasCollision hookCollision;

    private GameObject hookTarget; // Gameobject the hook hit
    private List<Transform> hookPositions; // List of all the hook gameobjects making the rope "bend"

    private CameraBehavior cam;

    private void Start()
    {
        activateAnim = GameObject.Find("Canvas").transform.Find("hookActivate").GetComponent<Animator>();
        rocketActive = GameObject.Find("Canvas").transform.Find("rocketCancel").GetComponent<Image>();
        active = GameObject.Find("Canvas").transform.Find("HookFill").GetComponent<Image>();
        cam = Camera.main.GetComponent<CameraBehavior>();
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
        hookPositions = new List<Transform> { this.transform.GetChild(1), hook.transform };
        cam.hook = hook.transform;
    }

    public void DestroyHook()
    {
        active.enabled = true;
        rocketActive.enabled = false;
        cam.hook = null;
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
            hookTarget = hookCollision.LastCollidedObject;
        } else
        {
            hook.transform.SetParent(t);
            hookTarget = t.gameObject;
        }

        cam.hook = null;
    }

    private void UpdateHook()
    {
        if (hookJoint != null) // Pull the hook up
        {
            Vector3 anchor = hookJoint.connectedAnchor;
            hookJoint.connectedAnchor = anchor.normalized * Mathf.Clamp((float)(anchor.magnitude - 1.5), 3, 1000);
            UpdateFinishedHook();
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
        Vector3[] pos = new Vector3[hookPositions.Count];
        for (int t = 0; t < hookPositions.Count; t++)
        {
            pos[t] = hookPositions[t].position;
        }

        hook.GetComponent<LineRenderer>().SetPositions(pos);
    }

    private void UpdateFinishedHook()
    {
        RaycastHit hit;
        if (Physics.Raycast(target.position, hook.transform.position - target.position, out hit))
        {
            if (hit.collider.tag == "Environment" && hit.collider.gameObject != hookTarget && !hookPositions.Contains(hit.collider.gameObject.transform))
            {
                // Make the rope "bend"
                hookPositions.Add(hit.collider.gameObject.transform);
            }
        }


    }
}
