using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTowersLogic : MonoBehaviour
{
    [SerializeField] GameObject changeButtons;
    [SerializeField] GameObject upgradeToLevel4Bbutton;
    [SerializeField] GameObject upgradeButton;

    [SerializeField] GameObject buildButtons;

    [Header("Prices display texts")]
    [SerializeField] Text buildArcherPrice;
    [SerializeField] Text buildMagicPrice;
    [SerializeField] Text buildCannonPrice;

    [Space]
    [SerializeField] Text upgradePrice;
    [SerializeField] Text destroyPrice;
    [SerializeField] Text upgrade4BPrice;

    public bool changeButtonsActive { get; private set; }
    public bool buildButtonsActive { get; private set; }

    // Add Text prices for upgrades

    // Start is called before the first frame update
    void Start()
    {
        changeButtons.SetActive(false);
        buildButtons.SetActive(false);
    }


    public void ShowChangeButtons(Tower tower)
    {
        Vector3 canvasPos = Camera.main.WorldToScreenPoint(tower.transform.position);
        changeButtons.SetActive(true);
        changeButtons.transform.position = canvasPos;
        changeButtons.GetComponent<Animator>().SetBool("hide", false);
        EnableTheButtons(tower);
        DisplayPrices(tower);
        changeButtonsActive = true;
    }

    void DisplayPrices(Tower tower)
    {
        if (tower is ArcherTower) 
            DisplayPriceForTower(tower, GameConstants.PRICES_FOR_ARCHER);
        else if (tower is CannonTower)
            DisplayPriceForTower(tower, GameConstants.PRICES_FOR_CANNON); // do something, it shouldn`t be emplty
        else if (tower is MagicTower) 
            DisplayPriceForTower(tower, GameConstants.PRICES_FOR_MAGIC);
    }

    void DisplayPriceForTower(Tower tower, int[] priceArray)
    {
        TowerState state = tower.tState;
        int stateNum = (int)state - 2;

        if(state != TowerState.LEVEL_4A && state != TowerState.LEVEL_4B) 
            upgradePrice.text = "" + priceArray[stateNum + 1];

        destroyPrice.text = "" + priceArray[stateNum] * 0.8f;

        if(state == TowerState.LEVEL_3)
            upgrade4BPrice.text = "" + priceArray[4];
    }

   

    private void EnableTheButtons(Tower tower)
    {
        if (tower.tState == TowerState.LEVEL_3)
        {
            upgradeToLevel4Bbutton.SetActive(true);
            upgradeButton.SetActive(true);
        }
        else if (tower.tState == TowerState.LEVEL_4A || tower.tState == TowerState.LEVEL_4B)
        {
            upgradeToLevel4Bbutton.SetActive(false);
            upgradeButton.SetActive(false);
        }
        else
        {
            upgradeButton.SetActive(true);
            upgradeToLevel4Bbutton.SetActive(false);
        }
    }

    public IEnumerator DisableUpgradeButtons()
    {
        changeButtonsActive = false;
        changeButtons.GetComponent<Animator>().SetBool("hide", true);
        yield return new WaitForSeconds(0.55f);
        changeButtons.SetActive(false);
    }


    public void ShowBuildButtons(Vector3 buildPos)
    {
        buildButtonsActive = true;
        Vector3 canvasPos = Camera.main.WorldToScreenPoint(buildPos);
        buildButtons.transform.position = canvasPos;
        buildButtons.SetActive(true);
        buildButtons.GetComponent<Animator>().SetBool("hide", false);
        SetPricesForBuild();
    }

    void SetPricesForBuild()
    {
        buildArcherPrice.text = GameConstants.PRICES_FOR_ARCHER[0].ToString();
        buildMagicPrice.text = GameConstants.PRICES_FOR_MAGIC[0].ToString();
        buildCannonPrice.text = GameConstants.PRICES_FOR_CANNON[0].ToString();
    }

    public IEnumerator DisableBuildButtons()
    {
        buildButtonsActive = false;
        buildButtons.GetComponent<Animator>().SetBool("hide", true);
        yield return new WaitForSeconds(0.55f);
        buildButtons.SetActive(false);
    }


}
