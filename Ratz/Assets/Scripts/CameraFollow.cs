using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float ease = .25f;
    [SerializeField] Camera cam;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        cam.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, ease);
    }
}
