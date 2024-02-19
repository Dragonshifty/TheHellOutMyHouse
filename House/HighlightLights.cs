using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightLights : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        foreach (Transform child in transform)
        {
                Vector3 highlightArea = new Vector3(1, 1, 1);

                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(child.position, .5f);
            
        }
    }
}
