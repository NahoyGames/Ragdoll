using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {

    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private GameObject nodePrefab;

    private GameObject hook;
    private List<GameObject> nodes;

    private void Update()
    {
        if (hook != null && nodes.Count < 200)
        {
            MakeNodes();
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            FireHook();
        }
        
    }

    public void FireHook()
    {
        hook = Instantiate(hookPrefab, this.transform.GetChild(1).position, Quaternion.identity);
        hook.GetComponent<Rigidbody>().AddForce((targetMouse.MouseWorldPos() - hook.transform.position).normalized * 50, ForceMode.Impulse);

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
    }
}
