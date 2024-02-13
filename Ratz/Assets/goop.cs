using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class goop : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection*moveSpeed*Time.deltaTime);

    }

    public void setMoveSpeed(int MS)
    {
        moveSpeed = MS;
    }
    public void setMoveDir(Vector2 moveDir)
    {
        moveDirection = moveDir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
