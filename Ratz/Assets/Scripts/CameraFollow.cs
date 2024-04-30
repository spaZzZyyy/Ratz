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

    [SerializeField] public Transform target;
    public Transform player;
    public Transform[] points;
    public int i;
    public float zoomSize;
    public float playerSize;
    private float currSize;

    void Start() {
        i = 0;
        player = target;
        currSize = playerSize;
        cam.orthographicSize = playerSize;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        cam.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, ease);
        if(player != target) {
            if(currSize < zoomSize) {
                currSize = currSize + 0.05f;
                cam.orthographicSize = currSize;
            }
            
        } else {
            if(currSize > playerSize) {
                currSize = currSize - 0.08f;
                cam.orthographicSize = currSize;
            }
            
        }
    }
    
}
