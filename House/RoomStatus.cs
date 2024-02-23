using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class RoomStatus : MonoBehaviour
{
    private bool lightsOn;
    private float temperature;
  
    
    [SerializeField] Transform lightswitch;
    [SerializeField] Transform hidingSpot;
    [SerializeField] List<Transform> waypoints;

   
    public bool LightsOn
    { 
        get { return lightsOn; } 
        set { lightsOn = value; } 
    }

    public float Temperature
    { 
        get { return temperature; } 
        set { temperature = value; } 
    }

    public Transform GetLightswitchLocation()
    {
        return lightswitch;
    }

    public Transform GetHidingSpotLocation()
    {
        return hidingSpot;
    }



    public List<Transform> GetWaypoints()
    {

        List<Transform> waypointsList = new List<Transform>();
        foreach (Transform child in waypoints)
        {
            waypointsList.Add(child);
        }
        // Debug.Log("Got Waypoints from Room Status" + waypoints.Count);
        return waypointsList;
    }


}
