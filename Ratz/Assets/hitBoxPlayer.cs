using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class hitBoxPlayer : MonoBehaviour
{
    [SerializeField] public float hitTime;
    SpriteRenderer spriteRenderer;
    [SerializeField] public float attackTime;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, .1f, .08f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(attackTime <= 0)
        {
            Destroy(gameObject);
        }
        attackTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<healthManagerEnemy>().hit = true;
        }
    }
}
