using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUpgrades : MonoBehaviour
{
    [SerializeField] int levelBaseHealth = 0;
    [SerializeField] int levelTowerCount= 0;
    [SerializeField] int levelArcherCount = 0;
    [SerializeField] int levelTowerDamage = 0;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("base-health", levelBaseHealth);
        PlayerPrefs.SetInt("tower-count", levelTowerCount);
        PlayerPrefs.SetInt("castle-archers", levelArcherCount);
        PlayerPrefs.SetInt("tower-damage", levelTowerDamage);
    }
}
