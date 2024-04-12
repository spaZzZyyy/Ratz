using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MadnessAnimations : MonoBehaviour
{
    [SerializeField] Material material; 
    [SerializeField] Material material2; 
    [SerializeField] List<GameObject> lights;
    [SerializeField] Color madnessColor;
    [SerializeField] Color regularColor;
    //[SerializeField] float madIntensity;
    
    private void OnEnable() {
        Actions.OnPlayerEnterMadness += OnPlayerEnterMadness;
        Actions.OnPlayerExitMadness += OnPlayerExitMadness;
    }

    private void OnDisable() {
        Actions.OnPlayerEnterMadness -= OnPlayerEnterMadness;
        Actions.OnPlayerExitMadness -= OnPlayerExitMadness;
    }

    void OnPlayerEnterMadness(){
        gameObject.GetComponent<SpriteRenderer>().material = material2;
        for (int i = 0; i < lights.Count; i++){
            //lights[i].GetComponent<Light2D>().color = Color.Lerp(regularColor,madnessColor, 1.2f); <--- Color light glitches doesn't show
            //lights[i].GetComponent<Light2D>().intensity += madIntensity;
        }
    }

    void OnPlayerExitMadness(){
        gameObject.GetComponent<SpriteRenderer>().material = material;
        for (int i = 0; i < lights.Count; i++){
            //lights[i].GetComponent<Light2D>().color = regularColor;
            //lights[i].GetComponent<Light2D>().intensity -= madIntensity;
        }
    }

}
