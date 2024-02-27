using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SearchRoom : MonoBehaviour, IActivate
{
    List<Transform> roomWaypoints;
    NavMeshAgent investigator;
    private Coroutine currentCoroutine;
    private int counter;


    private void Start() 
    {
        investigator = GetComponent<NavMeshAgent>();
    }
    public void DoYourThing(string name, string room)
    {
        StartSearch();
    }

    public void StartSearch()
    {
        counter = UnityEngine.Random.Range(3, 10);
        RoomKnowledge roomKnowledge = GetComponent<RoomKnowledge>();
        roomWaypoints = roomKnowledge.GetRoomPoints();
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
        investigator.destination = destination;

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

    private float CheckDistance(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination);
    }

    private void DestinationReached()
    {
        counter--;
        if (counter > 0)
        {
            // MoveToDestination();
            currentCoroutine = StartCoroutine(HoldPlease());
        } else
        {
            EventManager.FinishedTask(gameObject);
        }
    }

    private IEnumerator HoldPlease()
    {
        int holdtime = UnityEngine.Random.Range(1, 5);
        yield return new WaitForSeconds(holdtime);
        MoveToDestination();
    }

    public void CancelAll()
    {
        if (currentCoroutine != null)
        {
            StopAllCoroutines();
            currentCoroutine = null;
        }
    }
}
