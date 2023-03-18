using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] GameObject topPart;

    protected override IEnumerator BuildTheTower()
    {
        topPart.SetActive(false);
        yield return StartCoroutine(base.BuildTheTower());
        topPart.SetActive(true);
        topPart.GetComponent<CannonTop>().ChangeLevel(0);
        AttackRadius = GameConstants.RADIUS_FOR_CANNON[0];
    }

    public override IEnumerator UpgradeToLevel_2()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_CANNON[1]) == false) yield break;
        topPart.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_2());
        topPart.SetActive(true);
        topPart.GetComponent<CannonTop>().ChangeLevel(1);
        AttackRadius = GameConstants.RADIUS_FOR_CANNON[1];
    }

    public override IEnumerator UpgradeToLevel_3()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_CANNON[2]) == false) yield break;
        topPart.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_3());
        topPart.SetActive(true);
        topPart.GetComponent<CannonTop>().ChangeLevel(2);
        AttackRadius = GameConstants.RADIUS_FOR_CANNON[2];
    }

    public override IEnumerator UpgradeToLevel_4A()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_CANNON[3]) == false) yield break;
        topPart.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_4A());
        topPart.SetActive(true);
        topPart.GetComponent<CannonTop>().ChangeLevel(3);
        AttackRadius = GameConstants.RADIUS_FOR_CANNON[3];
    }

    public override IEnumerator UpgradeToLevel_4B()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(GameConstants.PRICES_FOR_CANNON[4]) == false) yield break;
        topPart.SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel_4B());
        topPart.SetActive(true);
        topPart.GetComponent<CannonTop>().ChangeLevel(4);
        AttackRadius = GameConstants.RADIUS_FOR_CANNON[4];
    }

    public override IEnumerator DestroyTower()
    {
        topPart.SetActive(false);
        int index = (int)tState;
        int moneyBack = (int)(GameConstants.PRICES_FOR_CANNON[index - 2] * 0.8f);
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        mm.AddMoney(moneyBack);
        yield return StartCoroutine(base.DestroyTower());
    }


    // Update is called once per frame
    void Update()
    {
        if (tState == TowerState.BUILDING || tState == TowerState.DESTROYING) return;
        EnemyAction enemy = FindEnemyToShoot();
        if (enemy == null) return;

        CannonTop top = topPart.GetComponent<CannonTop>();
        StartCoroutine(top.Shoot(enemy.transform.position));

    }
}
