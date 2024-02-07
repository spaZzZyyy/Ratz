using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class pillarMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject ceiling;
    [SerializeField] private GameObject floor;
    [SerializeField] public bool boolFall;
    [SerializeField] bool boolRetract;
    private Vector3 startingPosition;
    [SerializeField] float timer;
    [SerializeField] private Camera m_Camera;
    private void Start()
    {
        timer = 0;
        startingPosition = transform.position;
        boolFall = false;
        boolRetract = false;
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ceiling.GetComponent<Collider2D>());
    }

    private void Update()
    {
        if (boolFall)
        {
            boolFall = false;
            timer = 1.5f;
            rb.gravityScale = .2f;           
        } else if (boolRetract)
        {
            if (this.transform.position.y < startingPosition.y)
            {
                rb.gravityScale = -.5f;
            }
            else
            {                
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                boolRetract = false;
            }
        } else if (timer > 0 && !boolRetract)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                rb.gravityScale += 3f;

            }
        }
    }

    public void fall()
    {
        boolFall = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "Floor")
        {
            boolRetract = true;
            boolFall= false;
        }
    }
}
