using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderHurt : MonoBehaviour
{
    [SerializeField] healthManager healthManager;
    Material material;
    float timeHurt = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void OnEnable() {
        Actions.PlayerTookDamage += tookDamage;
    }

    private void OnDisable() {
        Actions.PlayerTookDamage -= tookDamage;
    }

    void tookDamage(){
        material.SetInt("_OnOff", 0); //sets onOff to true
        material.SetColor("_Color", new Color(1,0,0,2));
        StartCoroutine("tookDmg");
    }

    IEnumerator tookDmg(){
        yield return new WaitForSeconds(timeHurt);
        material.SetInt("_OnOff", 1); 
        Debug.Log("PlayerTookDmg");
    }
    
}
