using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }
    public Room room;
    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();
    public void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.width - 2;
        grid.rows = room.height - 4;
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;

        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.verticalOffset), y - (grid.rows - grid.horizontalOffset));
                go.name = "X: " + x + ", Y: " + y;
                availablePoints.Add(go.transform.position);
                go.SetActive(false);
            }
        }

        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();
    }
}
