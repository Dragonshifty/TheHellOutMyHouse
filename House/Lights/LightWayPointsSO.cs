using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Light WayPoints", fileName = "New Light WayPoints")]
public class LightWayPointsSO : ScriptableObject
{
    [SerializeField] Transform lightWayPointsPrefab;

    public List<Transform> GetLightWayPoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach(Transform child in lightWayPointsPrefab)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }
}
