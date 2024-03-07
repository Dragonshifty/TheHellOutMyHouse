using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq;
using PersonalitySpace;
using UnityEngine;

public class RunToHidingSpot : MonoBehaviour, IActivate
{
    private NavMeshAgent investigatorAgent;
    private TravelTo travelTo;
    private HouseInfo houseInfo;
    private Personality personality;
    private InvestigatorState investigator;
    private int hidingLevel;
    private float shakeMagnitude = 0.1f;
    private float shakeDuration = 4f;
    private Dictionary<string, Transform> houseRoomWaypoints;
    private void Start() 
    {
        investigatorAgent = GetComponent<NavMeshAgent>();
        investigator = GetComponent<InvestigatorState>();
        houseInfo = FindObjectOfType<HouseInfo>();
        if (houseInfo != null)
        {
            houseRoomWaypoints = houseInfo.GetRoomWaypoints();
        }
        else
        {
            Debug.LogError("HouseInfo component is null!");
        }
    }

    public void DoYourThing(Transform position, string room)
    {
        if (personality == null)
        {
            personality = Coordination.GetRole(investigator);
            hidingLevel = personality.GetHidingLevel();
        }
        GetWaypoint(room);
    }

    private void GetWaypoint(string room)
    {
        if (room.Equals("Outside"))
        {
            TravelToWaypoint(houseRoomWaypoints["Outside"]);
        } else
        {
            GameObject roomObject = GameObject.FindGameObjectWithTag(room);
            RoomStatus roomStatus = roomObject.GetComponent<RoomStatus>();
            Transform waypoint = roomStatus.GetHidingSpotLocation();
            TravelToWaypoint(waypoint);
        }     
    }

    private void TravelToWaypoint(Transform waypoint)
    {
        #pragma warning disable 4014
        travelTo = new TravelTo(investigatorAgent, waypoint);
        travelTo.MoveToWaypoint(DestinationReached);
    }

    private void DestinationReached()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        Vector3 startingPosition = gameObject.transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float xPosition = startingPosition.x + Random.Range(-shakeMagnitude, +shakeMagnitude);
            float zPosition = startingPosition.z + Random.Range(-shakeMagnitude, +shakeMagnitude);

            Vector3 newPosition = new Vector3(xPosition, startingPosition.y, zPosition);

            investigatorAgent.SetDestination(newPosition);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Finished();
    }

    private void Finished()
    {
        StopAllCoroutines();
        EventManager.FinishedTask(gameObject);
    }

    public void CancelAll()
    {
        StopAllCoroutines();
        travelTo.StopNavigation();
    }
}
