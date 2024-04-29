using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossShaderHurt : MonoBehaviour
{
    Material material;
    float timeHurt = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void OnEnable() {
        Actions.OnBossHurt += bossHurt;
    }

    private void OnDisable() {
        Actions.OnBossHurt -= bossHurt;
    }

    void bossHurt(){
        material.SetInt("_OnOff", 0); //sets onOff to true
        material.SetColor("_Color", new Color(1,0,0,2));
        StartCoroutine("tookDmg");
    }

    IEnumerator tookDmg(){
        yield return new WaitForSeconds(timeHurt);
        material.SetInt("_OnOff", 1); 
    }
}
