using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class startMusicTrigger : MonoBehaviour
{
    [SerializeField] musicManager musicManager;
    [SerializeField] mapControl mapControl;
    private bool isOn = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("hit");
        if(isOn == false) {
            if(collision.gameObject.CompareTag("Player")){
                musicManager.startGame = true;
                //mapControl.initiate = true;
                isOn = true;
            }
        }
        
    }
}
