using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseRoomReturn 
{
    public static Dictionary<string, bool> GetStarterHouseRooms ()
    {
        return new Dictionary<string, bool>
        {
            { "Outside", true },
            { "LivingRoom", false },
            { "Hallway", false },
            { "Kitchen", false },
            { "Bedroom1", false },
            { "Bedroom2", false }
        };
    }
}
