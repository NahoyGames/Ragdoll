using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    public Color[] colors;

    private void Start()
    {
        MeshRenderer[] mats = GetComponentsInChildren<MeshRenderer>();

        Color c = colors[Random.Range(0, colors.Length - 1)];

        foreach (MeshRenderer m in mats)
        {
            m.material.color = c;
        }
    }

}
