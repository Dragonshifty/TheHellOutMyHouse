using System;
using System.Collections;
using System.Collections.Generic;
using PersonalitySpace;
using UnityEngine;

public class ToDoList 
{
    private string investigator;
    private string currentRoom;
    private bool lightOn;
    private float roomTemerature;
    private string targetDestination;

    private Personality personality;
    private InvestigatorState investigatorState;
    private bool inHouse = false;
    private bool tester = false;
    

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

    public string TargetDestination
    { get; set; }

    public void GetPersonality()
    {
        PersonalityList personalityList = new PersonalityList();

        string[] personalityListEasy = new string[] {"Student", "Amateur", "Professional"};

        string choice = personalityListEasy[UnityEngine.Random.Range(0, 3)];

        switch (choice)
        {
            case "Student":
                personality = personalityList.GetStudent();
                break;
            case "Amateur":
                personality = personalityList.GetAmateur();
                break;
            case "Professional":
                personality = personalityList.GetProfessional();
                break;
        }

        Debug.Log($"{investigator} is {personality.GetInvestigatorType()}");
    }

    public void UpdateRoomStatus(RoomKnowledge roomKnowledge)
    {
        currentRoom = roomKnowledge.GetRoomName();
        lightOn = roomKnowledge.GetLightStatus();
        roomTemerature = roomKnowledge.GetRoomTemperature();
    }

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
        if (!tester)
        {
            tester = true;
            return new ActionList(investigatorState, "Kitchen", "Travel");
        }
        return new ActionList(investigatorState, "Kitchen", "Search");
        // return null;
    }

    private bool CheckInHouse()
    {
        return inHouse;
    }

}
