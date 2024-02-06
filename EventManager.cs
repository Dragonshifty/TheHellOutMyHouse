using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager 
{
    public static event Action HaveArrived;
    public static event Action HaveInvestigated;
    // public static event Action 


    public static void ArrivedAtDestination()
    {
        HaveArrived?.Invoke();
    }

    public static void FinishedInvestigating()
    {
        HaveInvestigated?.Invoke();
    }
}
