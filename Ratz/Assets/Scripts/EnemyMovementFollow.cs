using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementFollow : MonoBehaviour
{
   /* [SerializeField] GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private AttackPlayer playerAttack;
    [SerializeField] private float rayCastOffset;
    [SerializeField] private float debugDistance;
    [SerializeField] private float parriedForce;
    [SerializeField] private float parriedYOffset;
    [SerializeField] private float parriedXOffset;
    [SerializeField] private float parriedWindDown;
    private healthManagerEnemy healthManagerEnemyThis;

    [SerializeField] private Rigidbody2D rb;
    Vector2 dir;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttack = GetComponent<AttackPlayer>();
        rb = GetComponent<Rigidbody2D>();
        healthManagerEnemyThis = GetComponent<healthManagerEnemy>();
    }


    // Update is called once per frame
    void Update()
    {
        //if not attacking, don't move towards player
        if (playerAttack.attacking == false) {
             dir = findPlayerDirection();
            detectEdge(dir);
        }
        checkParried();
    }

    private void detectEdge(Vector2 dir)
    {
        Vector2 rayRightOrigin = new Vector2(this.transform.position.x + rayCastOffset, this.transform.position.y);
        Vector2 rayLeftOrigin = new Vector2(this.transform.position.x - rayCastOffset, this.transform.position.y);
        RaycastHit2D rayRight = Physics2D.Raycast(origin: rayRightOrigin, Vector2.down, debugDistance);
        RaycastHit2D rayLeft = Physics2D.Raycast(origin: rayLeftOrigin, Vector2.down, debugDistance);
        Debug.DrawRay(rayRightOrigin, Vector3.down*debugDistance, Color.green);
        Debug.DrawRay(rayLeftOrigin, Vector3.down * debugDistance, Color.green);
        if (playerAttack.windDownTime <= 0)
        {
            if (rayLeft.collider == null && dir == Vector2.left)
            {
                Debug.DrawRay(rayLeftOrigin, Vector3.down * debugDistance, Color.red);

            }
            else if (rayRight.collider == null && dir == Vector2.right)
            {
                Debug.DrawRay(rayRightOrigin, Vector3.down * debugDistance, Color.red);

            }
            else
            {
                transform.Translate(dir * speed * Time.deltaTime);
            }
        }
    }

    private void checkParried()
    {
        if (playerAttack.parried)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.AddForce(-dir * rb.mass * rb.mass);
            playerAttack.windDownTime = parriedWindDown;
            healthManagerEnemyThis.parriedWindow = parriedWindDown;
            playerAttack.parried = false;


        }
    }
    private Vector2 findPlayerDirection()
    {
        Vector2 dir = new Vector2(0,0); 
        float direction = this.transform.position.x - player.transform.position.x;
        if(direction < 0)
        {
            dir = Vector2.right;
        } else
        {
            dir = Vector2.left;
        }
        return dir;
    }*/
    
}
