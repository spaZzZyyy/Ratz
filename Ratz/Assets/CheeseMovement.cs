using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CheeseMovement : MonoBehaviour
{
    private Vector2 dir;
    [SerializeField] private float acceleration = 1.0f;
    private float maxSpeed = 60.0f;
    private float curSpeed = 0.0f;
    private float attackDur = 0.0f;
    [SerializeField] scriptBoss scriptBoss;
    [SerializeField] public int projectileCount;
    [SerializeField] GameObject projectile;
    [SerializeField] private float maxAttackDur;
    [SerializeField] public int projMoveSpeed;
    [SerializeField] public float projDown;
    [SerializeField] public float radius;
    [SerializeField] private GameObject miniCheese;
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
        switch (scriptBoss.attackPattern)
        {
            case 1:
                charge();
                Actions.OnBossCharge();
                break;
            case 2:
                spreadShot();
                Actions.OnBossAttackBurst();
                break;
            case 3:
                miniSlimes();
                Actions.OnBossMiniSlimes();
                break;
            default:
               // Debug.Log("somehow here");
                break;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            dir = -dir;
            curSpeed /= 2;
        }
        else if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("HealthManager").GetComponent<healthManager>().takeDamage(1);
        }
    }


    private void charge()
    {
        transform.Translate(dir * curSpeed * Time.deltaTime);

        curSpeed += acceleration * Time.deltaTime;

        if (curSpeed > maxSpeed){
            curSpeed = maxSpeed;
        }

        /*
        if (dir.x > 0){
            Actions.OnBossMoveLeft();
        }
        if (dir.x < 0){
            Actions.OnBossMoveRight();
        }
        */
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
                delayTime = 1.5f;
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

        for (int i =0; i <= numOfProjectiles; i++)
          {
              float bulDirX = transform.position.x +Mathf.Sin( (angle*Mathf.PI)/180f)*radius;
              float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f)*radius;

              Vector3 bulMoveVec = new Vector3(bulDirX, bulDirY, 0);
              Vector2 bulDir = (bulMoveVec- transform.position).normalized;

              GameObject bul = Instantiate(projectile);
            Physics2D.IgnoreCollision(bul.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
            bul.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            
            bul.GetComponent<goop>().setMoveDir(bulDir);
              bul.GetComponent<goop>().setMoveSpeed(projMoveSpeed);

            angle += angleStep;
        }
    }

    private void miniSlimes()
    {
        curSpeed = 5;
        //Go towards the middles of the arena
        Vector2 newPosition = new Vector2(2, transform.position.y);
        if (transform.position.x < 1.75 || transform.position.x > 2.25)
        {
            delayTime = 2;
            curSpeed += acceleration * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, newPosition, curSpeed * Time.deltaTime);

        }
        else
        {
            if (delayTime < 0)
            {
                Debug.Log("spawn time");
                GameObject newCheese1 = Instantiate(miniCheese, transform.position, Quaternion.identity);
                newCheese1.GetComponent<miniCheese>().attackTime = attackDur;
                newCheese1.GetComponent<miniCheese>().startDir =  Vector2.left;
                newCheese1.tag = "mainCheese";
                GameObject newCheese2 = Instantiate(miniCheese, new Vector2(this.transform.position.x+1, this.transform.position.y+3), Quaternion.identity);
                newCheese2.GetComponent<miniCheese>().attackTime = attackDur;
                newCheese2.GetComponent<miniCheese>().startDir = Vector2.right;
                Destroy(this.gameObject);

            }
        }
        delayTime-= Time.deltaTime;
    }
}



