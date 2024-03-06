using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvestigateEvent : MonoBehaviour, IActivate
{
    NavMeshAgent investigatorAgent;
    TravelTo travelTo;
    InvestigatorState investigator;
    Coroutine currentCoroutine;
    Vector3 currentSpot;
    Fear fear;
    private int counter;
    List<string> inventory;

    private void Start() 
    {
        investigatorAgent = GetComponent<NavMeshAgent>();
        investigator = GetComponent<InvestigatorState>();
        fear = GetComponent<Fear>();
    }

    public void DoYourThing(Transform position, string room)
    {
        fear.multiplier++;
        counter = UnityEngine.Random.Range(3, 10);
        inventory = Coordination.GetInventory(investigator);
        TravelToWaypoint(position);
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
        investigatorAgent.destination = GetNewPosition();
        yield return new WaitForSeconds(holdtime);
        DestinationReached();
    }

    private Vector3 GetNewPosition()
    {
        float xPosition = UnityEngine.Random.Range(-1.5f, 1.5f);
        float zPosition = UnityEngine.Random.Range(-1.5f, 1.5f);
        Vector3 newPosition = currentSpot;
        newPosition.x += xPosition;
        newPosition.z += zPosition;
        return newPosition;
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
