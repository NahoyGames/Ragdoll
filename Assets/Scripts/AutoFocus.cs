using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEditor;

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

    [ContextMenu("Update Depth of Field")]
    public void UpdateDof()
    {
        PostProcessVolume ppv = GetComponent<PostProcessVolume>();
        ppv.profile.TryGetSettings(out dof);
        dof.focusDistance.value = (target.position - this.transform.position).magnitude;
    }

}
