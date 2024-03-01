using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceState : MonoBehaviour
{
    private IEvidence currentState;
    

    void Start()
    {
        currentState = GetComponent<IdleEvidence>();
    }


    public void PlantEMF()
    {
        currentState = GetComponent<EMF>();
        currentState.ActionEvidence();
    }

    public void PlantUV()
    {
        currentState = GetComponent<UV>();
        currentState.ActionEvidence();
    }

    public void TemperatureChange()
    {
        currentState = GetComponent<Thermometer>();
        currentState.ActionEvidence();
    }

    public void SetIdleEvidence()
    {
        currentState = GetComponent<IdleEvidence>();
    }
}
