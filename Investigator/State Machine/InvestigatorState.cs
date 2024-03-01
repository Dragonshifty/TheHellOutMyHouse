using UnityEngine;
using UnityEngine.AI;

public class InvestigatorState : MonoBehaviour 
{

    IActivate currentState;
    Transform transformCache;
    [SerializeField] string investigatorName;
    
    private void Start() 
    {   
        transformCache = gameObject.transform;
        currentState = GetComponent<Idle>();
        EventManager.CancelAction += StopAction;
    }

   

    public string GetInvestigatorName()
    {
        return investigatorName;
    }

    private void StartAction(Transform position, string roomName)
    {
        currentState.DoYourThing(position, roomName);
    }

    public void StopAction()
    {
        currentState.CancelAll();
        currentState = GetComponent<Idle>();
    }

    public void ChangeRoom(string room)
    {
        currentState = GetComponent<GoToRoom>();
        StartAction(transformCache, room);
    }

    public void TurnOnLight(string room)
    {
        currentState = GetComponent<FlickLightSwitch>();
        StartAction(transformCache, room);
    }

    public void SearchRoom(string room)
    {
        currentState = GetComponent<SearchRoom>();
        StartAction(transformCache, room);
    }

    public void FindHiding(string room)
    {
        currentState = GetComponent<FindHidingSpot>();
        StartAction(transformCache, room);
    }

    public void GrabNewEvidence(string room)
    {
        currentState = GetComponent<GrabEvidence>();
        StartAction(transformCache, room);
    }

    public void InvestigateMinorEvent(Transform eventPosition, string room)
    {
        
         currentState = GetComponent<InvestigateEvent>();
        StartAction(eventPosition, room);
    }

    public IActivate GetCurrentState()
    {
        return currentState;
    }


}