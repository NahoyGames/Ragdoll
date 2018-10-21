using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour {

    [SerializeField] private GameObject[] thursters;

    private Image cooldownUI;
    private Image cancelUI;

    float fuel;
    bool overheated = false;

    Rigidbody torso;

    private void Start()
    {
        torso = transform.GetComponentInParent<Rigidbody>();
        cooldownUI = GameObject.Find("Canvas").transform.Find("Jetpackfill").GetComponent<Image>();
        cancelUI = GameObject.Find("Canvas").transform.Find("jetpackCancel").GetComponent<Image>();
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

    public bool Active
    {
        get
        {
            return false;
        }
        set
        {
            if (value)
            {
                thursters[0].SetActive(true);
                thursters[1].SetActive(true);
            }
            else
            {
                thursters[0].SetActive(false);
                thursters[1].SetActive(false);
            }
        }
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
