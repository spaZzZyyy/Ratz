using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class bossAnimations : MonoBehaviour
{
    Animator bossAni;
    Transform bossTF;
    float bossThic;
    float bossHeight;
    void Start()
    {
        bossAni = GetComponent<Animator>();
    }
    private void OnEnable() {
        Actions.OnBossAttackBurst += bossAttackBurst;
        Actions.OnBossMiniSlimes += bossMiniSlimes;
        Actions.OnBossCharge += bossCharge;
    }

    private void OnDisable() {
        Actions.OnBossAttackBurst -= bossAttackBurst;
        Actions.OnBossMiniSlimes -= bossMiniSlimes;
        Actions.OnBossCharge -= bossCharge;
    }


    void bossAttackBurst(){
        bossAni = GetComponent<Animator>();
        StartCoroutine("EnterShoot");
    }
    IEnumerator EnterShoot(){
        yield return new WaitForSeconds(2.7f);
        bossAni.SetTrigger("CheeseAttackBurst");
    }

    void bossMiniSlimes(){
        bossAni = GetComponent<Animator>();
        bossAni.SetTrigger("AttackBurstEnd");
    }

    void bossCharge(){
        bossAni = GetComponent<Animator>();
        bossAni.SetTrigger("AttackBurstEnd");
    }
}
