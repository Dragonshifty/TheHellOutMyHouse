using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceController : MonoBehaviour
{
    private string evidenceInPlay = "";

    public string EvidenceInPlay
    {
        get { return evidenceInPlay; }
        set { evidenceInPlay = value; }
    }

    EvidenceState evidenceState;

    private void Start() 
    {
        evidenceState = GetComponent<EvidenceState>();    
    }

    
}
