using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

public class CheeseMovement : MonoBehaviour
{
    private Vector2 dir;
    [SerializeField] private float acceleration = 1.0f;
    private float maxSpeed = 60.0f;
    private float curSpeed = 0.0f;
    private float attackDur = 0.0f;
    [SerializeField] public int attackChoice;
    public int projectileCount;
    [SerializeField] GameObject projectile;
    [SerializeField] private float maxAttackDur;
    [SerializeField] public int projMoveSpeed;
    [SerializeField] public float radius;
    float delayTime;

    [SerializeField] private float startAngle = 90f, endAngle = 270f;
    // Start is called before the first frame update
    void Start()
    {
        dir = Vector2.left;
        projectileCount = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackDur <= 0)
        {
            attackChoice = selectAttack();
        }
        else
        {
            switch(attackChoice)
            {
                case 1:
                    charge();
                    break;
                case 2:
                    spreadShot();
                    break;
                default:
                    Debug.Log("somehow here");
                    break;
            }
            attackDur -=Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            dir = -dir;
            curSpeed /= 2;
        }
    }

    private int selectAttack()
    {
        int rand = Random.Range(1, 3);
        attackDur = maxAttackDur;
        return rand;
    }

    private void charge()
    {
        transform.Translate(dir * curSpeed * Time.deltaTime);

        curSpeed += acceleration * Time.deltaTime;

        if (curSpeed > maxSpeed)
            curSpeed = maxSpeed;
    }

    private void spreadShot()
    {
        
        curSpeed = 4;
        //Go towards the middles of the arena
        Vector2 middlePosition = new Vector2(4, transform.position.y);
        if (transform.position.x < 3.75 || transform.position.x > 4.25)
        {
            delayTime = 2;
            curSpeed += acceleration * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, middlePosition, curSpeed * Time.deltaTime);
        }
        else
        {            
            //When at center and time out, fire
            if (delayTime < 0)
            {
                delayTime = 1f;
                Vector2 startpoint = this.transform.position;
                spawnProjectiles(projectileCount, startpoint);
                if(startAngle == -90)
                {
                    startAngle = -75;
                    endAngle = 75;
                }
                else
                {
                    startAngle = -90;
                    endAngle = 90;
                }
            }
        }
      
        delayTime -= Time.deltaTime;
     
    }
    private void spawnProjectiles(int numOfProjectiles, Vector2 startPoint)
    {
       
        float angleStep = Mathf.Abs((endAngle-startAngle)) / numOfProjectiles;
        float angle = startAngle;

        for (int i =0; i < numOfProjectiles+1; i++)
          {
              float bulDirX = transform.position.x +Mathf.Sin( (angle*Mathf.PI)/180f)*radius;
              float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f)*radius;

              Vector3 bulMoveVec = new Vector3(bulDirX, bulDirY, 0);
              Vector2 bulDir = (bulMoveVec- transform.position).normalized;

              GameObject bul = Instantiate(projectile);
              bul.transform.position = transform.position;
              bul.GetComponent<goop>().setMoveDir(bulDir);
              bul.GetComponent<goop>().setMoveSpeed(projMoveSpeed);

            angle += angleStep;
          }

       
    }
}



