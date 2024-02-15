using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRotate : MonoBehaviour
{
    //TODO change speed and distance to private  
    private int startingDegree = 0;
    public float[] degrees;
    private int i;
    private int j;
    Quaternion target;

    public BeatManager beatManager;
    public float beatCount;
    public float speed;
    //*testing
    public int testSpeed;
    private float interval;
    public float distance;

    void Start()
    {
        target = Quaternion.Euler(0, 0, degrees[startingDegree]);
    }

    
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, testSpeed * Time.deltaTime);
        interval = (60f / beatManager._bpm) / beatCount;
        speed = distance / interval;
    }

    public void Rotate() {
        i++;
        if(i == degrees.Length) {
            i = 0;
        }

        if((i + 1) == degrees.Length) {
            j = 0;
        } else {
            j = i + 1;
        }
        distance = Mathf.Abs(degrees[i] - degrees[j]);
    }
}
