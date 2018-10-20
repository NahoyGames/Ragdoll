using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMouse : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private MultipleCollisions col;
    [SerializeField] private float strength;
    [SerializeField] private Camera cam;

    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        if (col.HasACollision())
        {
            PhyiscsGoTo(Input.mousePosition, strength);
        }

	}

    private void PhyiscsGoTo(Vector3 pos, float multiplier)
    {
        // Get the Mouse Position in world space
        pos = cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, -4.45f - cam.transform.position.z));
        pos.z = -4.45f;

        // Get the force to be applied and restrict it
        Vector3 deltaPos = pos - target.position;
        deltaPos = Vector3.ClampMagnitude(deltaPos, 1);

        // Strength Multiplier
        multiplier *= Mathf.Pow(deltaPos.magnitude, 2);

        rb.AddForce(deltaPos * multiplier * Time.deltaTime, ForceMode.Impulse);
    }

    public static Vector3 MouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -4.45f - Camera.main.transform.position.z));
        pos.z = -4.45f;

        return pos;
    }
}
