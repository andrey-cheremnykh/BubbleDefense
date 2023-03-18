using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReward : MonoBehaviour
{
    [SerializeField] int coinValue = 5;
    [SerializeField] int crystalValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EnemyHealth>().onDeath += Reward;
    }


    void Reward()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        mm.AddMoney(coinValue);
        CrystalGameManager cm = FindObjectOfType<CrystalGameManager>();
        cm.AddCrystal(crystalValue);
    }
   
}
