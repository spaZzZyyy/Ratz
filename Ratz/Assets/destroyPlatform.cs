using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SinePlatform(Clone)" || collision.gameObject.name == "SinePlatformUp(Clone)")
        {
            Destroy(collision.gameObject);
        }
    }
}
