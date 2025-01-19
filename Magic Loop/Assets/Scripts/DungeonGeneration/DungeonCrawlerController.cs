using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
}

public class DungeonCrawlerController : MonoBehaviour
{
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
    public static readonly Dictionary<Direction, Vector2Int> DirectionMovementMap = new Dictionary<Direction, Vector2Int>()
    {
        {Direction.up, Vector2Int.up},
        {Direction.left, Vector2Int.left},
        {Direction.down, Vector2Int.down},
        {Direction.right, Vector2Int.right}
    };
    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrowler> dungeonCrowlers = new List<DungeonCrowler>();

        for (int i = 0; i < dungeonData.numberOfCrawlers; i++)
        {
            dungeonCrowlers.Add(new DungeonCrowler(Vector2Int.zero));
        }

        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);

        for (int i = 0; i < iterations; i++)
        {
            foreach (DungeonCrowler crowler in dungeonCrowlers)
            {
                Vector2Int newPosition = crowler.Move(DirectionMovementMap);
                positionsVisited.Add(newPosition);
            }
        }

        return positionsVisited;
    }
}