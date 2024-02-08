using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class startBox : MonoBehaviour
{   
    [SerializeField] AudioClip music;
    [SerializeField] AudioSource musicBox;
    public float musicTimer = 0;
    public bool musicPlaying = false;
    private void OnCollisionEnter2D(Collision2D other) {
        StartCoroutine("playMusic");
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            musicPlaying = false;
            musicBox.Stop();
            StopCoroutine("playMusic");
        }
        if(Input.GetKeyUp(KeyCode.F)){
            musicPlaying = true;
            StartCoroutine("playMusic");
        }
    }

    private void FixedUpdate() {
        if(musicPlaying){
            musicTimer += 0.015f;
        }
    }
    IEnumerator playMusic(){
        musicBox.time = musicTimer;
        musicBox.Play();
        yield return new WaitForSeconds(0);
    }
}
