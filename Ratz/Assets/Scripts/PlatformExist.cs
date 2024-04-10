using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformExist : MonoBehaviour
{
    [SerializeField] musicManager musicManager;
    Material material;
    [SerializeField] GameObject artBlock;
    public bool _onWhileMad;

    void Start(){
        material = artBlock.GetComponent<SpriteRenderer>().material;
        

    }

    void Update()
    {
        if(_onWhileMad) {
            if(musicManager.platformMad) {
                GetComponent<Collider2D>().enabled = true;
                material.SetInt("_OpacityActive", 0);
            } else {
                GetComponent<Collider2D>().enabled = false;
                material.SetInt("_OpacityActive", 1);
            }
        } else {
            if(musicManager.platformMad) {
                GetComponent<Collider2D>().enabled = false;
                material.SetInt("_OpacityActive", 1);
            } else {
                GetComponent<Collider2D>().enabled = true;
                material.SetInt("_OpacityActive", 0);
            }
        }
    }
}
