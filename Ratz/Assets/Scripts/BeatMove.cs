using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMove : MonoBehaviour
{
    //TODO change i, j, speed, and distance to private
    private int startingPoint = 0;
    public Transform[] points;
    public int i;

    public BeatManager beatManager;
    public float beatCount;
    public float speed;
    private float interval;
    public float distance;
    //*checking for two point problem
    public float checking;

    void Start() {
        transform.position = points[startingPoint].position;
        //* checking for two point problem
        checking = (points[1].position - points[2].position).magnitude;
        print("length is " + points.Length);
    }
    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        interval = (60f / beatManager._bpm) / beatCount;
        speed = distance / interval;
    }
   
    //! doesn't work when more then two points

    //TODO fix for multiple points
    public void Move() {
        i++;
        if(i == points.Length) {
            i = 0;
            distance = (points[points.Length].position - points[i].position).magnitude;
        } else {
            distance = (points[i - 1].position-points[i].position).magnitude;
        }

        // if((i + 1) == points.Length) {
        //     j = 0;
        // } else {
        //     j = i + 1;
        // }
        
        // interval = (60f / beatManager._bpm) / beatCount;
        // speed = distance / interval;
       
    }

    
    
}
