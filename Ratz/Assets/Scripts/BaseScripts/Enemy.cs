using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public Rigidbody2D RB {  get; set; }

    public float CurrentHealth { get; set; }

    public CheeseStateMachine StateMachine { get; set; }
    public CheeseAttackState attackState { get; set; }
    public CheeseIdleState IdleState { get; set; }
    public CheeseChargeState chargeState { get; set; }

    #region Idel Var
    public float idleTime = 10f;
        
    #endregion

    private void Awake()
    {
        StateMachine = new CheeseStateMachine();

        IdleState = new CheeseIdleState(this, StateMachine);
        attackState = new CheeseAttackState(this, StateMachine);


    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);

    }

    private void Update()
    {
        StateMachine.currentCheeseState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.currentCheeseState.PhysicsUpdate();
    }

    public void Damage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {

    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.currentCheeseState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged
    }
}
