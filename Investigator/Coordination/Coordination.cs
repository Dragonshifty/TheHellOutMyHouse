using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordination 
{
    public static Dictionary<string, bool> roomsSearched = new Dictionary<string, bool>
    {
        { "Outside", true },
        { "LivingRoom", false },
        { "Hallway", false },
        { "Kitchen", false },
        { "Bedroom1", false },
        { "Bedroom2", false }
    };

    public static bool HasRoomBeenSearch(string room)
    {
        if (roomsSearched.ContainsKey(room))
        {
            return roomsSearched[room];
        } else
        {
            Debug.LogError ("Can't check if room searched, key error");
            return false;
        }
    }

    public static void SetRoomSearched(string room, bool searched)
    {
        if (roomsSearched.ContainsKey(room))
        {
            roomsSearched[room] = searched;
        } else
        {
            Debug.LogError("Can't set room searched, key error");
        }
    }
}
