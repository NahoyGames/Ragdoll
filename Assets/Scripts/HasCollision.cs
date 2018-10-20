using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasCollision : MonoBehaviour {

    bool isColliding = false;

    public bool IsColliding
    {
        get
        {
            return isColliding;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isColliding |= collision.gameObject.tag == "Environment";
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            isColliding = false;
        }
    }

}
