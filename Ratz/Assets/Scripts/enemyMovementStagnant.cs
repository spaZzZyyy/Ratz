using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementStagnant : MonoBehaviour
{
    /*[SerializeField] GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private AttackPlayer playerAttack;
    [SerializeField] private float rayCastOffset;
    [SerializeField] private float debugDistance;
    [SerializeField] private float parriedForce;
    [SerializeField] private float parriedYOffset;
    [SerializeField] private float parriedXOffset;
    [SerializeField] private float parriedWindDown;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveWindDownStart;
    private healthManagerEnemy healthManagerEnemyThis;
    private float moveWindDown;
    Vector2 dir;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttack = GetComponent<AttackPlayer>();
        rb = GetComponent<Rigidbody2D>();
        dir = Vector2.right;
        moveWindDown = moveWindDownStart;
        healthManagerEnemyThis = GetComponent<healthManagerEnemy>();

    }


    // Update is called once per frame
    void Update()
    {
        //if not attacking, don't move towards player
        if (playerAttack.attacking == false)
        {
            dir = detectEdge(dir);
        }
        checkParried();
        moveWindDownStart -= Time.deltaTime;
    }

    private Vector2 detectEdge(Vector2 dir)
    {
        Vector2 rayRightOrigin = new Vector2(this.transform.position.x + rayCastOffset, this.transform.position.y);
        Vector2 rayLeftOrigin = new Vector2(this.transform.position.x - rayCastOffset, this.transform.position.y);
        RaycastHit2D rayRight = Physics2D.Raycast(origin: rayRightOrigin, Vector2.down, debugDistance);
        RaycastHit2D rayLeft = Physics2D.Raycast(origin: rayLeftOrigin, Vector2.down, debugDistance);
        Debug.DrawRay(rayRightOrigin, Vector3.down * debugDistance, Color.green);
        Debug.DrawRay(rayLeftOrigin, Vector3.down * debugDistance, Color.green);
        if (playerAttack.windDownTime <= 0)
        {
            if ((rayLeft.collider == null || rayLeft.collider.gameObject.tag == "Enemy") && dir == Vector2.left && moveWindDownStart <=0)
            {
                moveWindDown = moveWindDownStart;
                dir = Vector2.right;
                Debug.DrawRay(rayLeftOrigin, Vector3.down * debugDistance, Color.red);

            }
            else if ((rayRight.collider == null || rayRight.collider.gameObject.tag == "Enemy" )&& dir == Vector2.right && moveWindDownStart <= 0)
            {
                moveWindDown = moveWindDownStart;
                dir = Vector2.left;
                Debug.DrawRay(rayRightOrigin, Vector3.down * debugDistance, Color.red);
            }
            
                transform.Translate(dir * speed * Time.deltaTime);
        }
        return dir;
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
    }*/
}
