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

    private void Update() {
        if (Input.GetKey(KeyCode.S))
        {
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = null;
            Debug.Log("Stopped");
        }
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
        // Debug.Log(index + " waypoints");
        if (roomWaypoints.Count == 0)
        {
            Debug.LogError("No waypoints available.");
            // return Vector3.zero; // Or any default position you prefer
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
}
