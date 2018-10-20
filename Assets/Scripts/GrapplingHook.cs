using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {

    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private GameObject nodePrefab;

    private GameObject hook;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (hook != null)
            {
                Destroy(hook);
            }
            else
            {
                hook = Instantiate(hookPrefab, this.transform.GetChild(1).position, Quaternion.identity);
                hook.GetComponent<Rigidbody>().velocity = (targetMouse.MouseWorldPos() - hook.transform.position).normalized * 50;
            }
        }

        if (hook != null)
        {
            if (hook.GetComponent<SpringJoint>() == null && hook.GetComponent<HasCollision>().IsColliding)
            {
                SpringJoint joint = hook.AddComponent<SpringJoint>();

                joint.connectedBody = this.GetComponent<Rigidbody>();
                joint.autoConfigureConnectedAnchor = false;
                joint.spring = 1000;

                hook.GetComponent<Rigidbody>().isKinematic = true;
            }
            else if (hook.GetComponent<SpringJoint>() != null)
            {
                Vector3 anchor = hook.GetComponent<SpringJoint>().connectedAnchor;
                hook.GetComponent<SpringJoint>().connectedAnchor = anchor.normalized * Mathf.Clamp((float)(anchor.magnitude - 0.2), 3, 1000);
            }

            hook.GetComponent<LineRenderer>().SetPositions(new Vector3[] {
                this.transform.GetChild(1).position,
                hook.transform.position
            });
        }
    }

    /*private List<GameObject> nodes;
    private bool hookGotCollision = false;

    private void Update()
    {
        if (hook != null)
        {
            if (!hookGotCollision && nodes.Count < 200)
            {
                MakeNodes();
            }
            else if (hook.GetComponent<HasCollision>().IsColliding)
            {
                hookGotCollision = true;
            }

        }
        else if (Input.GetButtonDown("Fire1"))
        {
            FireHook();
        }
        
    }

    public void FireHook()
    {
        hook = Instantiate(hookPrefab, this.transform.GetChild(1).position, Quaternion.identity);
        hook.GetComponent<Rigidbody>().velocity = (targetMouse.MouseWorldPos() - hook.transform.position).normalized * 50;

        nodes = new List<GameObject>
        {
            hook
        };
    }

    private void MakeNodes()
    {
        GameObject lastNode = nodes[nodes.Count - 1];
        GameObject currentNode = lastNode;

        // Get the number of nodes to create between the last node and the hook
        int nodesToCreate = (int)(Vector3.Distance(lastNode.transform.position, this.transform.GetChild(1).position) / 0.5);

        Debug.Log(nodesToCreate);

        for (int i = 0; i < nodesToCreate; i++)
        {
            SpringJoint lastNodeJoint = currentNode.AddComponent<SpringJoint>();

            currentNode = Instantiate(nodePrefab, Vector3.Lerp(lastNode.transform.position, this.transform.GetChild(1).position, 1 / (i + 1)), Quaternion.identity);


            // Set the joint's anchor
            lastNodeJoint.autoConfigureConnectedAnchor = true;
            lastNodeJoint.connectedBody = currentNode.GetComponent<Rigidbody>();
            lastNodeJoint.autoConfigureConnectedAnchor = false;

            // Joint settings
            lastNodeJoint.spring = 100000;
            lastNodeJoint.damper = 100000;

            nodes.Add(currentNode);
        }
    }*/
}
