using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLevel : MonoBehaviour
{
    public Sprite CurrentRoom;
    public Sprite BossRoom;
    public Sprite ShopRoom;
    public Sprite TreasureRoom;
    public Sprite DefaultRoom;
    public Sprite UnexploredRoom;

    public List<RoomData> rooms = new List<RoomData>();

    public int mapSize = 20, treasureCount = 0, bossCount = 0, shopCount = 0;

    void Start()
    {
        Level.current = CurrentRoom;
        Level.boss = BossRoom;
        Level.shop = ShopRoom;
        Level.treasure = TreasureRoom;
        Level.discovered = DefaultRoom;
        Level.unexplored = UnexploredRoom;

        RoomData startRoom = new RoomData();
        startRoom.location = new Vector2(0, 0);
        startRoom.roomSprite = Level.current;
        startRoom.isCurrent = true;
        startRoom.isDiscovered = true;
        DrawRoomOnMap(startRoom);

        DrawMap(startRoom);
    }

    private bool CheckIfRoomExists(Vector2 location)
    {
        return rooms.Exists(x => x.location == location);
    }

    private bool CheckIfRoomsAround(Vector2 location, string direction)
    {
        switch (direction)
        {
            case "up":
                return rooms.Exists(x => x.location == location + new Vector2(0, 1)) &&
                    rooms.Exists(x => x.location == location + new Vector2(1, 0)) &&
                    rooms.Exists(x => x.location == location + new Vector2(-1, 0));
            case "down":
                return rooms.Exists(x => x.location == location + new Vector2(0, -1)) &&
                    rooms.Exists(x => x.location == location + new Vector2(1, 0)) &&
                    rooms.Exists(x => x.location == location + new Vector2(-1, 0));
            case "left":
                return rooms.Exists(x => x.location == location + new Vector2(-1, 0)) &&
                    rooms.Exists(x => x.location == location + new Vector2(0, 1)) &&
                    rooms.Exists(x => x.location == location + new Vector2(0, -1));
            case "right":
                return rooms.Exists(x => x.location == location + new Vector2(1, 0)) &&
                    rooms.Exists(x => x.location == location + new Vector2(0, 1)) &&
                    rooms.Exists(x => x.location == location + new Vector2(0, -1));
            default:
                return false;
        }
    }

    private void DrawMap(RoomData room)
    {
        for (int i = 0; i < 4; i++)
        {
            if (Random.value > Level.roomGenerationChance)
            {
                RoomData newRoom = new RoomData();
                switch (i)
                {
                    case 0:
                        if (!CheckIfRoomExists(room.location + new Vector2(0, 1)))
                        {
                            newRoom.location = room.location + new Vector2(0, 1);
                        }
                        break;
                    case 1:
                        if (!CheckIfRoomExists(room.location + new Vector2(0, -1)))
                        {
                            newRoom.location = room.location + new Vector2(0, -1);
                        }
                        break;
                    case 2:
                        if (!CheckIfRoomExists(room.location + new Vector2(-1, 0)))
                        {
                            newRoom.location = room.location + new Vector2(-1, 0);
                        }
                            break;
                    case 3:
                        if (!CheckIfRoomExists(room.location + new Vector2(1, 0)))
                        {
                            newRoom.location = room.location + new Vector2(1, 0);
                        }
                        break;
                }
                if (Random.value < Level.treasureGenerationChance && treasureCount != 1)
                {
                    newRoom.isTreasure = true;
                    treasureCount++;
                }
                else if (Random.value < Level.shopGenerationChance && shopCount != 1)
                {
                    newRoom.isShop = true;
                    shopCount++;
                }
                else if (Random.value < Level.bossGenerationChance && bossCount != 1)
                {
                    newRoom.isBoss = true;
                    bossCount++;
                }
                newRoom.roomSprite = Level.unexplored;
                rooms.Add(newRoom);
                if (rooms.Count < mapSize)
                {
                    DrawRoomOnMap(newRoom);
                    DrawMap(newRoom);
                }
            }
        }
    }

    private void DrawRoomOnMap(RoomData room)
    {
        GameObject MapTile = new GameObject("MapTile");
        Image RoomImage = MapTile.AddComponent<Image>();
        RoomImage.sprite = room.roomSprite;
        RectTransform rectTransform = RoomImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Level.height, Level.width) * Level.iconScale;
        rectTransform.position = room.location * (Level.iconScale * Level.height * Level.scale + (Level.padding * Level.height * Level.scale));
        RoomImage.transform.SetParent(transform, false);
    }
}
