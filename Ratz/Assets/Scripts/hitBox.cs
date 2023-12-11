using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBox : MonoBehaviour
{
    [SerializeField] float windUpTime;
    [SerializeField] float ParryTime;
    [SerializeField] float hit;
    public bool parryTime;
    SpriteRenderer spriteRenderer;
    AttackPlayer attackPlayer;
    [SerializeField] GameObject player;
    private bool playerIn;
    // Start is called before the first frame update
    void Start()
    {
        attackPlayer = transform.parent.GetComponent<AttackPlayer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(.2f, .2f, 1f, .5f);
        parryTime = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (windUpTime <= 0)
        {
            spriteRenderer.color = new Color(1f, .7f, .2f, .5f);
            parryTime = true;
            if (ParryTime <= 0)
            {
                parryTime = false;
                spriteRenderer.color = new Color(1f, .1f, .08f, .5f);
                if(playerIn) {
                    Debug.Log("Hit player");
                    attackPlayer.attacking = false;
                    Destroy(gameObject);
                }
                else if(hit <= 0)
                {
                    attackPlayer.attacking = false;
                    Destroy(gameObject);
                }
                hit -= Time.deltaTime;
            }
            ParryTime -= Time.deltaTime;
        }
        windUpTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            playerIn = false;
        }
    }
}
