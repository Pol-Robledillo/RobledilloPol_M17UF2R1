using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width, height, xPosition, yPosition;
    public Door leftDoor, rightDoor, topDoor, bottomDoor;
    public List<Door> doors = new List<Door>();
    // Start is called before the first frame update
    void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("RoomController is null");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();

        foreach (Door d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
            {
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        }

        RoomController.instance.RegisterRoom(this);
    }
    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.left:
                    break;
                case Door.DoorType.right:
                    break;
                case Door.DoorType.top:
                    break;
                case Door.DoorType.bottom:
                    break;
            }
        }
    }
    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(xPosition + 1, yPosition))
        {
            return RoomController.instance.FindRoom(xPosition + 1, yPosition);
        }
        return null;
    }
    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(xPosition - 1, yPosition))
        {
            return RoomController.instance.FindRoom(xPosition - 1, yPosition);
        }
        return null;
    }
    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(xPosition, yPosition + 1))
        {
            return RoomController.instance.FindRoom(xPosition, yPosition + 1);
        }
        return null;
    }
    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(xPosition, yPosition - 1))
        {
            return RoomController.instance.FindRoom(xPosition, yPosition - 1);
        }
        return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
    public Vector3 GetRoomCenter()
    {
        return new Vector3(xPosition * width, yPosition * height);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
