using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleArchers : MonoBehaviour
{
    [SerializeField] float damage = 10;

    [SerializeField] GameObject archerPrefab;
    AttackPoint[] enemyPoints;

    Archer[] archers;
    EnemyHealth enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyPoints = transform.parent.GetComponentsInChildren<AttackPoint>();
        SetArchers();
    }

    void SetArchers()
    {
        SetupUpgradesInLevel setup = FindObjectOfType<SetupUpgradesInLevel>();
        int archerAmount = setup.ArcherBaseCount;
        archers = new Archer[archerAmount];
        for (int i = 0; i < archerAmount; i++)
        {
            GameObject cloneArcher = Instantiate(archerPrefab);
            cloneArcher.transform.position = transform.GetChild(i).position;
            cloneArcher.transform.rotation = transform.GetChild(i).rotation;
            archers[i] = cloneArcher.GetComponent<Archer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
            SearchForEnemy();
        else
            ShootEnemy();
        
    }

    void SearchForEnemy()
    {
        foreach (var point in enemyPoints)
        {
            if (point.CheckFree() == false)
            {
                enemy = point.enemy.GetComponent<EnemyHealth>();
            }
        }
    }

    void ShootEnemy()
    {
        if(!enemy.IsAlive)
        {
            enemy = null;
            return;
        }
        foreach (var archer in archers)
        {
            Vector3 dir = enemy.transform.position - archer.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            archer.transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y+180, 0);
            archer.Shoot(enemy.transform, damage);
        }
    }


}
