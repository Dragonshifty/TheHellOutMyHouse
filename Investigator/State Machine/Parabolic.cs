using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Parabolic : MonoBehaviour, IActivate
{
    NavMeshAgent investigatorAgent;
    private TravelTo travelTo;
    private HouseInfo houseInfo;
    private Coroutine currentCoroutine;
    private InvestigatorState investigator;


    private void Start() 
    {
           
    }
    public void DoYourThing(Transform position, string room)
    {
        throw new System.NotImplementedException();
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
