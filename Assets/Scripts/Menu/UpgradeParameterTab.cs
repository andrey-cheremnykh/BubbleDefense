using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeParameterTab : MonoBehaviour
{
    [SerializeField] string id = "tower-count";
    [SerializeField] string parameterName = "Tower Count";

    [Space]
    [SerializeField] TMP_Text priceText;
    [SerializeField] TMP_Text currentInfoText;
    [SerializeField] TMP_Text upgradeInfoText;
    [SerializeField] Button upgradeButton;


    [Space]
    [SerializeField] int[] prices;
    [SerializeField] int beginValue = 5;
    [SerializeField] int upgradeValue = 3;


    // Start is called before the first frame update
    void Start()
    {
        int level = PlayerPrefs.GetInt(id);
        UpdateTexts(level);
        upgradeButton.onClick.AddListener(UpgradeParameter);
    }

    void UpdateTexts(int level)
    {
        int newValue = beginValue + upgradeValue * level;
        if (level >= prices.Length) // that means we upgrade this parameter fully
        {
            priceText.text = "--";
            currentInfoText.text = parameterName + ": " + newValue;
            upgradeInfoText.text = "max";
        }
        else
        {
            priceText.text = "" + prices[level];
            currentInfoText.text = parameterName + ": " + newValue;
            upgradeInfoText.text = "+" + upgradeValue;
        }
    }

    void UpgradeParameter()
    {
        int level = PlayerPrefs.GetInt(id);
        if (level >= prices.Length) return;

        CrystalMenuManager cm = FindObjectOfType<CrystalMenuManager>();
        bool isSuccess = cm.SpendCrystals(prices[level]);

        if (!isSuccess) return;
        level++;
        PlayerPrefs.SetInt(id, level);
        UpdateTexts(level);
    }

   
}
