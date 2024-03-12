using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBox : MonoBehaviour
{
    [SerializeField] public float windUpTime;
    [SerializeField] public float parryZone;
    [SerializeField] public float hitTime;
    public bool parryTime;
    public bool attackThrough;
    SpriteRenderer spriteRenderer;
    AttackPlayer attackPlayer;
    [SerializeField] GameObject player;
    private bool playerIn;
    private HealthManager healthManager;
    public Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        attackPlayer = transform.parent.GetComponent<AttackPlayer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(.2f, .2f, 1f, .5f);
        parryTime = false;
        attackThrough = false;
        player = GameObject.FindGameObjectWithTag("Player");
        healthManager = GameObject.FindGameObjectWithTag("healthManager").GetComponent<HealthManager>();
    }
    /*
    // Update is called once per frame
    void Update()
    {

        if (windUpTime <= 0)
        {
            spriteRenderer.color = new Color(1f, .7f, .2f, .5f);
            parryTime = true;
            if (parryZone <= 0)
            {
                parryTime = false;
                spriteRenderer.color = new Color(1f, .1f, .08f, .5f);
                if(playerIn) {
                    player.GetComponent<PlayerMovement>().Hit(dir);
                    attackPlayer.attackThrough = true;
                    attackPlayer.attacking = false;
                    healthManager.damage();
                    Destroy(gameObject);

                }
                else if(hitTime <= 0)
                {
                    attackPlayer.attackThrough = true;
                    attackPlayer.attacking = false;
                    Destroy(gameObject);
                }
                hitTime -= Time.deltaTime;
            }
            parryZone -= Time.deltaTime;
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
    }*/
}
