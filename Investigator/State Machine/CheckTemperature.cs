using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckTemperature : MonoBehaviour, IActivate
{


    List<Transform> roomWaypoints;
    NavMeshAgent investigatorAgent;
    RoomKnowledge roomKnowledge;
    private Coroutine currentCoroutine;
    private int counter;


    private void Start()
    {
        investigatorAgent = GetComponent<NavMeshAgent>();
        roomKnowledge = GetComponent<RoomKnowledge>();
    }
    public void DoYourThing(Transform position, string room)
    {
        StartSearch();
    }

    public void StartSearch()
    {
        counter = UnityEngine.Random.Range(3, 10);  
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
        }
        else
        {
            EventManager.FinishedTask(gameObject);
        }
    }

    private IEnumerator HoldPlease()
    {
        int holdtime = UnityEngine.Random.Range(1, 5);
        Debug.Log(roomKnowledge.GetRoomName() + " is at " +roomKnowledge.GetRoomTemperature() + "°");
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


