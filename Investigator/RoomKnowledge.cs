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

        // if (gameObject.tag.Equals("Player"))
        // {
        //     room.ClearCoroutines();
        //     Debug.Log(GetRoomName() + "Down");
        //     LowerTemp();
        // }
    }

    private void OnTriggerExit(Collider other) 
    {
        // if (gameObject.tag.Equals("Player"))
        // {
        //     Debug.Log(GetRoomName() + "Up");
        //     room.ClearCoroutines();
        //     RaiseTemp();
        // }    
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

    public Transform GetHidingSpot()
    {
        return room.GetHidingSpotLocation();
    }


    public List<Transform> GetRoomPoints()
    {
        return room.GetWaypoints();
    }

    public void RaiseTemp()
    {
        StartCoroutine(room.RestoreTemperature());
    }

    public void LowerTemp()
    {
        StartCoroutine(room.LowerTemperature());
    }
}
