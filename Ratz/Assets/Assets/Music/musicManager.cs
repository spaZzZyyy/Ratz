using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    public AudioClip music;

    private void Start() {
        StartCoroutine(playMusic());
    }

    IEnumerator playMusic(){
        AudioSource musicClip = GetComponent<AudioSource>();
        musicClip.Play();
        yield return new WaitForSeconds(musicClip.clip.length);
        musicClip.Play();
    }
}
