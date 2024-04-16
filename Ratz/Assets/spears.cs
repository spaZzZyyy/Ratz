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
        
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "mainCheese")
        {
            if (collision.gameObject.layer == 8)
            {
                if (collision.gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
                {
                    Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
                }
            }
            else
            {
                rmSpear = true;
            }
        }
        else
        {
            if (!rmSpear && collision.gameObject.tag == "Player")
            {
                GameObject.Find("HealthManager").GetComponent<healthManager>().takeDamage(1);
                Destroy(gameObject);
            }
            else if (!rmSpear) 
            {
                GameObject.Find("BossManager").GetComponent<SceneManagerBoss>().DamageBoss();
                Destroy(gameObject);
            }
        }
    }
}
