using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;

public class TravelTo 
{
    private NavMeshAgent investigatorAgent;
    private Transform waypoint;
    private CancellationTokenSource cancellationTokenSource;

    public TravelTo(NavMeshAgent navigatorAgent, Transform waypoint)
    {
        this.investigatorAgent = navigatorAgent;
        this.waypoint = waypoint;
    }

    public async Task MoveToWaypoint(Action DestinationReached = null)
    {
        cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        Vector3 destination = waypoint.position;

        investigatorAgent.destination = destination;

        while(!cancellationToken.IsCancellationRequested && CheckDistance(destination) > 1.5f)
        {
            await Task.Delay(400, cancellationToken);
        }

        if (!cancellationToken.IsCancellationRequested)
        {
            DestinationReached?.Invoke();
        }
    }
    
    private float CheckDistance(Vector3 destination)
    {
        return Vector3.Distance(investigatorAgent.transform.position, destination);
    }

    public void StopNavigation()
    {
        cancellationTokenSource?.Cancel();
    }
}
