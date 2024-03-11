using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideObject : MonoBehaviour
{
    public bool _makeDisappear;
    private SpriteRenderer spriteR;
    
    void Start() {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

   
    // Update is called once per frame
    void Update()
    {
        if(_makeDisappear == true) {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        } else {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
