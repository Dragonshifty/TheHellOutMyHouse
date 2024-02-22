using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SearchRoom : MonoBehaviour, IActivate
{
    List<Transform> roomWaypoints;
    NavMeshAgent investigator;


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
        RoomKnowledge roomKnowledge = GetComponent<RoomKnowledge>();
        roomWaypoints = roomKnowledge.GetRoomPoints();
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        int index = UnityEngine.Random.Range(0, roomWaypoints.Count);
        // Debug.Log(index + " waypoints");
        if (roomWaypoints.Count == 0)
        {
            Debug.LogError("No waypoints available.");
            // return Vector3.zero; // Or any default position you prefer
        }
        Vector3 destination = roomWaypoints[index].position;
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
        // Debug.Log("Reached");
        MoveToDestination();
        // EventManager.FinishedTask(gameObject);
    }
}
