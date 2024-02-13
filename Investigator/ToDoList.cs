using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ToDoList 
{
    private string investigator;
    private string currentRoom;
    private string targetDestination;
    private InvestigatorState investigatorState;
    private bool inHouse = false;
    private bool livingRoomLight = false;
    // private bool lightsOn;

    public ToDoList(string investigator, InvestigatorState investigatorState)
    {
        this.investigator = investigator;
        this.investigatorState = investigatorState;
    }

    public string Investigator
    { get; private set; }

    public string CurrentRoom
    { get; set; }

    public string TargetDestination
    { get; set; }

    public ActionList GetNextAction()
    {
        if (!inHouse)
        {
            if (investigator.Equals("Sam")) return new ActionList(investigatorState, "LivingRoom", "Travel");
            if (investigator.Equals("Simon")) return new ActionList(investigatorState, "Kitchen", "Travel");
        }

        if (!livingRoomLight)
        {
            Debug.Log("To Do Light");
            livingRoomLight = true;
            return new ActionList(investigatorState, "LivingRoom", "Light");
        }
        return null;
    }

    private bool CheckInHouse()
    {
        return inHouse;
    }

}
