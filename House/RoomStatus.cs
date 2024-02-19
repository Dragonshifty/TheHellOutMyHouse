using System.Collections;
using System.Collections.Generic;
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
    { get; set; }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypointsList = new List<Transform>();
        foreach (Transform child in waypoints)
        {
            waypointsList.Add(child);
        }
        return waypointsList;
    }


}
