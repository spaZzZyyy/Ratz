using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckPoint : MonoBehaviour
{
    //Script is for animations when the player hits the checkpoint
    // Start is called before the first frame update
    [SerializeField] GameObject lights;
    [SerializeField] GameObject lights2;
    Light2D lightScript;
    Light2D lightScript2;
    GameObject respawnPoint;
    [SerializeField] float lightUpSpeed;
    float intensity;
    float intensity2;
    bool lightsTriggered = false;

    private void Start() {
        lightScript = lights.GetComponent<Light2D>();
        intensity = lightScript.intensity;
        lightScript.intensity = 0;

        lightScript2 = lights2.GetComponent<Light2D>();
        intensity2 = lightScript2.intensity;
        lightScript2.intensity = 0;
    }

    private void OnEnable() {
        Actions.OnPlayerHitCheckPoint += changeLights;
    }

    private void OnDisable() {
        Actions.OnPlayerHitCheckPoint -= changeLights;
    }

    void changeLights(GameObject respawnPoint){
        if (respawnPoint == this.gameObject){

            if(lightsTriggered == false){

                StartCoroutine("flickerLights");
                lightsTriggered = true;

            }
        }
    }

    IEnumerator flickerLights(){
        lightScript.intensity = Mathf.Lerp(0, intensity, lightUpSpeed);
        lightScript2.intensity = Mathf.Lerp(0, intensity2, lightUpSpeed);
        yield return new WaitForSeconds(0.2f);
        lightScript.intensity = Mathf.Lerp(intensity, 0.5f, lightUpSpeed);
        lightScript2.intensity = Mathf.Lerp(intensity2, 0.5f, lightUpSpeed);
        yield return new WaitForSeconds(0.2f);
        lightScript.intensity = Mathf.Lerp(0, intensity, lightUpSpeed);
        lightScript2.intensity = Mathf.Lerp(0, intensity2, lightUpSpeed);
    }
}
