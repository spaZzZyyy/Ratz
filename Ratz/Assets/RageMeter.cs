using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageMeter : MonoBehaviour
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
        eyes = GameObject.Find("rageEyesUI");
        mask = GameObject.Find("rageMask");
        maskStartPosition = mask.transform.position;
        eyesStartPosition = eyes.transform.position;
    }
    void Update()
    {
        if (manager.madOn)
        {
            mask.SetActive(true);
            eyes.SetActive(true);
            precentage = manager.madAmount / manager.madMax;
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
