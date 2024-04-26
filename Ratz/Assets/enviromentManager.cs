using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enviromentManager : MonoBehaviour
{
    private ResourceManager manager;
    private SceneManagerBoss bossManager;
    private GameObject threePillars;
    [SerializeField] scriptBoss scriptBoss;
    bool allFall;

    void OnEnable(){
        Actions.OnBossStart += startBoss;
    }

    void OnDisable(){
        Actions.OnBossStart -= startBoss;
    }

    void startBoss(){
        callEnviroment();
    }

    private void Start()
    {
        manager = GameObject.Find("Body").GetComponent<ResourceManager>();
        bossManager = GameObject.Find("BossManager").GetComponent<SceneManagerBoss>();
        threePillars = GameObject.FindGameObjectWithTag("pillarBoss");
    }

    private void Update()
    {

        if (threePillars.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity != Vector2.zero && threePillars.transform.GetChild(1).GetComponent<Rigidbody2D>().velocity != Vector2.zero && threePillars.transform.GetChild(2).GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            allFall = true;
        }
        else
        {
            allFall = false;
        }
    }

    public void callEnviroment()
    {
        if (scriptBoss.attackPattern == 2)
        {
            GameObject.FindGameObjectWithTag("spearSpawn").gameObject.GetComponent<ShootSpear>().spawnSpears();
        }
        else
        {
            if (manager.madOn)
            {
                int childCount = threePillars.transform.childCount;
                int randChoice = Random.Range(0, childCount);
                if(threePillars.transform.GetChild(randChoice).GetComponent<Rigidbody2D>().velocity == Vector2.zero)
                {
                    threePillars.transform.GetChild(randChoice).GetComponent<pillarMovement>().boolFall = true;
                }
                else if (allFall)
                {
                    return;
                }
                else
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
