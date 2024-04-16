using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class miniCheese : MonoBehaviour
{
    public float attackTime;
    public Vector2 startDir;
    BoxCollider2D box;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject bigCheese;
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float curSpeed;
    [SerializeField] private SceneManagerBoss manager;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.FindGameObjectWithTag("bossManager").GetComponent<SceneManagerBoss>();
        
    }
    // Update is called once per frame
    void Update()
    {
        attackTime = manager.attackBeats;
        if(attackTime > 0)
        {

            if(IsGrounded())
            {
                Jump();
            }
            transform.Translate(startDir * curSpeed * Time.deltaTime);

        }
        else
        {
            Vector2 middlePosition = new Vector2(4, -4.5f);
            if (transform.position.x < 3.75 || transform.position.x > 4.25)
            {
             transform.position = Vector2.MoveTowards(transform.position, middlePosition, curSpeed * Time.deltaTime);
            } else
            {
                if (this.tag == "mainCheese")
                {
                    Instantiate(bigCheese, transform.position, Quaternion.identity);
                }
                Destroy(this.gameObject);
            }
            
        }        
    }
    public bool IsGrounded()
    {
        RaycastHit2D rayHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return rayHit.collider != null;
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "Pillar" || collision.gameObject.tag == "spearSpawn")
        {
            startDir = -startDir;
        }
        else if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("HealthManager").GetComponent<healthManager>().takeDamage(1);
        }
    }
}
