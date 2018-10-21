using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour {

    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private GameObject rocketTrail;
    private Animator activateAnim;

    [SerializeField] private Transform target;

    private void Start()
    {
        activateAnim = GameObject.Find("Canvas").transform.Find("rocketActivate").GetComponent<Animator>();
    }

    public void FireRocket()
    {
        activateAnim.SetTrigger("AbilityActivated");

        Vector3 dir = (TargetMouse.MouseWorldPos() - transform.position);

        GameObject rocket = Instantiate(rocketPrefab, target.position, Quaternion.Euler(dir));
        rocket.transform.LookAt(TargetMouse.MouseWorldPos());
        rocket.GetComponent<Rigidbody>().velocity = dir.normalized * 25;

        Instantiate(rocketTrail, transform.position, rocket.transform.rotation);
    }

}
