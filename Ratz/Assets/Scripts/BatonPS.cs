using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatonPS : MonoBehaviour
{
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {       
        Actions.OnPlayerSwitchTrack += switchTrack;
    }
    void OnDisable() {
        Actions.OnPlayerSwitchTrack -= switchTrack;
    }

    void switchTrack(){
        ps.Play();
        Debug.Log("Played");
    }


}
