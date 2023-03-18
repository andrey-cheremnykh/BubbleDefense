using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid;
    Queue<Waypoint> searchQueue;

    [SerializeField] Waypoint startPoint, endPoint;

    bool isFound;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Dictionary<Vector2Int, Waypoint>();
        searchQueue = new Queue<Waypoint>();

        startPoint.towerOnPoint = gameObject;// bug with towers loop might be here...
    }  

    void FillTheGrid()
    {
        grid.Clear();
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].isExplored = false;
            Vector2Int wPos = waypoints[i].gridPosition;
            if (grid.ContainsKey(wPos))
            {
                Debug.LogWarning("The Tile: "+ wPos + "is duplicated");
                continue;
            }
            if (waypoints[i].towerOnPoint != null) continue;
            grid.Add(wPos, waypoints[i]);
            
        }

    }

    public bool CheckThePath(Waypoint buildPoint)
    {
        FillTheGrid();
        grid.Remove(buildPoint.gridPosition);
        isFound = false;
        searchQueue.Clear();
        searchQueue.Enqueue(startPoint);
        startPoint.isExplored = true;

        while (isFound != true && searchQueue.Count > 0)
        {
            Waypoint sPoint = searchQueue.Dequeue();

            ExploreNeigbours(sPoint);
        }

        return isFound;
    }


    public List<Vector3> FindPath()
    {
        FillTheGrid();
        isFound = false;
        searchQueue.Clear();
        List<Vector3> path = new List<Vector3>();
        searchQueue.Enqueue(startPoint);
        startPoint.isExplored = true;

        while (isFound != true && searchQueue.Count > 0)
        {
            Waypoint sPoint = searchQueue.Dequeue();

            ExploreNeigbours(sPoint);
        }

        if (isFound == false) return null;

        Waypoint pointer = endPoint;

        while(pointer != null)
        {
            path.Add(pointer.transform.position);
            pointer = pointer.fromPoint;
        }
        Vector3 spawnPos = transform.GetChild(0).position;
        path.Add(spawnPos);
        path.Reverse();
        return path;
    }

    void ExploreNeigbours(Waypoint point)
    {
        Vector2Int pos = point.gridPosition;
        if (grid.ContainsKey(pos + Vector2Int.up)) ExploreThePoint(pos + Vector2Int.up, point);
        if (grid.ContainsKey(pos + Vector2Int.right)) ExploreThePoint(pos + Vector2Int.right, point);
        if (grid.ContainsKey(pos + Vector2Int.down)) ExploreThePoint(pos + Vector2Int.down, point);
        if (grid.ContainsKey(pos + Vector2Int.left)) ExploreThePoint(pos + Vector2Int.left, point);

    }

    void ExploreThePoint(Vector2Int pos, Waypoint fromPoint)
    {
        Waypoint myPoint = grid[pos];
        if (myPoint.isExplored) return;

        myPoint.isExplored = true;
        myPoint.fromPoint = fromPoint;

        if(myPoint == endPoint)
        {
            isFound = true;
            return;
        }

        searchQueue.Enqueue(myPoint);
    }


}
