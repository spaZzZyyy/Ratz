using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBox : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float windUpTime;
    [SerializeField] float ParryTime;
    [SerializeField] float hit;
    [SerializeField] float hitGap;
    public bool parryTime;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(.2f, .2f, 1f, .5f);
        this.gameObject.transform.position = new Vector3(this.transform.position.x + hitGap, this.transform.position.y, this.transform.position.z);
        parryTime = false;
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
                if(hit <= 0)
                {
                    Destroy(gameObject);
                }
                hit -= Time.deltaTime;
            }
            ParryTime -= Time.deltaTime;
        }
        windUpTime -= Time.deltaTime;
    }
}
