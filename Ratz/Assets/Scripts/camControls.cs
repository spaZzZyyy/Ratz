using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Cam Controls")]
public class camControls : ScriptableObject
{
    public float timeUntilZoomOut;
    public float camZoomInSize;
    public float camZoomOutSize;
    public float camZoomSpeed;
}
