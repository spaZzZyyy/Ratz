using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnSinePlatformsUp : MonoBehaviour
{
    public void spawnPlatform()
    {
        var parent = GameObject.FindGameObjectWithTag("sineSpawnUp");
        if (parent.transform.childCount < 25)
        {
            var newPlatform = Instantiate(Resources.Load("Prefabs/Platforms/SinePlatformUp"), parent.transform);
            newPlatform.GetComponent<sineMovementUp>().moveSpeed = Random.Range(1, 3);
            newPlatform.GetComponent<sineMovementUp>().frequency = Random.Range(2, 4);
            newPlatform.GetComponent<sineMovementUp>().magnitude = Random.Range(1, 6);
            int randBool = Random.Range(0, 2);
            newPlatform.GetComponent<sineMovementUp>().musicManager = randBool != 0;
        }
    }
}
