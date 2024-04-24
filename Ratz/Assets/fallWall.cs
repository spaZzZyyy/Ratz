using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallWall : MonoBehaviour
{
    [SerializeField] fall wall;
    bool fell = false;
    private void Start()
    {
        wall = GameObject.Find("Left").GetComponent<fall>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log("falling");
        if (collision.gameObject.tag == "Player" && !fell)
        {
            fell = true;
            wall.falling();
        }
    }
}
