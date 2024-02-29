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
        // EventManager.HaveArrived += gameObject => ArrivedAtDestination(gameObject);
        // EventManager.HaveInvestigated += gameObject => FinishedInvestigating(gameObject);
        // EventManager.HaveFinishedTask += gameObject => FinishedTask(gameObject);
        EventManager.CancelAction += StopAction;
        // rigidbody = GetComponent<Rigidbody>();
        // StartAction();
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
    // public void ArrivedAtDestination(GameObject gameObject)
    // {
    //     if (gameObject == this.gameObject)
    //     {
    //         currentState = GetComponent<Investigating>();
    //         // StartAction();
    //     }
        
    // }

    // public void FinishedInvestigating(GameObject gameObject)
    // {
    //     if (gameObject == this.gameObject)
    //     {
    //         currentState = GetComponent<Travel>();
    //         // StartAction();
    //     } 
    // }

    // private void FinishedTask(GameObject gameObject)
    // {
    //     if (gameObject == this.gameObject)
    //     {
    //         currentState = GetComponent<Idle>();
    //     }
    // }

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