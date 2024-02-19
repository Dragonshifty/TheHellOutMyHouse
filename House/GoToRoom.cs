using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToRoom : MonoBehaviour, IActivate
{
    NavMeshAgent investigator;    
    HouseInfo houseInfo;

    Dictionary <string, Transform> roomWaypoints;
    private void Start() 
    {
        investigator = GetComponent<NavMeshAgent>();  
        houseInfo = FindObjectOfType<HouseInfo>();
        if (houseInfo != null)
        {
            roomWaypoints = houseInfo.GetRoomWaypoints();
        }
        else
        {
            Debug.LogError("HouseInfo component is null!");
        }

    }


    public void DoYourThing(string name, string room)
    {
        MoveToDestination(room);
    }

    private void MoveToDestination(string waypointName)
    {
        // Debug.Log(waypointName);
        Vector3 destination = roomWaypoints[waypointName].position;
        investigator.destination = destination;

        StartCoroutine(CheckForDestinationReached(destination));
    }

    private IEnumerator CheckForDestinationReached(Vector3 destination)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            if (CheckDistance(destination) < 1.5f)
            {
                DestinationReached();
                yield break;
            }
        }
    }

    private float CheckDistance(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination);
    }

    private void DestinationReached()
    {
        EventManager.FinishedTask(gameObject);
    }

}