using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CheeseBossController : Enemy
{


    
    /*
    [SerializeField] private bossHealthManager bossHealth;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int movementForce;
    [SerializeField] private int afterMoveCoolDown;
    private float MoveCoolDown;
    private int attackChoice;
    private bool hitWall;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       MoveCoolDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveCoolDown <= 0)
        {
            attackChoice = 1; //Random.Range(1,1);
            switch (attackChoice)
            {
                case 1:  //Moves across arena 
                    chargeSingle();
                    MoveCoolDown = afterMoveCoolDown;
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
        MoveCoolDown-= Time.deltaTime;
    }

    private void chargeSingle()
    {
        Vector2 dir;
        if (this.transform.position.x > 0)
        {
            dir = Vector2.left;
        } else
        {
            dir = Vector2.right;
        }
        hitWall = false;
        while(!hitWall)
        {
            rb.AddForce(dir * movementForce);
        }
        rb.velocity = Vector2.zero;
        rb.AddForce(-dir * movementForce*10);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall" &&(attackChoice == 1)){
            hitWall = true;
        }
    }
    */
}
