using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxContainer : MonoBehaviour
{
    [SerializeField] List<TrailRenderer> renderers;
    public void ClearChildren()
    {
        foreach(TrailRenderer rend in renderers)
        {
            rend.Clear();
        }
    }
}
