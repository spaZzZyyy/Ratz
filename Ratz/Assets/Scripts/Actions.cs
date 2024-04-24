using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions
{
    public static Action OnPlayerJump;
    public static Action OnPlayerDashed;
    public static Action OnCameraSwitchTrigger;
    public static Action OnParry;
    public static Action NotParry;
    public static Action PlayerTookDamage;
    public static Action PlayerHealed;
    public static Action OnPlayerSwitchTrack;
    public static Action<GameObject> OnPlayerHitCheckPoint;
    public static Action OnPlayerEnterMadness;
    public static Action OnPlayerExitMadness;
    public static Action OnFlowerShoot;
    public static Action OnPlayerDeath;
    public static Action OnBossStart;
}
