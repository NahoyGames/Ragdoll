﻿using System.Collections;
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

    GameObject lastCollidedObject;
    public GameObject LastCollidedObject
    {
        get
        {
            return lastCollidedObject;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isColliding |= collision.gameObject.tag == "Environment";
        lastCollidedObject = collision.collider.gameObject;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            isColliding = false;
        }
    }

}
