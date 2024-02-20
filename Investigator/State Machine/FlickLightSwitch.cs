using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FlickLightSwitch : MonoBehaviour, IActivate
{
    NavMeshAgent investigator;
    private LightswitchInfo lightswitchInfo;
    private Dictionary<string, Transform> lightswitches;
    public void DoYourThing(string name, string room)
    {
        MoveToDestination(room);
    }

    void Start()
    {
        investigator = GetComponent<NavMeshAgent>();
        lightswitchInfo = FindObjectOfType<LightswitchInfo>();
        lightswitches = lightswitchInfo.GetLightswitchWaypoints();
    }

    private void MoveToDestination(string waypointName)
    {
        Vector3 destination = lightswitches[waypointName].position;
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
        EventManager.ChangedLights(gameObject);
        EventManager.FinishedTask(gameObject);
        
    }
}
