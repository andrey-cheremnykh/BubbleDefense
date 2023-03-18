using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildTowerManager : MonoBehaviour
{
    [SerializeField] GameObject archerTowerPrefab;
    [SerializeField] GameObject cannonTowerPrefab;
    [SerializeField] GameObject magicTowerPrefab;

    [SerializeField] GameObject selectVFX;

    [SerializeField] TMP_Text towerCapacityText;

    Waypoint selectedWaypoint;

    Pathfinder pathfinder;

    ButtonTowersLogic towerButtons;
    UpgradeTowerManager upgradeTowerManager;
    EnemySpawner spawner;
    AudioSource audio;

    bool isInTransition = false;

    int maxTowerCapacity = 5;
    int currentTowerAmount;

    public void DecreaseTowerAmount()
    {
        currentTowerAmount--;
        DisplayTowerCapacity();
    }

    void DisplayTowerCapacity()
    {
        towerCapacityText.text = $"{currentTowerAmount}/{maxTowerCapacity}";
    }

    void Start()
    {
        SetupUpgradesInLevel setup = FindObjectOfType<SetupUpgradesInLevel>();
        maxTowerCapacity = setup.TowerCount;
        audio = GetComponent<AudioSource>();
        audio.mute = GlobalAudioManager.instance.IsSoundMuted();
        upgradeTowerManager = FindObjectOfType<UpgradeTowerManager>();
        spawner = FindObjectOfType<EnemySpawner>();
        pathfinder = FindObjectOfType<Pathfinder>();
        towerButtons = FindObjectOfType<ButtonTowersLogic>();
        selectVFX.SetActive(false);
        DisplayTowerCapacity();
    }

    public void SelectWaypoint(Waypoint buildPoint)
    {
        if (towerButtons.changeButtonsActive) upgradeTowerManager.DeselectTower();
        if (spawner.IsSpawning == true) return;
        if (isInTransition == true) return;
        if (pathfinder.CheckThePath(buildPoint) == false) return;

        if(selectedWaypoint != null)
        {
            DeselectWaypoint();
        }
        else if(currentTowerAmount < maxTowerCapacity)
        {
            StartCoroutine(ActivateTransition());
            selectedWaypoint = buildPoint;
            selectVFX.transform.position = buildPoint.transform.position + new Vector3(0, 5.2f, 0);
            selectVFX.SetActive(true);
            towerButtons.ShowBuildButtons(selectVFX.transform.position);
            
        }
        /*GameObject towerClone = Instantiate(towerPrefab);
        towerClone.transform.position = buildPoint.transform.position + Vector3.up * 5;
        buildPoint.towerOnPoint = towerClone;
        // add some extra things*/
    }

    public void DeselectWaypoint()
    {
        if (selectedWaypoint == null) return;
        if (isInTransition == true) return;
        print("Begin Deselection Build");
        selectedWaypoint = null;
        selectVFX.SetActive(false);
        StartCoroutine(DisableUI());
    }

    IEnumerator ActivateTransition()
    {
        isInTransition = true;
        yield return new WaitForSeconds(0.4f);
        isInTransition = false;
    }

    IEnumerator DisableUI()
    {
        isInTransition = true;
        yield return StartCoroutine(towerButtons.DisableBuildButtons());
        isInTransition = false;
    }

    public void BuildArcherTower()
    {
        if (!CanBuildTower()) return;
        SetupTower(archerTowerPrefab, GameConstants.PRICES_FOR_ARCHER[0]);
    }

    public void BuildCannonTower()
    {
        if (!CanBuildTower()) return;
        SetupTower(cannonTowerPrefab, GameConstants.PRICES_FOR_CANNON[0]);
    }

    public void BuildMagicTower()
    {
        if (!CanBuildTower()) return;
        SetupTower(magicTowerPrefab, GameConstants.PRICES_FOR_MAGIC[0]);
    }

    bool CanBuildTower()
    {
        if (spawner.IsSpawning == true) return false;
        if (selectedWaypoint == null) return false;
        if (isInTransition) return false;
        if (currentTowerAmount >= maxTowerCapacity) return false;

        return true;
    }

    void SetupTower(GameObject towerPrefab, int price)
    {
        currentTowerAmount++;
        DisplayTowerCapacity();
        audio.Play();
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(price))
        {
            GameObject cloneTower = Instantiate(towerPrefab);
            cloneTower.transform.position = selectedWaypoint.transform.position + Vector3.up * 5;
            selectedWaypoint.towerOnPoint = cloneTower;
            cloneTower.GetComponent<Tower>().SetWaypointOn(selectedWaypoint);
        }
       
        StartCoroutine(DisableUI());
        selectedWaypoint = null;
        selectVFX.SetActive(false);
    }



}
