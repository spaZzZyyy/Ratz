using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    [SerializeField] GameObject musicManager;
    private startBox startBox;
    private Rigidbody2D platformRb;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        startBox = musicManager.GetComponent<startBox>();
        platformRb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        
        move();
        
    }

    private void move(){
        if (startBox.musicTimer < 1)
        {
            platformRb.velocity = new Vector2(0, 6);
        }
        else{
            platformRb.velocity = new Vector2(0, 0);
        }
    }
}
