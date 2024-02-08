using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Outliner : MonoBehaviour
{
    

    private void OnDrawGizmos() 
    {
        foreach (Transform child in transform)
        {
            BoxCollider boxCollider = child.GetComponent<BoxCollider>();

            if (boxCollider != null)
            {
                Vector3 boxSize = boxCollider.size;

                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(child.position, boxSize);
            }
        }    
    }
}
