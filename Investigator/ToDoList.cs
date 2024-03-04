using System;
using System.Collections;
using System.Collections.Generic;
using PersonalitySpace;
using Unity.VisualScripting;
using UnityEngine;

public class ToDoList 
{
    private string investigator;
    private string investigatorType;
    private string currentRoom;
    private bool lightOn;
    private float roomTemerature;

    private Personality personality;
    private InvestigatorState investigatorState;
    private GroupInventory groupInventory;
    private string inventorySlotOne = "";
    private string inventorySlotTwo = "";
    private bool inHouse;
    private bool clash;
    private Explore explore = new Explore();
    private Dictionary<string, bool> roomsVisited = HouseRoomReturn.GetStarterHouseRooms();
    private Dictionary<string, bool> hidingSpotsFound = HouseRoomReturn.GetStarterHouseRooms();
    
    public ToDoList(string investigator, InvestigatorState investigatorState, GroupInventory groupInventory)
    {
        this.investigator = investigator;
        this.investigatorState = investigatorState;
        this.groupInventory = groupInventory;
    }

    public string Investigator
    { get; private set; }

    public string CurrentRoom
    { 
        get { return currentRoom; } 
        set { currentRoom = value; }
    }

    public void InitialiseInvestigator()
    {
        GetPersonality();
        SetCoordinateInventory();
    }

    public void GetPersonality()
    {
        PersonalityList personalityList = new PersonalityList();

        string[] personalityListEasy = new string[] {"Student", "Amateur", "Experienced", "Professional"};

        string choice = personalityListEasy[UnityEngine.Random.Range(0, 3)];

        switch (choice)
        {
            case "Student":
                personality = personalityList.GetStudent();
                break;
            case "Amateur":
                personality = personalityList.GetAmateur();
                break;
            case "Experienced":
                personality = personalityList.GetExperienced();
                break;
            case "Professional":
                personality = personalityList.GetProfessional();
                break;
        }

        investigatorType = personality.GetInvestigatorType();
        Coordination.SetRole(investigatorState, personality);

        Debug.Log($"{investigator} is {investigatorType}");
    }

    public void UpdateRoomStatus(RoomKnowledge roomKnowledge)
    {
        currentRoom = roomKnowledge.GetRoomName();
        lightOn = roomKnowledge.GetLightStatus();
        roomTemerature = roomKnowledge.GetRoomTemperature();
    }

    public void UpdateHidingSpot()
    {
        Debug.Log("Found Hiding Spot, Logging");
        hidingSpotsFound[currentRoom] = true;
    }

    public ActionList GetNextAction()
    {
        
        if (!inHouse)
        {
            inHouse = true;

            return new ActionList(investigatorState, "LivingRoom", "Travel");
        }

        if (!lightOn && !clash)
        {
            ActionList newActionList = new ActionList(investigatorState, currentRoom, "Light");
            Coordination.SetActionList(newActionList);
            if (Coordination.CheckSameAction(investigatorState))
            {
                clash = true;
                return new ActionList(investigatorState, currentRoom, "Clash");
            }
            return newActionList;
        } 
            
        clash = false;

        // return new ActionList (investigatorState, currentRoom, "CheckTemperature");

        if (inventorySlotOne.Equals(""))
        {
            return new ActionList(investigatorState, "Outside", "GrabEvidence");
        }
        

        if (SearchOrExplore() && !Coordination.HasRoomBeenSearch(currentRoom)) // true is search
            {
                Coordination.SetRoomSearched(currentRoom, true);
                return new ActionList(investigatorState, currentRoom, "FindHiding");
            } else
            {
                string chosenRoom = explore.ChooseNewRoom(roomsVisited);
                return new ActionList(investigatorState, chosenRoom, "Travel");
            }
    }

    public ActionList GetMinorEventAction()
    {
        return new ActionList(investigatorState, currentRoom, "MinorEvent");
    }

    private bool SearchOrExplore()
    {
        int searchLevel = personality.GetInvestigativeSkillLevel();
        if (searchLevel >= UnityEngine.Random.Range(0, 11)) return true;
        
        return false;
    }

    public void GetGear()
    {
        if (inventorySlotOne.Equals(""))
        {
            inventorySlotOne = groupInventory.GetRandomGear("Nothing");
            Debug.Log(investigatorState.GetInvestigatorName() + " took " + inventorySlotOne);
            return;
        }

        if (!inventorySlotOne.Equals("") && inventorySlotTwo.Equals(""))
        {
            inventorySlotTwo = groupInventory.GetRandomGear(inventorySlotOne);
            Debug.Log(investigatorState.GetInvestigatorName() + " took " + inventorySlotTwo);
            return;
        }

        if (!inventorySlotTwo.Equals(""))
        {
            groupInventory.PutItem(inventorySlotOne);
            inventorySlotOne = inventorySlotTwo;
            inventorySlotTwo = groupInventory.GetRandomGear(inventorySlotOne);            
            Debug.Log(investigatorState.GetInvestigatorName() + " took " + inventorySlotTwo);
        }

        SetCoordinateInventory();
    }

    private void SetCoordinateInventory()
    {
        List<string> inventory = new List<string>()
        {
            inventorySlotOne,
            inventorySlotTwo
        };
        Coordination.SetInventory(investigatorState, inventory);
    }

}
