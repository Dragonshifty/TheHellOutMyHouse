using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ToDoList 
{
    private string investigator;
    private string currentRoom;
    private bool lightOn;
    private float roomTemerature;
    private string targetDestination;
    private InvestigatorState investigatorState;
    private bool inHouse = false;
    // private bool livingRoomLight = false;
    // private int test = 0;
    

    public ToDoList(string investigator, InvestigatorState investigatorState)
    {
        this.investigator = investigator;
        this.investigatorState = investigatorState;
    }

    public string Investigator
    { get; private set; }

    public string CurrentRoom
    { 
        get { return currentRoom; } 
        set { currentRoom = value; }
    }

    public void UpdateRoomStatus(RoomKnowledge roomKnowledge)
    {
        currentRoom = roomKnowledge.GetRoomName();
        lightOn = roomKnowledge.GetLightStatus();
        roomTemerature = roomKnowledge.GetRoomTemperature();
    }

    public string TargetDestination
    { get; set; }

    public ActionList GetNextAction()
    {
        if (!inHouse)
        {
            inHouse = true;
            if (investigator.Equals("Sam")) return new ActionList(investigatorState, "LivingRoom", "Travel");
            if (investigator.Equals("Simon")) return new ActionList(investigatorState, "Bedroom1", "Travel");
            
        }

        if (!lightOn)
        {
            return new ActionList(investigatorState, currentRoom, "Light");
        }
        return new ActionList(investigatorState, "Kitchen", "Travel");
        // return null;
    }

    private bool CheckInHouse()
    {
        return inHouse;
    }

}
