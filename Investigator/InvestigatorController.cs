using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigatorController : MonoBehaviour
{
    private InvestigatorState samState;
    private InvestigatorState simonState;
    GameObject samObject;
    GameObject simonObject;
    private bool samIdle = true;
    private bool simonIdle = true;
    
    private void Awake() 
    {
        samObject = GameObject.FindGameObjectWithTag("Sam");
        simonObject = GameObject.FindGameObjectWithTag("simon");
        samState = samObject.GetComponent<InvestigatorState>();
        simonState = simonObject.GetComponent<InvestigatorState>();
    }
    void Start()
    {
        EventManager.HaveFinishedTask += gameObject => SetIdle(gameObject);
    }

    void Update()
    {
        
    }

    private void SetIdle(GameObject investigator)
    {
        if (investigator == samObject) samIdle = true;

        if (investigator == simonObject) simonIdle = true;
    }
}
