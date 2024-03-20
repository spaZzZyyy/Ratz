using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class spawnFallingPlatforms : MonoBehaviour
{
    int startChildCount;

    private void Start()
    {
        startChildCount = gameObject.transform.childCount;
    }
    public void fallPlatform()
    {
        //If last spawn has no child, spawn all platforms
        if (this.gameObject.transform.GetChild(startChildCount - 1).childCount == 0)
        {
            Debug.Log(this.gameObject.transform.GetChild(startChildCount - 1).gameObject.name);
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var newChild = Instantiate(Resources.Load("Prefabs/Platforms/fallPlatform"), this.gameObject.transform.GetChild(i).transform);
            }
        }
        else
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (this.gameObject.transform.GetChild(i).childCount > 0)
                {
                    if (this.gameObject.transform.GetChild(i).GetChild(0).GetComponent<fallPlatform>().isFalling == false)
                    {
                        this.gameObject.transform.GetChild(i).GetChild(0).GetComponent<fallPlatform>().isFalling = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
}
