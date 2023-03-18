using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointChooser : MonoBehaviour
{
    AttackPoint[] attackPoints;

    // Start is called before the first frame update
    void Start()
    {
        attackPoints = GetComponentsInChildren<AttackPoint>();
    }

    public AttackPoint GetFreePoint()
    {
        for (int i = 0; i < attackPoints.Length; i++)
        {
            if (attackPoints[i].CheckFree()) return attackPoints[i];
        }

        return null; // complete with enemy script
    }


}
