using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorEvent : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EventManager.StartedMinorEvent(gameObject.transform);
        }
    }
}
