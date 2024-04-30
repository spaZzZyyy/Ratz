using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] musicManager musicManager;
    [SerializeField] healthManager healthManager;
    [SerializeField] Image mouseFace;
    [SerializeField] Image mouseNose;
    [SerializeField] Image mouseEyes;


    //set Max limits inside collision at bottom
   
    
    public bool madOn;
   

    public float halfMax;
    public float halfAmount;
    public bool halfOn;
    private float halfStartCap;


    void Start()
    {
        madOn = false;

        halfMax = 0;
        halfAmount = 0;
        halfOn = false;

        mouseFace = GameObject.Find("mouseFace").GetComponent<Image>();
        mouseNose = GameObject.Find("mouseNose").GetComponent<Image>();
        mouseEyes = GameObject.Find("uiEyes").GetComponent<Image>();

    }
        void Update()
       
        {
            if (halfOn)
            {
                if (halfAmount < halfMax)
                {
                    halfAmount++;
                }
                else
                {
                    musicManager.halfOut = true;
                }
            }
            else
            {
                if (halfAmount > 0)
                {
                    halfAmount = halfAmount - 3;
                    if (halfAmount < halfStartCap)
                    {
                        musicManager.halfOut = false;
                    }
                    else
                    {
                        musicManager.halfOut = true;
                    }
                }
            }



            if (madOn)
            {
            mouseFace.sprite = Resources.Load<Sprite>("Sprites/madFace");
            mouseEyes.sprite = Resources.Load<Sprite>("Sprites/madEyes");
            mouseNose.sprite = Resources.Load<Sprite>("Sprites/madNose");
            }
            else
            {
            mouseFace.sprite = Resources.Load<Sprite>("Sprites/baseFace");
            mouseEyes.sprite = Resources.Load<Sprite>("Sprites/baseEyes");
            mouseNose.sprite = Resources.Load<Sprite>("Sprites/baseNose");
            }
        }
    //TODO change back max number to 1000
    private void OnTriggerEnter2D(Collider2D switchOn){
        if(switchOn.gameObject.CompareTag("resourceSwitch")) {
            if(halfMax == 0){
                halfOn = true;
                musicManager.halfOut = false;
                halfMax = 5000;
                //cap doesn't let you do halftime till at least 20% regened
                halfStartCap = halfMax - ((halfMax / 10) * 2);
            } else {
                musicManager.madOut = false;
            }
            Destroy(switchOn.gameObject);
        } if(switchOn.gameObject.CompareTag("cheese")) {
            if(halfMax != 0) {
                halfMax = halfMax + 1000;
            }
            healthManager.gainHealth(5);
            Destroy(switchOn.gameObject);
        }
    }
}
