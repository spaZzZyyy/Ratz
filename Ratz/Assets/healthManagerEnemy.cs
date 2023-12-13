using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthManagerEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hit;
    private int health;
    [SerializeField] private int maxHealth;
    public float parriedWindow;
    
    void Start()
    {
        hit = false;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfHit();
        checkIfDead();
        parriedWindow -= Time.deltaTime;
    }

    void checkIfHit()
    {
        if (hit)
        {
            if(parriedWindow >= 0)
            {
                health -= 3;
            } else
            {
                health -= 1;
            }
            if(GetComponent<EnemyMovementDash>() != null)
            {
                GetComponent<EnemyMovementDash>().checkHitAndParried();
            }
            else if (GetComponent<EnemyMovementFollow>() != null)
            {

            } else if (GetComponent<enemyMovementStagnant>() != null)
            {

            }
            hit = false;
        }

    }
    
    void checkIfDead()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
