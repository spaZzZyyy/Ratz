using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementDash : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private AttackPlayer playerAttack;
    [SerializeField] private float rayCastOffset;
    [SerializeField] private float debugDistance;
    [SerializeField] private float parriedWindDown;
    [SerializeField] private float attackDashLength;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float hitOffsetx;
    [SerializeField] private float hitOffsety;
    private healthManagerEnemy healthManagerEnemyThis;
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
        if (playerAttack.attacking == false && !playerAttack.attackThrough)
        {
            dir = findPlayerDirection();
            detectEdge(dir);
        } else
        {
            checkAttackThrough();
        }
        checkParried();
        checkHitAndParried();
        
       
       
    }

    private void detectEdge(Vector2 dir)
    {
        Vector2 rayRightOrigin = new Vector2(this.transform.position.x + rayCastOffset, this.transform.position.y);
        Vector2 rayLeftOrigin = new Vector2(this.transform.position.x - rayCastOffset, this.transform.position.y);
        RaycastHit2D rayRight = Physics2D.Raycast(origin: rayRightOrigin, Vector2.down, debugDistance);
        RaycastHit2D rayLeft = Physics2D.Raycast(origin: rayLeftOrigin, Vector2.down, debugDistance);
        Debug.DrawRay(rayRightOrigin, Vector3.down * debugDistance, Color.green);
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
            float distance = player.transform.position.x - this.transform.position.x;
            this.transform.position = new Vector2(this.transform.position.x + distance, this.transform.position.y) - dir*10;
            healthManagerEnemyThis.parriedWindow = parriedWindDown;
            playerAttack.parried = false;
        }
    }

    private void checkAttackThrough()
    {
        if (playerAttack.attackThrough)
        {
            if(dir == Vector2.left)
            {
                this.transform.position = new Vector2(this.transform.position.x - attackDashLength, this.transform.position.y);
            }
            else
            {
                this.transform.position = new Vector2(this.transform.position.x + attackDashLength, this.transform.position.y);
            }
            playerAttack.attackThrough = false;
        }
    }

    public void checkHitAndParried()
    {
        if (healthManagerEnemyThis.parriedWindow >= 0 && healthManagerEnemyThis.hit)
        {
            rb.AddForce(new Vector2(player.GetComponent<Attack>().dir.x*hitOffsetx, hitOffsety) * rb.mass* rb.mass/2);
            Debug.Log("force applied");
        }
    }
    private Vector2 findPlayerDirection()
    {
        Vector2 dir = new Vector2(0, 0);
        float direction = this.transform.position.x - player.transform.position.x;
        if (direction < 0)
        {
            dir = Vector2.right;
        }
        else
        {
            dir = Vector2.left;
        }
        return dir;
    }
}
