using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] musicManager musicManager;

    private int madMax = 100;
    private int madAmount;
    public bool madUnlock;
    public bool madOn;

    private int halfMax = 100;
    private int halfAmount;
    public bool halfUnlock;
    public bool halfOn;

    void Start()
    {
        madUnlock = false;
        halfUnlock = false;   
        madOn = false;
        halfOn = false;
        madAmount = 0;
        halfAmount = 0;
    }

    void Update()
    {
        if(halfOn) {
            if(halfAmount < halfMax) {
                halfAmount ++;
            } else {
                if(musicManager.trackToPlay == 2){
                    musicManager.trackToPlay = 1;
                } else {
                    musicManager.trackToPlay = 3;
                }
                halfOn = false;
            }   
        } else {
            if(halfAmount > 1) {
                halfAmount --;
            }
        }

        if(madOn) {
            if(madAmount < madMax) {
                madAmount ++;
            } else {
                if(musicManager.trackToPlay == 3){
                    musicManager.trackToPlay = 1;
                } else {
                    musicManager.trackToPlay = 2;
                }
                madOn = false;
            }   
        } else {
            if(madAmount > 1) {
                madAmount --;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D switchOn){
        if(switchOn.gameObject.CompareTag("resourceSwitch")) {
            if(halfUnlock == true){
                madUnlock = true;
                musicManager.numOfTracks = 4;
            } else {
                halfUnlock = true;
                musicManager.numOfTracks = 2;
            }
            Destroy(switchOn.gameObject);
        }
    }
}
