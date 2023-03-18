using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField] GameObject arrowPrefab;

    [SerializeField] GameObject archerOnTower;
    [SerializeField] GameObject extraArcher;
    float damage;

    protected override IEnumerator BuildTheTower()
    {
        SetupUpgradesInLevel setup = FindObjectOfType<SetupUpgradesInLevel>();
        float muliplyer = 1 + setup.TowerDamagePercentage;
        damage = GameConstants.DAMAGE_FOR_ARCHER[0] * muliplyer;
        AttackRadius = GameConstants.RADIUS_FOR_ARCHER[0];
        
        archerOnTower.SetActive(false);
        yield return StartCoroutine(base.BuildTheTower());
        archerOnTower.SetActive(true);
    }

    public override IEnumerator UpgradeToLevel_2()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_ARCHER[1]) == false) yield break;
        archerOnTower.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_2());
        archerOnTower.SetActive(true);
        damage = GameConstants.DAMAGE_FOR_ARCHER[1];
        AttackRadius = GameConstants.RADIUS_FOR_ARCHER[1];
    }

    public override IEnumerator UpgradeToLevel_3()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_ARCHER[2]) == false) yield break;
        archerOnTower.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_3());
        archerOnTower.SetActive(true);
        archerOnTower.transform.localPosition = new Vector3(0, 6, 0);
        damage = GameConstants.DAMAGE_FOR_ARCHER[2];
        AttackRadius = GameConstants.RADIUS_FOR_ARCHER[2];
    }

    public override IEnumerator UpgradeToLevel_4A()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_ARCHER[3]) == false) yield break;
        archerOnTower.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_4A());
        archerOnTower.SetActive(true);
        archerOnTower.transform.localPosition = new Vector3(2.8f, 6.8f, 1.2f);
        extraArcher.SetActive(true);
        extraArcher.transform.localPosition = new Vector3(-2.8f, 6.8f, 1.2f);
        damage = GameConstants.DAMAGE_FOR_ARCHER[3];
        AttackRadius = GameConstants.RADIUS_FOR_ARCHER[3];
    }

    public override IEnumerator UpgradeToLevel_4B()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_ARCHER[4]) == false) yield break;
        archerOnTower.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_4B());
        
        damage = GameConstants.DAMAGE_FOR_ARCHER[4];
        AttackRadius = GameConstants.RADIUS_FOR_ARCHER[4];
        StartCoroutine(ShootAllEnemies());
    }

    public override IEnumerator DestroyTower()
    {
        int index = (int)tState;
        int moneyBack = (int)(GameConstants.PRICES_FOR_ARCHER[index - 2] * 0.8f);
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        mm.AddMoney(moneyBack);
        archerOnTower.SetActive(false);
        yield return StartCoroutine( base.DestroyTower());
    }



    // Update is called once per frame
    void Update()
    {
        if (tState == TowerState.BUILDING || tState == TowerState.DESTROYING) return;
        EnemyAction enemy = FindEnemyToShoot();
        if (enemy == null) return;

        if (tState != TowerState.LEVEL_4B)
        {
            RotateTowardsEnemy(enemy.transform);
            archerOnTower.GetComponent<Archer>().Shoot(enemy.transform, damage);
            if (extraArcher.activeInHierarchy == true)
                extraArcher.GetComponent<Archer>().Shoot(enemy.transform, damage);
        }
       
    }

    IEnumerator ShootAllEnemies()
    {
        List<EnemyAction> enemiesToShoot = FindAllEnemiesInRadius();
        foreach (EnemyAction enemy in enemiesToShoot)
        {
            GameObject newArrow = Instantiate(arrowPrefab);
            newArrow.transform.parent = transform;
            newArrow.transform.localPosition = new Vector3(0, 6.8f, 0);
            newArrow.GetComponent<Arrow>().Launch(enemy.transform, damage);
        }
        if (tState == TowerState.DESTROYING) yield break;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShootAllEnemies());
    }


    void RotateTowardsEnemy(Transform enemyTransform)
    {
        archerOnTower.transform.LookAt(enemyTransform);
        archerOnTower.transform.rotation = Quaternion.Euler(0, archerOnTower.transform.eulerAngles.y + 180, 0);
    }


}
