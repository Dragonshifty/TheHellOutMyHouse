using System.Collections;
using System.Collections.Generic;
using PersonalitySpace;
using UnityEngine;
using UnityEngine.AI;

public class FindHidingSpot : MonoBehaviour, IActivate
{
    NavMeshAgent investigatorAgent;
    private TravelTo travelTo;
    InvestigatorState investigator;
    List<Transform> roomWaypoints;
    Transform hidingSpot;
    Coroutine currentCoroutine;
    Personality personality;
    private int hidingLevel;
    int counter;

    private void Start()
    {
        investigator = GetComponent<InvestigatorState>();
        investigatorAgent = GetComponent<NavMeshAgent>();
    }

    public void CancelAll()
    {
        StopAllCoroutines();
        if (currentCoroutine != null) currentCoroutine = null;
        travelTo.StopNavigation();
    }

    public void DoYourThing(Transform position, string room)
    {
        StartSearch();
    }

    public void StartSearch()
    {
        counter = UnityEngine.Random.Range(3, 10);
        RoomKnowledge roomKnowledge = GetComponent<RoomKnowledge>();
        roomWaypoints = roomKnowledge.GetRoomPoints();
        hidingSpot = roomKnowledge.GetHidingSpot();
        if (personality != null)
        {
            personality = Coordination.GetRole(investigator);
            hidingLevel = personality.GetHidingLevel();
        }
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        int index = UnityEngine.Random.Range(0, roomWaypoints.Count);

        if (roomWaypoints.Count == 0)
        {
            Debug.LogError("No waypoints available.");
        }

        TravelToWaypoint(roomWaypoints[index]);
    }

    private void TravelToWaypoint(Transform target)
    {
        #pragma warning disable 4014
        travelTo = new TravelTo(investigatorAgent, target);
        travelTo.MoveToWaypoint(DestinationReached);
    }

    private void DestinationReached()
    {
        counter--;
        if (counter > 0)
        {
            currentCoroutine = StartCoroutine(HoldPlease());
        }
        else
        {
            EventManager.FinishedTask(gameObject);
        }
    }

    private float CheckDistance(Vector3 target)
    {
        return Vector3.Distance(transform.position, target);
    }

    private IEnumerator HoldPlease()
    {
        int holdtime = UnityEngine.Random.Range(1, 3);
        yield return new WaitForSeconds(holdtime);
        if (FindSpot())
        {
            CancelAll();
            EventManager.FoundHidingSpot(gameObject);
            EventManager.FinishedTask(gameObject);
            yield break;
        } else
        {      
            MoveToDestination();
            yield break;
        }       
    }

    private bool FindSpot()
    {
        int chance = UnityEngine.Random.Range(0, 11);
        // if (CheckDistance(hidingSpot.position) < 1f) return true;
        if (CheckDistance(hidingSpot.position) < 1f && chance <= hidingLevel) return true;
        return false;
    }
}
