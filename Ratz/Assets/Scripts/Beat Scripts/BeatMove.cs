using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMove : MonoBehaviour
{
    private int startingPoint = 0;
    public Transform[] points;
    private int i;

    public BeatManager beatManager;
    public float beatCount;
    private float speed;
    private float interval;
    private float distance;

    void Start() {
        transform.position = points[startingPoint].position;
    }
    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        
        interval = (60f / beatManager._bpm) / beatCount;
        speed = distance / interval;
    }
   

    public void Move() {
        i++;
        if(i == points.Length) {
            i = 0;
            distance = (points[points.Length - 1].position - points[i].position).magnitude;
        } else {
            distance = (points[i - 1].position - points[i].position).magnitude;
        }
    }    
}
