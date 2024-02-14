using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapControl : MonoBehaviour
{
    Animator ani;
    [SerializeField] ScriptControls scriptControls;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(scriptControls.musicStartStop)){ // Pause Music
            
        }
    }
}
