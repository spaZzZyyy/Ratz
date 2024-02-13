using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class miniCheese : MonoBehaviour
{
    public float attackTime;
    public Vector2 startDir;
    private int curPos;
    private int finalPos;
    BoxCollider2D box;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(attackTime > 0)
        {

            if(IsGrounded())
            {

            }
           
        }
        attackTime-= Time.deltaTime;
    }
    public bool IsGrounded()
    {
        RaycastHit2D rayHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return rayHit.collider != null;
    }

    public void Jump()
    {

    }
}
