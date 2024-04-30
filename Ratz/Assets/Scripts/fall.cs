using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject ceiling;
    [SerializeField] private float fallSpeedSlow;
    [SerializeField] private float fallSpeedIncrease;
    [SerializeField] private float fallSpeedIncStart;
    [SerializeField] private float fallSpeedUp;
    [SerializeField] public bool boolFall;
    [SerializeField] bool boolStay;
    private Vector3 startingPosition;
    [SerializeField] float timer;
    private void Start()
    {
        timer = 0;
        startingPosition = transform.position;
        boolFall = false;
        boolStay = false;
        rb = GetComponent<Rigidbody2D>();
        ceiling = GameObject.Find("Ceiling");
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ceiling.GetComponent<Collider2D>());
        fallSpeedIncStart = fallSpeedIncrease;

    }

    private void Update()
    {
        if (boolFall)
        {
            boolFall = false;
            timer = 1.5f;
            rb.velocity = Vector2.down * fallSpeedSlow;
        }
        else if (boolStay)
        {
            rb.velocity = Vector2.zero;
        }
        else if (timer > 0 && !boolStay)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            rb.velocity += rb.velocity * fallSpeedIncrease;
        }
    }

    public void falling()
    {
        boolFall = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.name == "Floor" )
        {
            Debug.Log("ouch");
            boolStay = true;
        }
    }
}
