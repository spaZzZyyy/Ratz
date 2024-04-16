using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enviromentManager : MonoBehaviour
{
    private ResourceManager manager;
    private SceneManagerBoss bossManager;
    private GameObject threePillars;

    private void Start()
    {
        manager = GameObject.Find("Body").GetComponent<ResourceManager>();
        bossManager = GameObject.Find("BossManager").GetComponent<SceneManagerBoss>();
        threePillars = GameObject.Find("Pillars");
    }

    public void callEnviroment()
    {
        if (bossManager.attackChoice == 2)
        {
            GameObject.FindGameObjectWithTag("spearSpawn").gameObject.GetComponent<ShootSpear>().spawnSpears();
        }
        else
        {
            if (manager.madOn)
            {
                int childCount = threePillars.transform.childCount;
                int randChoice = Random.Range(0, childCount);
                if(threePillars.transform.GetChild(randChoice).GetComponent<Rigidbody2D>().velocity != Vector2.zero)
                {
                    threePillars.transform.GetChild(randChoice).GetComponent<pillarMovement>().boolFall = true;
                } else
                {
                    callEnviroment();
                }
            }
            else
            {
                GameObject.FindGameObjectWithTag("spearSpawn").gameObject.GetComponent<ShootSpear>().spawnSingleSpear();
            }
        }
    }
}
