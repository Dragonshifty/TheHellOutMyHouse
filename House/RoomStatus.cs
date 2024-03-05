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
    
    private bool raise;
    private bool lower;

    GameObject player;
    // BoxCollider collider;
    Vector3 colliderCenter;

    public bool Raise
    {
        set { raise = value; }
    }

    public bool Lower
    {
        set { lower = value; }
    }
    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // collider = GetComponent<BoxCollider>();
        // colliderCenter = collider.bounds.center;
    }

    // private void Update() 
    // {
    //     RaycastHit hit;
    //     if (Physics.Raycast(player.transform.position, colliderCenter - player.transform.position, out hit))
    //     {
    //         if (hit.collider == collider)
    //         {
    //             // Player is inside the collider
    //             // room.ClearCoroutines();
    //             // LowerTemp();
    //             Debug.Log(gameObject.tag);
    //         }
    //         else
    //         {
    //             // Player is not inside the collider
    //             // room.ClearCoroutines();
    //             // RaiseTemp();
    //         }
    //     }
    // }
   
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

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            ClearCoroutines();
            lower = true;
            raise = false;
            StartCoroutine(LowerTemperature());
        }    
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            ClearCoroutines();
            lower = false;
            raise = true;
            StartCoroutine(RestoreTemperature());
        }
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
        return waypointsList;
    }

    
    public IEnumerator LowerTemperature()
    {
        raise = false;
        lower = true;
        while (temperature > -2 && lower)
        {
            yield return new WaitForSeconds(.1f);
            temperature--;
        }
    }

    public IEnumerator RestoreTemperature()
    {
        // Debug.Log("Restoring");
        raise = true;
        lower = false;
        while (temperature < 16 && raise)
        {
            yield return new WaitForSeconds(1);
            temperature++;
        }
    }

    public void ClearCoroutines()
    {
        StopAllCoroutines();
    }
}
