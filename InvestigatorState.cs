using UnityEngine;
using UnityEngine.AI;

public class InvestigatorState : MonoBehaviour 
{
    NavMeshAgent investigator;
    IActivate currentState;

    private void Start() 
    {
        investigator = GetComponent<NavMeshAgent>();
        currentState = GetComponent<Travel>();
        EventManager.HaveArrived += ArrivedAtDestination;
        EventManager.HaveInvestigated += FinishedInvestigating;
        StartAction();
    }

    private void StartAction()
    {
        currentState.DoYourThing(investigator);
    }
    private void ArrivedAtDestination()
    {
        currentState = GetComponent<Investigating>();
        StartAction();
    }

    private void FinishedInvestigating()
    {
        currentState = GetComponent<Travel>();
        StartAction();
    }
}