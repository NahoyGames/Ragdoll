using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AutoFocus : MonoBehaviour {

    [SerializeField] private Transform target;

    DepthOfField dof;

    private void Start()
    {
        // Get the Post Processing Volume
        PostProcessVolume ppv = GetComponent<PostProcessVolume>();

        // Get Depth of Field
        ppv.profile.TryGetSettings(out dof);
    }

    private void Update()
    {
        dof.focusDistance.value = (target.position - this.transform.position).magnitude;
    }

}
