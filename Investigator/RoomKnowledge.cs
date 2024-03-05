using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RoomKnowledge : MonoBehaviour
{
    RoomStatus room;
    List<string> roomNames;

    private string roomStatusName = "RoomStatus";

    void Start()
    {
        InitialiseRoomStatus();
    }

    private void InitialiseRoomStatus()
    {
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        roomNames = new List<string>();

        foreach (GameObject obj in allGameObjects)
        {
            if (obj.GetComponent(roomStatusName) != null)
            {
                roomNames.Add(obj.gameObject.tag);
            }
        }
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
