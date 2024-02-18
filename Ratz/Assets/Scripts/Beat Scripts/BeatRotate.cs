using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRotate : MonoBehaviour
{
    public float[] degrees;
    private int i;

    public BeatManager beatManager;
    public float beatCount;
    private float speed;
    private float interval;
    private float degreeDiff;
    private float distance;
    private float diameter; 
    

    void Start() {
        diameter = transform.localScale.x;
    }

    void Update()
    {
        Quaternion target = Quaternion.Euler(0, 0, degrees[i]);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * speed);

        interval = (60f / beatManager._bpm) / beatCount;
        distance = (degreeDiff / 360) * Mathf.PI * diameter;
        speed = distance / interval;
    }

    public void Rotate() {
        i++;
        if(i == degrees.Length) {
            i = 0;
            degreeDiff = Mathf.Abs(degrees[degrees.Length - 1] - degrees[i]);
        } else {
            degreeDiff = Mathf.Abs(degrees[i - 1] - degrees[i]);
        }
    }
}
