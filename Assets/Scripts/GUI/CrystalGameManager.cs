using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGameManager : MonoBehaviour
{
    int crystalCount = 0;

    public void AddCrystal(int addition)
    {
        crystalCount += addition;
    }

    public int SaveCrystals()
    {
        int allCrystals = PlayerPrefs.GetInt("crystals");
        PlayerPrefs.SetInt("crystals", allCrystals + crystalCount);
        return crystalCount;
    }
    
}
