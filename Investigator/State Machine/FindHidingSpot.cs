using System.Collections;
using System.Collections.Generic;
using PersonalitySpace;
using UnityEngine;
using UnityEngine.AI;

public class FindHidingSpot : MonoBehaviour, IActivate
{
    NavMeshAgent investigatorAgent;
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
        Vector3 destination = roomWaypoints[index].position;
        investigatorAgent.destination = destination;

        currentCoroutine = StartCoroutine(CheckForDestinationReached(destination));
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

    private void DestinationReached()
    {
        counter--;
        if (counter > 0)
        {
            // MoveToDestination();
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
