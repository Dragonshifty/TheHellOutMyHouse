using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightswitchInfo : MonoBehaviour
{
    [SerializeField] List<LightswitchWaypoints> lightswitchList;
    private Dictionary<string, Transform> lightswitches;

    private void Awake()
    {
        InitialiseRooms();
    }
    private void InitialiseRooms()
    {
        lightswitches = new Dictionary<string, Transform>();

        foreach (var entry in lightswitchList)
        {
            lightswitches.Add(entry.lightswitchName, entry.waypoint);
        }
    }

    public Dictionary<string, Transform> GetLightswitchWaypoints()
    {
        return lightswitches;
    }
}
