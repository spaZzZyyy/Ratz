using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShootSpear : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] float speed;
    public void spawnSpear() 
    {
        var parents = GameObject.FindGameObjectsWithTag("spearSpawn");
        for (int i = 0; i < parents.Length; i++)
        {
            var parent = parents[i];  
            if (parent.transform.childCount == 0)
            {
                var newPlatform = Instantiate(Resources.Load("Prefabs/Platforms/spear"), parent.transform);
                newPlatform.GetComponent<spears>().direction = parent.gameObject.GetComponent<ShootSpear>().direction;
                newPlatform.GetComponent<spears>().speed = parent.gameObject.GetComponent<ShootSpear>().speed;
            }
        }
        
        if (collision.gameObject.tag == "wall")
        {
            gameObject.tag = "platform";
            rb.isKinematic = true;
            transform.Rotate(0,0,0);
            fadeTimer = destroyTime;
            
        }
    }
}
