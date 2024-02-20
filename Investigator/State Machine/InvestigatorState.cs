using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class InvestigatorState : MonoBehaviour 
{

    IActivate currentState;
    
    [SerializeField] string investigatorName;
    // private string currentRoom = "Outside";
    private void Start() 
    {   
        currentState = GetComponent<Idle>();
        EventManager.HaveArrived += gameObject => ArrivedAtDestination(gameObject);
        EventManager.HaveInvestigated += gameObject => FinishedInvestigating(gameObject);
        EventManager.HaveFinishedTask += gameObject => FinishedTask(gameObject);
        // StartAction();
    }

    public string GetInvestigatorName()
    {
        return investigatorName;
    }

    private void StartAction(string roomName)
    {
        currentState.DoYourThing(investigatorName, roomName);
    }
    public void ArrivedAtDestination(GameObject gameObject)
    {
        if (gameObject == this.gameObject)
        {
            currentState = GetComponent<Investigating>();
            // StartAction();
        }
        
    }

    public void FinishedInvestigating(GameObject gameObject)
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

    public void ChangeRoom(string room)
    {
        currentState = GetComponent<GoToRoom>();
        StartAction(room);
    }

    public void TurnOnLight(string room)
    {
        currentState = GetComponent<FlickLightSwitch>();
        StartAction(room);
    }

    public IActivate GetCurrentState()
    {
        return currentState;
    }
}