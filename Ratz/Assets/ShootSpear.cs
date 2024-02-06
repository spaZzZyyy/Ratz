using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpear : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public GameObject wall;
    [SerializeField] public bool boolShoot;
    [SerializeField] public float startingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(startingSpeed, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "spear")
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
            this.gameObject.tag = "platform";
    }
}
