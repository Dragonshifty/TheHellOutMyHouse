using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomKnowledge : MonoBehaviour
{
    RoomStatus room;
    List<string> roomNames;

    void Start()
    {
        roomNames = new List<string>
        {
            "Outside",
            "LivingRoom",
            "Kitchen",
            "Hallway",
            "Bedroom1",
            "Bedroom2"
        };
    }


    private void OnTriggerEnter(Collider other) 
    {
        string roomName = other.gameObject.tag;   

        foreach (string name in roomNames)
        {
            if (name.Equals(roomName))
            {
                room = other.GetComponent<RoomStatus>();
                EventManager.EnteredRoom(gameObject);
            }
        }
    }

    public string GetRoomName()
    {
        return room.gameObject.tag;
    }

    public bool GetLightStatus()
    {
        return room.LightsOn;
    }

    public void FlickLightswitch(bool onOff)
    {
        room.LightsOn = onOff;
    }

    public float GetRoomTemperature()
    {
        return room.Temperature;
    }
}
