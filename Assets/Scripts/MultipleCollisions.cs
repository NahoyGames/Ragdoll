using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleCollisions : MonoBehaviour {

    public HasCollision[] colliders;

    public bool HasACollision()
    {
        foreach (HasCollision c in colliders)
        {
            if (c.IsColliding)
            {
                return true;
            }
        }

        return false;
    }

}
