using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTower : Tower
{
    [SerializeField] GameObject crystal;

    float fireRate = 1;

    bool isReloaded = true;

    protected override IEnumerator BuildTheTower()
    {
        crystal.SetActive(false);
        yield return StartCoroutine(base.BuildTheTower());
        crystal.SetActive(true);
        crystal.GetComponent<MagicCrystal>().ChangeCrystal(1);
        AttackRadius = GameConstants.RADIUS_FOR_MAGIC[0];
    }

    public override IEnumerator UpgradeToLevel_2()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_MAGIC[1]) == false) yield break;
        crystal.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_2());
        crystal.SetActive(true);
        crystal.GetComponent<MagicCrystal>().ChangeCrystal(2);
        AttackRadius = GameConstants.RADIUS_FOR_MAGIC[1];
    }

    public override IEnumerator UpgradeToLevel_3()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_MAGIC[2]) == false) yield break;
        crystal.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_3());
        crystal.SetActive(true);
        crystal.GetComponent<MagicCrystal>().ChangeCrystal(3);
        AttackRadius = GameConstants.RADIUS_FOR_MAGIC[2];
    }

    public override IEnumerator UpgradeToLevel_4A()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_MAGIC[3]) == false) yield break;
        crystal.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_4A());
        crystal.SetActive(true);
        crystal.GetComponent<MagicCrystal>().ChangeCrystal(4);
        AttackRadius = GameConstants.RADIUS_FOR_MAGIC[3];
        fireRate = 2;
    }

    public override IEnumerator UpgradeToLevel_4B()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_MAGIC[4]) == false) yield break;
        crystal.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_4B());
        crystal.SetActive(true);
        crystal.GetComponent<MagicCrystal>().ChangeCrystal(5);
        AttackRadius = GameConstants.RADIUS_FOR_MAGIC[4];
    }

    public override IEnumerator DestroyTower()
    {
        int index = (int)tState;
        int moneyBack = (int)(GameConstants.PRICES_FOR_MAGIC[index - 2] * 0.8f);
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        mm.AddMoney(moneyBack);
        crystal.SetActive(false);
        yield return StartCoroutine(base.DestroyTower());
    }

    // Update is called once per frame
    void Update()
    {
        if (tState == TowerState.BUILDING || tState == TowerState.DESTROYING) return;
        if (isReloaded == false) return;
        EnemyAction enemy = FindEnemyToShoot();
        if (!enemy) return;
        if (tState == TowerState.LEVEL_4B)
            crystal.GetComponent<MagicCrystal>().SplashShoot(GetEnemiesToShoot());
        else
            crystal.GetComponent<MagicCrystal>().Shoot(enemy.GetComponent<EnemyHealth>());
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        isReloaded = false;
        yield return new WaitForSeconds(1 / fireRate);
        isReloaded = true;
    }

    EnemyHealth[] GetEnemiesToShoot()
    {
        List<EnemyAction> enemies = FindAllEnemiesInRadius();
        EnemyHealth[] enHealths = new EnemyHealth[enemies.Count];
        for (int i = 0; i < enHealths.Length; i++)
        {
            enHealths[i] = enemies[i].GetComponent<EnemyHealth>();
        }
        return enHealths;
    }
}
