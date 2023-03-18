using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupUpgradesInLevel : MonoBehaviour
{
    public float BaseHealth { get; private set; }
    public int TowerCount { get; private set; }
    public int ArcherBaseCount { get; private set; }
    public float TowerDamagePercentage { get; private set; }


    // Start is called before the first frame update
    void Awake()
    {
        SetBaseHealth();
        SetTowerCount();
        SetTowerDamage();
        SetArcherCount();
    }

    void SetBaseHealth()
    {
        int level = PlayerPrefs.GetInt("base-health");
        BaseHealth = 100 + level * 100;
    }

    void SetTowerCount()
    {
        int level = PlayerPrefs.GetInt("tower-count");
        TowerCount = 5 + level * 3;
    }

    void SetArcherCount()
    {
        int level = PlayerPrefs.GetInt("castle-archers");
        ArcherBaseCount = level;
    }

    void SetTowerDamage()
    {
        int level = PlayerPrefs.GetInt("tower-damage");
        TowerDamagePercentage = level * 0.1f;
    }

}
