using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrystalMenuManager : MonoBehaviour
{
    [SerializeField] TMP_Text crystalText;

    // Start is called before the first frame update
    void Start()
    {
        int allCrystals = PlayerPrefs.GetInt("crystals");
        crystalText.text = "" + allCrystals;
    }

    public void AddCrystals(int addition)
    {
        int allCrystals = PlayerPrefs.GetInt("crystals");
        PlayerPrefs.SetInt("crystals", allCrystals + addition);
        StartCoroutine(AnimateCrystalText(allCrystals, allCrystals + addition));
    }


    public bool SpendCrystals(int price)
    {
        int allCrystals = PlayerPrefs.GetInt("crystals");
        if (price > allCrystals) return false;

        PlayerPrefs.SetInt("crystals", allCrystals - price);
        StartCoroutine(AnimateCrystalText(allCrystals, allCrystals - price));
        return true;
    }


    IEnumerator AnimateCrystalText(int begin, int end)
    {
        float duration = 0.5f;
        float timer = 0;
        int crystals = begin;
        while (timer < 1)
        {
            timer += 0.01f / duration;
            crystals = (int)Mathf.Lerp(begin, end, timer);
            crystalText.text = "" + crystals;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        crystalText.text = "" + end;
    }

   
}
