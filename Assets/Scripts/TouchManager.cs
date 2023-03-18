using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    BuildTowerManager buildManager;
    UpgradeTowerManager upgradeManager;

    Camera mainCamera;

    float maxDelay = 0.2f;
    float timer = 0;

    [SerializeField] LayerMask touchableLayer;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        buildManager = FindObjectOfType<BuildTowerManager>();
        upgradeManager = FindObjectOfType<UpgradeTowerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (timer < 0.2f)
            {
                SelectObject();
            }

            timer = 0;
        }


    }

    void SelectObject()
    {
        Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool isTouched = Physics.Raycast(mouseRay, out hitInfo, 1000, touchableLayer);
        if (!isTouched)
        {
            buildManager.DeselectWaypoint();
            upgradeManager.DeselectTower();
            return;
        }
        Waypoint touchedWaypoint = hitInfo.transform.GetComponent<Waypoint>();
        if (touchedWaypoint)
        {
            SelectWaypoint(touchedWaypoint);
            return;
        }
        Tower touchedTower = hitInfo.transform.GetComponent<Tower>();
        if (touchedTower)
        {
            SelectTower(touchedTower);
        }

    }

    void SelectTower(Tower touchedTower)
    {
        if (touchedTower.tState == TowerState.BUILDING || touchedTower.tState == TowerState.DESTROYING)
            return;
        upgradeManager.SelectTower(touchedTower);
    }

    void SelectWaypoint(Waypoint touchedWaypoint)
    {
        if (touchedWaypoint.towerOnPoint) return;
        buildManager.SelectWaypoint(touchedWaypoint);
    }

    
}
