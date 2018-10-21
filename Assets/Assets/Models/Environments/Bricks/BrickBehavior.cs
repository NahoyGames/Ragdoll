using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour {

    public Color[] brickColors;
    
	void Start () {

        this.GetComponent<MeshRenderer>().material.color = brickColors[Random.Range(0, brickColors.Length - 1)];

	}
}
