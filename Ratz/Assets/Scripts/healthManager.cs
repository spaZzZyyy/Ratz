using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class healthManager : MonoBehaviour
{
    /* EXAMPLE OF HOW TO IMPLEMENT
    1. [SerializeField] healthManager healthManager; | Insert this line of code as a variable of script
    2. Assign healthManger to the variable made in the inspector
    3. where you want to give damage or heal use
        -healthManager.takeDamage(PUT DMG NUMBER HERE);
        -healthManager.gainHealth(PUT HEAL NUMBER HERE);
    */

    [SerializeField] RespawnScript respawnScript;
    public bool respawn = false;
    
    bool canTakeDamage = true;
    float secondsOfIframes = 1; // After damage taken how long is the player immune for
    public int health = 6;
    public int maxHealth = 6;
    
    void Update()
    {
        if(health < 1) {
            respawn = true;
        }

        if(respawn == true) {
            if(health < 1) {
                health = maxHealth;
                respawnScript.respawnPoint = respawnScript.mainRespawnPoint;
                respawnScript.Respawn();
                respawn = false;
            } else {
                respawnScript.Respawn();
                respawn = false;
            }
        }
        if(health > maxHealth) {
            health = maxHealth;
        }
    }

    public void takeDamage(int damageToTake){
        if(canTakeDamage == true){
            health -= damageToTake;
            StartCoroutine("OnGiveIFrames");
            Actions.PlayerTookDamage();
        }
        Debug.Log(health);
    }

    public void gainHealth(int heal){
        health += heal;
        Actions.PlayerHealed();
        Debug.Log(health);
    }

    IEnumerator OnGiveIFrames(){
        canTakeDamage = false;
        yield return new WaitForSeconds(secondsOfIframes);
        canTakeDamage = true;
    }
}
