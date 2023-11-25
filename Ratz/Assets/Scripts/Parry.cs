using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Parry : MonoBehaviour
{
    [SerializeField] List<GameObject> hitBoxes;


    // Update is called once per frame
    void Update()
    {
        if(hitBoxes.Count > 0)
        {
            Debug.Log(hitBoxes);
            for(int i = 0; i < hitBoxes.Count; i++)
            {
                hitBox currHitBox = hitBoxes[i].GetComponent<hitBox>();
                if (currHitBox.parryTime)
                {
                    Debug.Log("parried");
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "hitBox")
        {
            hitBoxes.Append(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "hitBox")
        {
            hitBoxes.Remove(collision.gameObject);
        }
    }
}
