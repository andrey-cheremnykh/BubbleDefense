using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionWerewolf : EnemyAction
{
    public override IEnumerator SlowEnemyDown(float slowness, float duration)
    {
        slowness = slowness / 2;
        StartCoroutine( base.SlowEnemyDown(slowness, duration));
        yield break;
    }
}
