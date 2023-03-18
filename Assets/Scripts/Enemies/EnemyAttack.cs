using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int damage = 20;
    
    public void Damage()
    {
        CastleHealth castle = FindObjectOfType<CastleHealth>();
        castle.GetDamage(damage);
    }

}
