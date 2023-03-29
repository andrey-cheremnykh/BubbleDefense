using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorCube : MonoBehaviour
{
    int gridscale = 10;
    [SerializeField] bool displayName = true;

    // Update is called once per frame
    void Update()
    {
        SetToGrid();
        if(displayName) LabelCube();
    }

    void SetToGrid()
    {
        int x = Mathf.RoundToInt(transform.position.x / gridscale) * gridscale;
        int z = Mathf.RoundToInt(transform.position.z / gridscale) * gridscale;

        transform.position = new Vector3(x, 0, z);
    }

    void LabelCube()
     {
        int x = Mathf.RoundToInt(transform.position.x / gridscale);
        int y = Mathf.RoundToInt(transform.position.z / gridscale);
        gameObject.name = $"Tile ({x}, {y})";
    }

}
