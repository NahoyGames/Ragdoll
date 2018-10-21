using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recording : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 0.05f;
            Cursor.visible = false;
            this.transform.position = new Vector3(transform.position.x, transform.position.y, -20);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1f;
            Cursor.visible = true;
            this.transform.position = new Vector3(transform.position.x, transform.position.y, -50);
        }

    }
}
