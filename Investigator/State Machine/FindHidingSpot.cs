using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindHidingSpot : MonoBehaviour, IActivate
{
    NavMeshAgent navMeshAgent;
    public void DoYourThing(string name, string room)
    {
        MoveTo();
    }

    private void MoveTo()
    {
        RoomKnowledge roomKnowledge = GetComponent<RoomKnowledge>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = roomKnowledge.GetHidingSpot().position;

    }

}
