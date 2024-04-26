using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] Image healthWhiskers;
    
    bool canTakeDamage = true;
    float secondsOfIframes = 1; // After damage taken how long is the player immune for
    public int health = 6;
    public int maxHealth = 6;

    private void Start()
    {
        healthWhiskers = GameObject.Find("healthWhiskers").GetComponent<Image>();
    }

    void Update()
    {
        if(health < 1) {
            respawn = true;
        }

        if(respawn == true) {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                respawn = false;
            }
            else if (health < 1) {
                health = maxHealth;
                respawnScript.respawnPoint = respawnScript.mainRespawnPoint;
                respawnScript.Respawn();
                respawn = false;
                healthWhiskers.sprite = Resources.Load<Sprite>("Sprites/health8");
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
            healthWhiskers.sprite = Resources.Load<Sprite>("Sprites/health" + health);
        }
    }

    public void gainHealth(int heal){
        if((health + heal) > maxHealth) {
            health = maxHealth;
        } else {
            health += heal;
        }
        ////Actions.PlayerHealed();
        Debug.Log(health);
    }

    IEnumerator OnGiveIFrames(){
        canTakeDamage = false;
        yield return new WaitForSeconds(secondsOfIframes);
        canTakeDamage = true;
    }
}
