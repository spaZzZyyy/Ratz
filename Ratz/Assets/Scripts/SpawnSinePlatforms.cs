using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnSinePlatforms : MonoBehaviour
{
    public void spawnPlatform()
    {
        var parent = GameObject.FindGameObjectWithTag("sineSpawn");
        if (parent.transform.childCount < 3)
        {
            var newPlatform = Instantiate(Resources.Load("Prefabs/Platforms/SinePlatform"), parent.transform);
            newPlatform.GetComponent<SineMovement>().moveSpeed = Random.Range(2, 5);
            newPlatform.GetComponent<SineMovement>().frequency = Random.Range(1, 2);
            newPlatform.GetComponent<SineMovement>().magnitude = Random.Range(1, 10);
            int randBool = Random.Range(0, 2);
            newPlatform.GetComponent<SineMovement>().musicManager = randBool != 0;
        }
    }

}
