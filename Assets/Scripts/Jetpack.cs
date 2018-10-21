using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour {

    [SerializeField] private Image cooldownUI;
    [SerializeField] private Image cancelUI;

    float fuel;
    bool overheated = false;

    Rigidbody torso;

    private void Start()
    {
        torso = transform.GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        fuel = Mathf.Clamp(fuel + (Time.deltaTime * 30), 0, 100);

        if (fuel >= 99)
        {
            overheated = false;
        }

        UpdateUI();
    }

    public void FireJetpack()
    {
        if (fuel <= 10 || overheated)
        {
            overheated = true;
            return;
        }

        torso.AddForce((transform.forward + ((TargetMouse.MouseWorldPos() - torso.position).normalized * 2f)) * 12500 * Time.deltaTime, ForceMode.Force);
        fuel = Mathf.Clamp(fuel - (Time.deltaTime * 60), 0, 100);

    }

    private void UpdateUI()
    {
        cooldownUI.rectTransform.localScale = new Vector3(
            0.5f,
            (0.5f * ((fuel - 10) / 90)),
            0.5f
        );

        if (overheated)
        {
            cancelUI.enabled = true;
        }
        else
        {
            cancelUI.enabled = false;

        }
    }

}
