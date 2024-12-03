using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    public string currentWorldName;
    public RoomInfo currentLoadRoomData;
    public Room currentRoom;
    public Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();
    public bool isLoadingRoom = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadRoom("StartRoom", 0, 0);
        LoadRoom("EmptyRoom", 1, 0);
        LoadRoom("EmptyRoom", -1, 0);
        LoadRoom("EmptyRoom", 0, 1);
        LoadRoom("EmptyRoom", 0, -1);
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
            return;
        }
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
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

    }
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.xPosition == x && item.yPosition == y) != null;
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;
    }
}
