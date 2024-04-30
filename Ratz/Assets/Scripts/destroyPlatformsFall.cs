using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyPlatformsFall : MonoBehaviour
{
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            Destroy(collision.gameObject);
        }
    }
}
