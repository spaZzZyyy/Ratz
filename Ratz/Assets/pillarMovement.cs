using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class pillarMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject ceiling;
    [SerializeField] private float fallSpeedSlow;
    [SerializeField] private float fallSpeedIncrease;
    [SerializeField] private float fallSpeedIncStart;
    [SerializeField] private float fallSpeedUp;
    [SerializeField] public bool boolFall;
    [SerializeField] private scriptBoss scriptBoss;
    [SerializeField] bool boolRetract;
    private Vector3 startingPosition;
    [SerializeField] float timer;
    private void Start()
    {
        timer = 0;
        startingPosition = transform.position;
        boolFall = false;
        boolRetract = false;
        rb = GetComponent<Rigidbody2D>();
        ceiling = GameObject.Find("Ceiling");
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ceiling.GetComponent<Collider2D>());
        fallSpeedIncStart = fallSpeedIncrease;

    }

    private void Update()
    {
        if (boolFall)
        {
            boolFall = false;
            timer = 1.5f;
            rb.velocity = Vector2.down * fallSpeedSlow;
        }
        else if (boolRetract)
        {
            if (this.transform.position.y < startingPosition.y)
            {
                rb.velocity = Vector2.up * fallSpeedUp;
            }
            else
            {
                fallSpeedIncrease = fallSpeedIncStart;
                rb.velocity = Vector2.zero;
                boolRetract = false;
            }
        }
        else if (timer > 0 && !boolRetract)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            rb.velocity += rb.velocity * fallSpeedIncrease;
        }
    }

    public void fall()
    {
        boolFall = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor" || (collision.gameObject.tag == "Enemy" && scriptBoss.attackPattern != 3) || collision.gameObject.tag == "Player")
        {
            if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag != "mainCheese") && rb.velocity.y <= 0)
            {
                Debug.Log(rb.velocity.y);
                Debug.Log("damaged boss");
                GameObject.Find("BossManager").GetComponent<SceneManagerBoss>().DamageBoss();
            } else if (collision.gameObject.tag == "Player" && rb.velocity.y <= 0)
            {
                GameObject.Find("HealthManager").GetComponent<healthManager>().takeDamage(1);
            }
            boolRetract = true;
            boolFall = false;
        }
    }
}

