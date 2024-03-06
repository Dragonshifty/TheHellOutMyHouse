using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GrabEvidence : MonoBehaviour, IActivate
{
    NavMeshAgent investigatorAgent;
    HouseInfo houseInfo;
    Coroutine currentCoroutine;
    TravelTo travelTo;

    Dictionary<string, Transform> roomWaypoints;
    
    private void Start()
    {
        investigatorAgent = GetComponent<NavMeshAgent>();
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


    public void DoYourThing(Transform position, string room)
    {
        TravelToWaypoint(room);
    }

    private void TravelToWaypoint(string room)
    {
        #pragma warning disable 4014
        travelTo = new TravelTo(investigatorAgent, roomWaypoints["Outside"].transform);
        travelTo.MoveToWaypoint(DestinationReached);
    }

    private void DestinationReached()
    {
        EventManager.CollectedGear(gameObject);
        EventManager.FinishedTask(gameObject);
    }

    public void CancelAll()
    {
        if (currentCoroutine != null)
        {
            StopAllCoroutines();
            currentCoroutine = null;
        }
        travelTo.StopNavigation();
    }
}
