using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowerManager : MonoBehaviour
{
    Tower selectedTower;
    ButtonTowersLogic towerButtons;

    BuildTowerManager buildTowerManager;

    bool isInTransition;

    AudioSource audio;

    AttackRadiusDisplay attackRadiusDisplay;

    // Start is called before the first frame update
    void Start()
    {
        attackRadiusDisplay = GetComponentInChildren<AttackRadiusDisplay>();
        audio = GetComponent<AudioSource>();
        buildTowerManager = FindObjectOfType<BuildTowerManager>();
        towerButtons = FindObjectOfType<ButtonTowersLogic>();
    }

    public void SelectTower(Tower tower)
    {
        if (isInTransition) return;
        if(selectedTower != null)
        {
            DeselectTower();
            return;
        }
        
        selectedTower = tower;
        attackRadiusDisplay.EnableRadius(selectedTower.transform.position, selectedTower.AttackRadius);
        towerButtons.ShowChangeButtons(tower);
        if (towerButtons.buildButtonsActive) buildTowerManager.DeselectWaypoint();
        StartCoroutine(DisableUIAppears());
    }

    public void DeselectTower()
    {
        if (selectedTower == null) return;
        if (isInTransition == true) return;
        attackRadiusDisplay.DisableRadius();
        StartCoroutine(DisableUI());
        selectedTower = null;
    }

    IEnumerator DisableUI()
    {
        isInTransition = true;
        yield return StartCoroutine(towerButtons.DisableUpgradeButtons());
        isInTransition = false;
    }

    IEnumerator DisableUIAppears()
    {
        isInTransition = true;
        yield return new WaitForSeconds(0.5f);
        isInTransition = false;
    }


    public void UpgradeSelectedTower()
    {
        if (selectedTower == null) return;
        if (isInTransition) return;
        audio.Play();

        StartCoroutine(towerButtons.DisableUpgradeButtons());
        attackRadiusDisplay.DisableRadius();
        // choose how to upgrade
        if (selectedTower.tState == TowerState.LEVEL_1)
            StartCoroutine(selectedTower.UpgradeToLevel_2());
        if (selectedTower.tState == TowerState.LEVEL_2)
            StartCoroutine(selectedTower.UpgradeToLevel_3());
        if (selectedTower.tState == TowerState.LEVEL_3)
            StartCoroutine(selectedTower.UpgradeToLevel_4A());
        selectedTower = null;
    }

    public void UpgradeToLevel_4B()
    {
        if (isInTransition) return;
        if (selectedTower == null) return;
        if (selectedTower.tState == TowerState.LEVEL_3)
            StartCoroutine(selectedTower.UpgradeToLevel_4B());
        StartCoroutine(towerButtons.DisableUpgradeButtons());
        selectedTower = null;
    }

    public void DestroySelectedTower()
    {
        if (isInTransition) return;
        if (selectedTower == null) return;
        buildTowerManager.DecreaseTowerAmount();
        audio.Play();
        StartCoroutine(selectedTower.DestroyTower());
        StartCoroutine(towerButtons.DisableUpgradeButtons());
        selectedTower = null;
    }



    
}
