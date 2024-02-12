using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInfo : MonoBehaviour
{
    [SerializeField] List<RoomWaypoints> roomList;
    private Dictionary<string, Transform> rooms;

    private void Awake() 
    {
        InitialiseRooms();    
    }

    private void InitialiseRooms()
    {
        rooms = new Dictionary<string, Transform>();

        foreach (var entry in roomList)
        {
            rooms.Add(entry.roomName, entry.roomWaypoint);
        }
    }

    public Dictionary<string, Transform> GetRoomWaypoints()
    {
        return rooms;
    }
}
