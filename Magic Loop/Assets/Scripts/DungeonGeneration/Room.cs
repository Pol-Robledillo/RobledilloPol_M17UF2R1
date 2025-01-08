using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width, height, xPosition, yPosition;
    public Door leftDoor, rightDoor, topDoor, bottomDoor;
    public List<Door> doors = new List<Door>();
    public Room(int x, int y)
    {
        xPosition = x;
        yPosition = y;
    }
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
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.left:
                    leftDoor = d;
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
                case Door.DoorType.right:
                    if (GetRight() == null)
                    {
                        door.gameObject.SetActive(true);
                        door.bridge.SetActive(false);
                    }
                    else
                    {
                        door.gameObject.SetActive(false);
                        door.bridge.SetActive(true);
                    }
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                    {
                        door.gameObject.SetActive(true);
                        door.bridge.SetActive(false);
                    }
                    else
                    {
                        door.gameObject.SetActive(false);
                        door.bridge.SetActive(true);
                    }
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                    {
                        door.gameObject.SetActive(true);
                        door.bridge.SetActive(false);
                    }
                    else
                    {
                        door.gameObject.SetActive(false);
                        door.bridge.SetActive(true);
                    }
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                    {
                        door.gameObject.SetActive(true);
                        door.bridge.SetActive(false);
                    }
                    else
                    {
                        door.gameObject.SetActive(false);
                        door.bridge.SetActive(true);
                    }
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
            Chaser[] enemies = GetComponentsInChildren<Chaser>();
            Shooter[] shooters = GetComponentsInChildren<Shooter>();
            foreach (Chaser c in enemies)
            {
                c.player = collision.gameObject;
                c.currentState = States.Chase;
            }
            foreach (Shooter s in shooters)
            {
                s.player = collision.gameObject;
                s.currentState = States.Attack;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
            Chaser[] enemies = GetComponentsInChildren<Chaser>();
            Shooter[] shooters = GetComponentsInChildren<Shooter>();
            foreach (Chaser c in enemies)
            {
                c.currentState = States.Idle;
            }
            foreach (Shooter s in shooters)
            {
                s.currentState = States.Idle;
            }
        }
    }
}
