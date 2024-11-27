using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level
{
    public static float width = 500;
    public static float height = 500;

    public static float scale = 1, iconScale = 0.1f, padding = 0.01f;

    public static float roomGenerationChance = 0.5f, treasureGenerationChance = 0.15f, shopGenerationChance = 0.15f, bossGenerationChance = 0.3f;

    public static Sprite discovered, unexplored, current, shop, boss, treasure;
}

public class RoomData
{
    public Vector2 location;
    public Sprite roomSprite;

    public bool isDiscovered = false;
    public bool isCurrent = false;
    public bool isShop = false;
    public bool isBoss = false;
    public bool isTreasure = false;
}