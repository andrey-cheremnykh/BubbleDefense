using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour
{
    public static readonly int gridscale = 10;

    public Waypoint fromPoint;
    public bool isExplored;

    public GameObject towerOnPoint;

    public Vector2Int gridPosition
    {
        get;
        private set;
    }

    void Start()
    {
        DefineGridPosition();
        
    }

    void DefineGridPosition()
    {
        int x = Mathf.RoundToInt(transform.position.x / gridscale);
        int y = Mathf.RoundToInt(transform.position.z / gridscale);
        gridPosition = new Vector2Int(x, y);
    }


  /*  private void OnMouseDown()
    {
        if (towerOnPoint != null) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        BuildTowerManager towerManager = FindObjectOfType<BuildTowerManager>();
        towerManager.SelectWaypoint(this);

    }*/
}
