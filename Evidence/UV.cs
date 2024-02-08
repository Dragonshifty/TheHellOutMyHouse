using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UV : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag.Equals("Simon") || other.gameObject.tag.Equals("Sam"))
        {
            if (other.gameObject.tag.Equals("Simon")) EvidenceController.simonInRange = true;
            if (other.gameObject.tag.Equals("Sam")) EvidenceController.samInRange = true;
        }    
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag.Equals("Simon") || other.gameObject.tag.Equals("Sam"))
        {
            if (other.gameObject.tag.Equals("Simon")) EvidenceController.simonInRange = false;
            if (other.gameObject.tag.Equals("Sam")) EvidenceController.samInRange = false;
        }    
    }
}
