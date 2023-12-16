using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    private GameObject healthManager;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthManager = GameObject.FindGameObjectWithTag("healthManager");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            healthManager.GetComponent<HealthManager>().damage();
            player.transform.position = Vector3.zero;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
