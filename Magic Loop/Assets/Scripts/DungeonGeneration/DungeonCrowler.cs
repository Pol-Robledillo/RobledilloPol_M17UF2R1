using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrowler : MonoBehaviour
{
    public Vector2Int position { get; set; }
    public DungeonCrowler(Vector2Int position)
    {
        this.position = position;
    }
    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        Direction toMove = (Direction)Random.Range(0, directionMovementMap.Count);
        position += directionMovementMap[toMove];
        return position;
    }
}
