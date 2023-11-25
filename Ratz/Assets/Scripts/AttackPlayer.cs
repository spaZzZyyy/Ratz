using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float attackLength = 1f;
    [SerializeField] GameObject player;
    [SerializeField] GameObject hitBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, attackLength);
        Debug.DrawRay(transform.position, Vector2.right* attackLength, Color.green);
        if(hit.collider != null)
        {
            if(hit.collider.gameObject == player)
            {
                Debug.DrawRay(transform.position, Vector2.right * attackLength, Color.red);
                if(gameObject.transform.childCount == 0)
                {
                    Instantiate(hitBox, parent:this.transform);
                }
            }
        }
    }
}
