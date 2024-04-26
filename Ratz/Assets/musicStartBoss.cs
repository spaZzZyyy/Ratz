using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicStartBoss : MonoBehaviour
{
    [SerializeField] musicManager musicManager;
    private bool isOn = false;
    [SerializeField] Rigidbody rb;

    private void Start()
    {
        rb = GameObject.Find("Left").GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOn == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                musicManager.startGame = true;
                isOn = true;
            }
        }
    }
}
