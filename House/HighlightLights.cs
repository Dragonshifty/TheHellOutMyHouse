using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightLights : MonoBehaviour
{
    [SerializeField] private Color gizmoColour = Color.yellow;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColour;

        foreach (Transform child in transform)
        {
                Gizmos.DrawSphere(child.position, .5f);
        }
    }
}
