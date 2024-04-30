using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Attack : MonoBehaviour
{
   /* public ScriptControls controls;
    public Vector2 dir;
    [SerializeField] private GameObject playerHitBox;
    [SerializeField] private float attackOffsetX;
    [SerializeField] private float attackOffsetY;
    [SerializeField] private float attackTime;
    private float timeSinceLastAttack;
    private Parry mouseParry;

    #region Assigning Controls

        private KeyCode _moveRightButton;
        private KeyCode _moveLeftButton;
        private KeyCode attackButton;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Assigning Controls

        _moveRightButton = controls.moveRight;
        _moveLeftButton = controls.moveLeft;

    #endregion
        dir = Vector2.right;
        timeSinceLastAttack = 0;
        mouseParry = GetComponent<Parry>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = getAttackDir(dir);
        if (Input.GetKeyDown(attackButton) && timeSinceLastAttack <=0 && !mouseParry.inParry)
        {
            timeSinceLastAttack = attackTime;
            attackHitBox(dir);
        }
        timeSinceLastAttack -= Time.deltaTime;
    }

    #region Get Direction of Attack
    Vector2 getAttackDir(Vector2 startDir)
    {
        Vector2 dir;
        if(Input.GetKeyDown(_moveRightButton))
        {
            dir = Vector2.right;
        } else if(Input.GetKeyDown(_moveLeftButton))
        {
            dir = Vector2.left;
        }
        else
        {
            dir = startDir;
        }
        return dir;
    }
    #endregion

    void attackHitBox(Vector2 dir)
    {
        GameObject newHitbox = Instantiate(playerHitBox, parent: this.transform);
        newHitbox.GetComponent<hitBoxPlayer>().attackTime = attackTime;
        newHitbox.transform.position = new Vector3((newHitbox.transform.position.x + attackOffsetX * dir.x),newHitbox.transform.position.y + attackOffsetY, 0);
    }*/
}
