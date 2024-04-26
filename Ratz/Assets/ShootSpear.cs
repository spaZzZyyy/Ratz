using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootSpear : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] float speed;
    [SerializeField] private GameObject wallLeft;
    [SerializeField] private GameObject wallRight;
    [SerializeField] private bool allSpawed;
    int spearCount;

    private void Start()
    {
        wallLeft = GameObject.Find("Left");
        wallRight = GameObject.Find("Right");
    }

    private void Update()
    {   
        spearCount = GameObject.FindGameObjectsWithTag("spear").Count();
        if (spearCount == 6 ) {
            allSpawed = true;
        }
    }

    public void spawnSpears() 
    {
        var parents = GameObject.FindGameObjectsWithTag("spearSpawn");
        for (int i = 0; i < parents.Length; i++)
        {
            var parent = parents[i];
            var spearParent = wallLeft;
            if (parent.transform.childCount == 0)
            {
                if (parent.transform.parent.name == "SpearsLeft")
                {
                    spearParent = wallLeft;
                }
                else
                {
                    spearParent = wallRight;
                }
                Debug.Log("spawning spears");

                if(SceneManager.GetActiveScene().buildIndex == 1)
                {
                    Actions.OnFlowerShoot();
                }
                
    
                var newPlatform = Instantiate(Resources.Load("Prefabs/Platforms/spear"), parent.transform);
                Physics2D.IgnoreCollision(newPlatform.GetComponent<Collider2D>(), spearParent.GetComponent<Collider2D>());
                newPlatform.GetComponent<spears>().direction = parent.gameObject.GetComponent<ShootSpear>().direction;
                newPlatform.GetComponent<spears>().speed = parent.gameObject.GetComponent<ShootSpear>().speed;
            }
        }
    }

    public void spawnSingleSpear()
    {
        var parent = wallRight;
        var spears = GameObject.FindGameObjectsWithTag("spearSpawn");
        int randoSpearIndex = Random.Range(0, spears.Length);
        if (spears[randoSpearIndex].transform.parent.name == "SpearsLeft")
        {
            parent = wallLeft;
        }
        else
        {
            parent = wallRight;
        }
        if (spears[randoSpearIndex].transform.childCount == 0 && spears[randoSpearIndex] != findClosestSpear())
        {
            var newPlatform = Instantiate(Resources.Load("Prefabs/Platforms/spear"), spears[randoSpearIndex].transform);
            Physics2D.IgnoreCollision(newPlatform.GetComponent<Collider2D>(), parent.GetComponent<Collider2D>());
            newPlatform.GetComponent<spears>().direction = spears[randoSpearIndex].gameObject.GetComponent<ShootSpear>().direction;
            newPlatform.GetComponent<spears>().speed = spears[randoSpearIndex].gameObject.GetComponent<ShootSpear>().speed;
        }

        else if (allSpawed)
        {
            return;
        }
        else
        {
            spawnSingleSpear();
        }
    }

    private GameObject findClosestSpear()
    {
        GameObject closest = null;
        var spearSpawns = GameObject.FindGameObjectsWithTag("spearSpawn");
        var findPlayer = GameObject.FindGameObjectWithTag("Player");
        foreach (var spear in spearSpawns)
        {
            if (closest == null)
            {
                closest = spear;
            } else
            {
                var findPlayerPosition = findPlayer.transform.position;
                var nextSpearSpawnPosition = spear.transform.position;
                var closestPosition = closest.transform.position;
                
                var temp1 = findPlayerPosition - closestPosition;
                var temp2 = findPlayerPosition - nextSpearSpawnPosition;

                var curCloseHyp = Mathf.Sqrt((temp1.x * temp1.x) + (temp1.y * temp1.y));
                var nextCloseHyp = Mathf.Sqrt((temp2.x * temp2.x) + (temp2.y * temp2.y));
                if(curCloseHyp > nextCloseHyp)
                {
                    closest = spear.gameObject;
                }
            }
        }
        return closest;
    }
}

