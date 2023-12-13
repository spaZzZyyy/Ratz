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
    [SerializeField] public bool parried;
    [SerializeField] public bool attackThrough;

    [SerializeField] public float attackWindUp;
    [SerializeField] public float attackParryTime;
    [SerializeField] public float attackDamageTime;
    [SerializeField] public float attackOffset;
    public float windDownTime;
    Vector2 attackDirection;
    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        windDownTime = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        parried = false;
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
                    hitBox newHitBoxComp = newHitbox.GetComponent<hitBox>();
                    newHitBoxComp.dir = attackDirection;
                    newHitBoxComp.windUpTime = attackWindUp;
                    newHitBoxComp.parryZone = attackParryTime;
                    newHitBoxComp.hitTime = attackDamageTime;
                   // newHitbox.transform.localScale = new Vector3(newHitbox.transform.localScale.x * attackLength/20, newHitbox.transform.localScale.y, newHitbox.transform.localScale.z);

                    if (attackDirection == Vector2.left)
                    {
                        newHitbox.transform.position = new Vector3(newHitbox.transform.position.x - attackOffset, newHitbox.transform.position.y, 0);
                    } else
                    {
                        newHitbox.transform.position = new Vector3(newHitbox.transform.position.x + attackOffset, newHitbox.transform.position.y, 0);

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
