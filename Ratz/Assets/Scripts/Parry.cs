using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Parry : MonoBehaviour
{
    public ScriptControls controls;
    private float parryTime = 1f;
    [SerializeField] public float parryTimeStart = 1f;
    private float parryCooldown = .5f;
    [SerializeField] private float parryCooldownStart = .5f;
    [SerializeField] public List<GameObject> hitBoxes = new List<GameObject>();
    [SerializeField] KeyCode _parryButton;
    [SerializeField] private float parriedWindDown;
    public bool inParry;
    private void Start()
    {
        parryTime = parryTimeStart;
         _parryButton = controls.parry;
        parryCooldown = 0;
        inParry = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region parry
        if (Input.GetKeyDown(_parryButton) && parryCooldown <= -.01f)
        {
            parryCooldown = parryCooldownStart;
            inParry = true;
            
        
        }
        else if (inParry)
        {
            if(hitBoxes.Count > 0) {
                for (int i = 0; i < hitBoxes.Count; i++)
                {
                    if (hitBoxes[i].GetComponent<hitBox>().parryTime)
                    {
                        GameObject curHitbox = hitBoxes[i];
                        curHitbox.transform.parent.GetComponent<AttackPlayer>().attacking = false;
                        curHitbox.transform.parent.GetComponent<AttackPlayer>().parried = true;
                        hitBoxes.Remove(curHitbox);
                        Destroy(curHitbox);
                    }
                }
                }
            
            parryTime -= Time.deltaTime;
            if(parryTime <= 0)
            {
                inParry = false;
                parryTime = parryTimeStart;
            }
        }
        else
        {
            parryCooldown -= Time.deltaTime;
        }
         

        #endregion

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hitBox" && !hitBoxes.Contains(collision.gameObject))
        {
            hitBoxes.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hitBox" && hitBoxes.Contains(collision.gameObject))
        {
            hitBoxes.Remove(collision.gameObject);
        }
    }
   
}