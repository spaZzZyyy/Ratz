using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpear : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public GameObject wall;
    [SerializeField] public bool boolShoot;
    [SerializeField] public float startingSpeed;
    [SerializeField] public float destroyTime = 10;
    private float fadeTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), wall.GetComponent<Collider2D>());

    }

    // Update is called once per frame
    void Update()
    {
        if (boolShoot)
        {
            boolShoot = false;
            rb.velocity = new Vector2(startingSpeed, 0);
        }
        if (this.gameObject.tag == "platform")
        {
            
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;

            fadeTimer -= Time.deltaTime;    
            if (fadeTimer < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "wall")
        {
            gameObject.tag = "platform";
            rb.isKinematic = true;
            transform.Rotate(0,0,0);
            fadeTimer = destroyTime;
            
        }
    }
}
