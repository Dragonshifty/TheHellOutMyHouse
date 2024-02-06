using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UV : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag.Equals("Investigator"))
        {
            Debug.Log("In range of evidence");
            EvidenceController.inRange = true;
        }    
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag.Equals("Investigator"))
        {
            Debug.Log("Leaving Evidence");
            EvidenceController.inRange = false;
        }    
    }
}
