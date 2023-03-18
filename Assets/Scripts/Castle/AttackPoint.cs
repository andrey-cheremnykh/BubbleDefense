using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    public GameObject enemy;

    public bool CheckFree()
    {
        if (enemy == null) return true;
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth.IsAlive == false) return true;

        return false;
    }
}
