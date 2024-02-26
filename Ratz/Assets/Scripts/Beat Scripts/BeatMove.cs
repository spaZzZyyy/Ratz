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
        i = 1;
        transform.position = points[startingPoint].position;
    }
    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        
        interval = (60f / beatManager._bpm) / beatCount;
        speed = distance / interval;
    }

    // TODO make player stay on platform (prob gonna have to raycast)
    // private void OnCollisionEnter2D(Collision2D collision) {
    //     collision.transform.SetParent(transform);
    // }

    // private void OnCollisionExit2D(Collision2D collision) {
    //     collision.transform.SetParent(null);
    // }
   

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
