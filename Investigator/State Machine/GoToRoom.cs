using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToRoom : MonoBehaviour, IActivate
{
    NavMeshAgent investigatorAgent;    
    private TravelTo travelTo;
    HouseInfo houseInfo;
    Coroutine currentCoroutine;

    Dictionary <string, Transform> roomWaypoints;
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
        travelTo = new TravelTo(investigatorAgent, roomWaypoints[room].transform);
        travelTo.MoveToWaypoint(DestinationReached);
    }

    private void DestinationReached()
    {
        EventManager.FinishedTask(gameObject);
    }

    public void CancelAll()
    {
        StopAllCoroutines();
        travelTo.StopNavigation();
    }
}
