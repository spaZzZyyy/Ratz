using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformExist : MonoBehaviour
{
    [SerializeField] musicManager musicManager;
    public bool _onWhileMad;


    void Update()
    {
        if(_onWhileMad) {
            if(musicManager.platformMad) {
                GetComponent<Collider2D>().enabled = true;
            } else {
                GetComponent<Collider2D>().enabled = false;
            }
        } else {
            if(musicManager.platformMad) {
                GetComponent<Collider2D>().enabled = false;
            } else {
                GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
