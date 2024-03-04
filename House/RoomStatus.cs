using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class RoomStatus : MonoBehaviour
{
    private bool lightsOn;
    [SerializeField] float temperature = 16;
  
    
    [SerializeField] Transform lightswitch;
    [SerializeField] Transform hidingSpot;
    [SerializeField] List<Transform> waypoints;

    GameObject player;
    BoxCollider collider;
    Vector3 colliderCenter;
    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        collider = GetComponent<BoxCollider>();
        colliderCenter = collider.bounds.center;
    }

    private void Update() 
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, colliderCenter - player.transform.position, out hit))
        {
            if (hit.collider == collider)
            {
                // Player is inside the collider
                // room.ClearCoroutines();
                // LowerTemp();
                Debug.Log(gameObject.tag);
            }
            else
            {
                // Player is not inside the collider
                // room.ClearCoroutines();
                // RaiseTemp();
            }
        }
    }
   
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

    
    public IEnumerator LowerTemperature()
    {
        while (temperature > -2)
        {
            yield return new WaitForSeconds(4);
            temperature--;
        }
    }

    public IEnumerator RestoreTemperature()
    {
        Debug.Log("Restoring");
        while (temperature <= 16)
        {
            yield return new WaitForSeconds(4);
            temperature++;
        }
    }

    public void ClearCoroutines()
    {
        StopAllCoroutines();
    }
}
