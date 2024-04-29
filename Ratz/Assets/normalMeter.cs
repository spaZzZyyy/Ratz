using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalMeter : MonoBehaviour
{
    [SerializeField] float canvasSize;
    [SerializeField] GameObject mask;
    [SerializeField] GameObject eyes;
    [SerializeField] float precentage;
    [SerializeField] Vector3 maskStartPosition;
    [SerializeField] Vector3 eyesStartPosition;
    ResourceManager manager;

    // Update is called once per frame
    private void Start()
    {
        manager = GameObject.Find("Body").GetComponent<ResourceManager>();
        eyes = GameObject.Find("NormalMeterUI");
        mask = GameObject.Find("normalMask");
        maskStartPosition = mask.transform.position;
        eyesStartPosition = eyes.transform.position;
    }
    void Update()
    {
        if (manager.halfOn && !manager.madOn && manager.halfMax >0)
        {
            mask.SetActive(true);
            eyes.SetActive(true);
            precentage = manager.halfAmount / manager.halfMax;
            mask.transform.position = new Vector3(maskStartPosition.x, maskStartPosition.y - canvasSize * precentage, maskStartPosition.z);
            eyes.transform.position = eyesStartPosition;
        }
        else
        {
            mask.transform.position = maskStartPosition;
            eyes.transform.position = eyesStartPosition;
            eyes.SetActive(false);
            mask.SetActive(false);
        }
    }
}
