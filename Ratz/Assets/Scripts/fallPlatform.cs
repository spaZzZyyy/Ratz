using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float fallSpeed;
    [SerializeField] float fallRate;
    public bool isFalling = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (isFalling)
        {
            fall();
        }
    }
    public void fall()
    {
        rb.velocity -= new Vector2(0, Mathf.Pow(fallSpeed, fallRate) * Time.deltaTime);
    }
}
