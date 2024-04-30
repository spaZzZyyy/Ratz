using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageMusic : MonoBehaviour
{

    [SerializeField] AudioSource startMusic;
    [SerializeField] AudioSource fallingMusic;
    private int i;
    [SerializeField] ScriptMovement movement;

    void Start()
    {
        startMusic.Play();
        i = 0;
    }

    void Update() {
        if(i == 0) {
            Actions.OnPlayerSwitchTrack();
        }
    }

    private void OnTriggerEnter2D(Collider2D musicTrigger){
        if(musicTrigger.gameObject.CompareTag("cageMusic")) {
            if(i == 0) {
                startMusic.Pause();
                i++;
            } else if(i == 1) {
                fallingMusic.Play();
                movement.gravityForce = 1f;
                i++;
            } else {
                movement.gravityForce = 12f;
            }
            Destroy(musicTrigger.gameObject);
        }
    }
}
