using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public GameObject explosion;
    public float radius;
    public float force;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, this.transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);

        foreach (Collider c in colliders)
        {
            Debug.Log(c.name);

            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(force, this.transform.position, radius);
            }
        }

        Destroy(this.gameObject);
    }
}
