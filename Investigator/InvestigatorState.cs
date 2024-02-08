using UnityEngine;
using UnityEngine.AI;

public class InvestigatorState : MonoBehaviour 
{

    IActivate currentState;
    
    [SerializeField] string investigatorName;
    private string currentRoom = "Outside";
    private void Start() 
    {   
        currentState = GetComponent<Idle>();
        EventManager.HaveArrived += gameObject => ArrivedAtDestination(gameObject);
        EventManager.HaveInvestigated += gameObject => FinishedInvestigating(gameObject);
        EventManager.HaveFinishedTask += gameObject => FinishedTask(gameObject);
        // StartAction();
    }

    private void StartAction(string roomName)
    {
        currentState.DoYourThing(investigatorName, roomName);
    }
    private void ArrivedAtDestination(GameObject gameObject)
    {
        if (gameObject == this.gameObject)
        {
            currentState = GetComponent<Investigating>();
            // StartAction();
        }
        
    }

    private void FinishedInvestigating(GameObject gameObject)
    {
        if (gameObject == this.gameObject)
        {
            currentState = GetComponent<Travel>();
            // StartAction();
        } 
    }

    private void FinishedTask(GameObject gameObject)
    {
        if (gameObject == this.gameObject)
        {
            currentState = GetComponent<Idle>();
        }
    }

    private void ChangeRoom(string room)
    {
        currentState = GetComponent<GoToRoom>();
        StartAction(room);
    }

    public IActivate GetCurrentState()
    {
        return currentState;
    }
}