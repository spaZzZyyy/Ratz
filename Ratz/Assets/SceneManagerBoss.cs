using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerBoss : MonoBehaviour
{
    [SerializeField] int maxAttackRange;
    [SerializeField] private CheeseMovement cheese;
    [SerializeField] public int attackBeats;
    public int attackChoice;

    [SerializeField] private int attackDuration1;
    [SerializeField] private int attackDuration2;
    [SerializeField] private int attackDuration3;

    [SerializeField] private int healthMax;
    private int health;

    private void Start()
    {
        health = healthMax;

    }

    private void Update()
    {
        if (health <0) {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }
            Destroy(GameObject.FindGameObjectWithTag("mainCheese"));
            Debug.Log("Game Over, you win");
        }
    }

    public void callAttack()
    {
        
        if (attackBeats <= 0)
        {
            cheese = GameObject.FindGameObjectWithTag("Enemy").GetComponent<CheeseMovement>();
            attackChoice = selectAttack();
            if(attackChoice == 1)
            {
                attackBeats = attackDuration1;
            }
            else if (attackChoice == 2)
            {
                attackBeats = attackDuration2;
            }
            else if(attackChoice == 3)
            {
                attackBeats = attackDuration3;
            }
        }
        else
        {
            attackBeats--;
        }
    }
    private int selectAttack()
    {
        int rand = Random.Range(1, maxAttackRange);
        Debug.Log(rand);
        cheese.attackChoice = rand;
        return rand;
        
    }

    public void DamageBoss()
    {
        health--;
    }
}
