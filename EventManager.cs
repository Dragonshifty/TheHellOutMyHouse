using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager 
{
    public static event Action<GameObject> HaveArrived;
    public static event Action<GameObject> HaveInvestigated;
    public static event Action <GameObject> HaveFinishedTask;


    public static void ArrivedAtDestination(GameObject gameObject)
    {
        HaveArrived?.Invoke(gameObject);
    }

    public static void FinishedInvestigating(GameObject gameObject)
    {
        HaveInvestigated?.Invoke(gameObject);
    }

    public static void FinishedTask(GameObject gameObject)
    {
        HaveFinishedTask?.Invoke(gameObject);
    }
}
