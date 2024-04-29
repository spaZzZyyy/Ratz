using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerBoss : MonoBehaviour
{
    [SerializeField] int maxAttackRange;
    [SerializeField] private CheeseMovement cheese;
    [SerializeField] scriptBoss scriptBoss;
    [SerializeField] public int attackBeats;
    public int attackChoice;

    [SerializeField] private int attackDuration1;
    [SerializeField] private int attackDuration2;
    [SerializeField] private int attackDuration3;

    [SerializeField] private int healthMax;
    private int health;

    void OnEnable(){
        Actions.OnBossStart += startBoss;
    }

    void OnDisable(){
        Actions.OnBossStart -= startBoss;
    }

    void startBoss(){
        callAttack();
    }

    private void Start()
    {
        health = healthMax;
        scriptBoss.attackBeats = 0;
        scriptBoss.attackPattern = 0;
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
        }
    }

    public void callAttack()
    {
        if (cheese == null)
        {
            findCheese();
        }
        if (scriptBoss.attackBeats <= 0)
        {
            attackChoice = selectAttack();
            if(attackChoice == 1)
            {
                scriptBoss.attackBeats = attackDuration1;
            }
            else if (attackChoice == 2)
            {
                scriptBoss.attackBeats = attackDuration2;
            }
            else if(attackChoice == 3)
            {
                scriptBoss.attackBeats = attackDuration3;
            }
            }
            else
            {
                scriptBoss.attackBeats--;
            }
        
    }

    private void findCheese()
    {
        if(GameObject.FindGameObjectWithTag("bossManager").GetComponent<SceneManagerBoss>().health > 0)
        {
            cheese = GameObject.FindGameObjectWithTag("Enemy").GetComponent<CheeseMovement>();
        }
    }
    private int selectAttack()
    {
        int rand = Random.Range(1, maxAttackRange);
        Debug.Log(rand);
        scriptBoss.attackPattern = rand;
        return rand;
        
    }

    public void DamageBoss()
    {
        health--;
        Debug.Log(health);
    }
}
