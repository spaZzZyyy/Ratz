using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public bool attacking;
    [SerializeField] private float attackLength = 1f;
    [SerializeField] GameObject player;
    [SerializeField] GameObject hitBox;
    [SerializeField] float startWindDownTime;
    float windDownTime;
    Vector2 attackDirection;
    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        windDownTime = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        attackDirection = playerDirection();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection, attackLength);
        Debug.DrawRay(transform.position, attackDirection * attackLength, Color.green);
        if(hit.collider != null && windDownTime <= 0)
        {
        windDownTime = startWindDownTime;
            if(hit.collider.gameObject == player)
            {
                attacking = true;
                Debug.DrawRay(transform.position, attackDirection * attackLength, Color.red);
                if(gameObject.transform.childCount == 0)
                {
                    
                    GameObject newHitbox= Instantiate(hitBox, parent:this.transform);

                    if (attackDirection == Vector2.left)
                    {
                        newHitbox.transform.position = new Vector3(newHitbox.transform.position.x - 10, newHitbox.transform.position.y, 0);
                    } else
                    {
                        newHitbox.transform.position = new Vector3(newHitbox.transform.position.x + 10, newHitbox.transform.position.y, 0);

                    }
                }
            }
        }
        windDownTime -=  Time.deltaTime;
    }
   private Vector2 playerDirection()
    {
        float direction = this.transform.position.x - player.transform.position.x;
        if (direction > 0)
        {
           
            return Vector2.left;
        }
        else
        {

            return Vector2.right;
        }
    }
}
