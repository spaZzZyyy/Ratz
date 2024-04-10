using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] musicManager musicManager;
    [SerializeField] healthManager healthManager;


    //set Max limits inside collision at bottom
    public float madMax;
    public float madAmount;
    public bool madOn;
    private float madStartCap;

    public float halfMax;
    public float halfAmount;
    public bool halfOn;
    private float halfStartCap;

    
    void Start() {
        madMax = 0;
        madAmount = 0;
        madOn = false;

        halfMax = 0;
        halfAmount = 0;
        halfOn = false;
    }
    
    void Update()
    {
        if(halfOn) {
            if(halfAmount < halfMax) {
                halfAmount ++;
            } else {
                musicManager.halfOut = true;
            }   
        } else {
            if(halfAmount > 0) {
                halfAmount --;
                if(halfAmount < halfStartCap) {
                    musicManager.halfOut = false;
                } else {
                    musicManager.halfOut = true;
                }
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
    //TODO change back max number to 1000
    private void OnTriggerEnter2D(Collider2D switchOn){
        if(switchOn.gameObject.CompareTag("resourceSwitch")) {
            if(halfMax == 0){
                halfMax = 100000;
                halfStartCap = halfMax - ((halfMax / 10) * 2);
            } else {
                musicManager.madOut = false;
                madMax = 100000;
                madStartCap = madMax - ((madMax / 10) * 2);
            }
            Destroy(switchOn.gameObject);
        } if(switchOn.gameObject.CompareTag("cheese")) {
            healthManager.gainHealth(5);
            madAmount = 0; 
            Destroy(switchOn.gameObject);
        }
    }
}
