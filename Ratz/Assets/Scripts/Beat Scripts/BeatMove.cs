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
    private float bpm;
    public bool noHalftime;



    void Start() {
        i = 1;
        transform.position = points[startingPoint].position;
        bpm = beatManager._bpm;

    }
    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        
        if(noHalftime) {
            interval = (60f / bpm) / beatCount;
        } else {
            interval = (60f / beatManager._bpm) / beatCount;
        }
        speed = distance / interval;
    }

    // TODO Breaks if player gets squished at all 
    private void OnCollisionEnter2D(Collision2D collision) {

        if((this.transform.position.y - collision.collider.transform.position.y) < 0) {
            collision.transform.SetParent(transform);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision) {
        collision.transform.SetParent(null);
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
