using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    public string currentWorldName;
    public RoomInfo currentLoadRoomData;
    public Room currentRoom;
    public Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();
    public bool isLoadingRoom = false;
    public bool spawnedBossRoom = false;
    public bool updatedRooms = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentWorldName = SceneManager.GetActiveScene().name;
    }
    private void Update()
    {
        UpdateRoomQueue();
    }
    private void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }
        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            return;
        }
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }
    private IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.xPosition, bossRoom.yPosition);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.xPosition == tempRoom.xPosition && r.yPosition == tempRoom.yPosition);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("EndRoom", tempRoom.xPosition, tempRoom.yPosition);
        }
    }
    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.x = x;
        newRoomData.y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }
    public IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }
    public void RegisterRoom(Room room)
    {
        if(!DoesRoomExist(currentLoadRoomData.x, currentLoadRoomData.y))
        {
            room.transform.position = new Vector3(room.width * currentLoadRoomData.x, room.height * currentLoadRoomData.y, 0);
            room.xPosition = currentLoadRoomData.x;
            room.yPosition = currentLoadRoomData.y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.xPosition + ", " + room.yPosition;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currentRoom = room;
            }

            loadedRooms.Add(room);
            if (currentLoadRoomData.name == "EndRoom")
            {
                RemoveDoors();
            }
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }
    public void RemoveDoors()
    {
        foreach(Room room in loadedRooms)
        {
            Debug.Log("Removing doors");
            room.RemoveUnconnectedDoors();
        }
    }
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.xPosition == x && item.yPosition == y) != null;
    }
    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.xPosition == x && item.yPosition == y);
    }
    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[]
        {
            "EmptyRoom",
            "BasicRoom"
        };
        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;
    }
}
