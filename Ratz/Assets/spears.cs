using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spears : MonoBehaviour
{
    public Vector2 direction;
    Rigidbody2D _rb;
    public float speed;
    bool rmSpear = false;
    [SerializeField] float lastingTime = 6f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rmSpear)
        {
            _rb.velocity = Vector2.zero;
            if (lastingTime<=0) {
                Destroy(gameObject);
            }
            lastingTime -= Time.deltaTime;
        }
        else
        {
            _rb.velocity = direction * speed;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            rmSpear = true;
        }
        else
        {
            if (!rmSpear)
            {
                Debug.Log("Damage Player");
                Destroy(gameObject);
            }
        }
    }
}
