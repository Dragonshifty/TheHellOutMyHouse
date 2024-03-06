using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FlickLightSwitch : MonoBehaviour, IActivate
{
    NavMeshAgent investigatorAgent;
    private TravelTo travelTo;
    Coroutine currentCoroutine;
    private LightswitchInfo lightswitchInfo;
    private Dictionary<string, Transform> lightswitches;
    
    public void DoYourThing(Transform position, string room)
    {
        TravelToWaypoint(room);
    }

    void Start()
    {
        investigatorAgent = GetComponent<NavMeshAgent>();
        lightswitchInfo = FindObjectOfType<LightswitchInfo>();
        lightswitches = lightswitchInfo.GetLightswitchWaypoints();
    }

    private void TravelToWaypoint(string room)
    {
        #pragma warning disable 4014
        travelTo = new TravelTo(investigatorAgent, lightswitches[room].transform);
        travelTo.MoveToWaypoint(DestinationReached); 
    }

    
    private void DestinationReached()
    {
        EventManager.ChangedLights(gameObject);
        EventManager.FinishedTask(gameObject);
    }

    public void CancelAll()
    {
        if (currentCoroutine != null)
        {
            StopAllCoroutines();
            currentCoroutine = null;
        }
        travelTo.StopNavigation();
    }
}
