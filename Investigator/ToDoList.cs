using System;
using System.Collections;
using System.Collections.Generic;
using PersonalitySpace;
using System.Linq;
using UnityEngine;

public class ToDoList 
{
    private string investigator;
    private string investigatorType;
    private string currentRoom;
    private bool lightOn;
    private float roomTemerature;
    private Personality personality;
    private PersonalityController personalityController = new PersonalityController();
    private InvestigatorState investigatorState;
    private GroupInventory groupInventory;
    private string inventorySlotOne = "";
    private string inventorySlotTwo = "";
    private bool haveStarted;
    private bool clash;
    private bool needNewGear;
    private bool needNewRoom = true;
    private bool haveFoundHidingSpot;
    private bool minorEventHaveHidden;
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
        personality = personalityController.GetPersonality();

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
        if (minorEventHaveHidden)
        {
            minorEventHaveHidden = false;
            return new ActionList(investigatorState, currentRoom, "ReturnToInvestigate");
        }

        if (inventorySlotOne.Equals("") || needNewGear)
        {
            needNewGear = false;
            return new ActionList(investigatorState, "Outside", "GrabEvidence");
        }

        if (!haveStarted)
        {
            haveStarted = true;

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

        if (needNewRoom) 
        {
            string chosenRoom = explore.ChooseNewRoom(roomsVisited);
            needNewRoom = false;
            return new ActionList(investigatorState, chosenRoom, "Travel");            
        }

        if (personalityController.HidingSpotLevelCheck(personality) && hidingSpotsFound[currentRoom] == false)
        {
            return new ActionList(investigatorState, currentRoom, "FindHiding");
        }

        
        if (!Coordination.DiscoveredEvidence.Contains("thermometer"))
        {
            if ((inventorySlotOne.Equals("thermometer") || inventorySlotTwo.Equals("thermometer")) && !needNewGear)
            {
                needNewGear = true;
                needNewRoom = true;
                return new ActionList(investigatorState, currentRoom, "CheckTemperature");
            }
        }

        if (inventorySlotOne.Equals("salt") && !needNewGear)
        {
            needNewGear = true;
            needNewRoom = true;
            return new ActionList(investigatorState, currentRoom, "LaySalt");
        }

        if (personalityController.InvestigatorLevelCheck(personality) && !Coordination.HasRoomBeenSearch(currentRoom)) // true is search
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
        if (personalityController.FearLevelCheck(personality))
        {
            string hidingSpotDestination = "Outside";
            minorEventHaveHidden = true;
            foreach (KeyValuePair<string, bool> entry in hidingSpotsFound)
            {
                if (!entry.Key.Equals(currentRoom) && entry.Value == true)
                {
                    hidingSpotDestination = entry.Key;
                    break;
                }
            }
            return new ActionList(investigatorState, hidingSpotDestination, "Hide");
        }
        return new ActionList(investigatorState, currentRoom, "MinorEvent");
    }

    public void GetGear()
    {
        List<string> discoveredEvidence = Coordination.DiscoveredEvidence;

        if (inventorySlotOne.Equals(""))
        {
            inventorySlotOne = groupInventory.GetRandomGear("Nothing");
            Debug.Log(investigatorState.GetInvestigatorName() + " took " + inventorySlotOne);
            return;
        }

        if (discoveredEvidence.Any())
        {
            foreach (string entry in discoveredEvidence)
            {
                if (entry.Equals(inventorySlotOne))
                {
                    groupInventory.PutItem(entry);
                    inventorySlotOne = groupInventory.GetRandomGear(entry);
                    Debug.Log(investigatorState.GetInvestigatorName() + " took " + inventorySlotOne);
                }
            }
            return;
        }

        groupInventory.PutItem(inventorySlotOne);
        inventorySlotOne = groupInventory.GetRandomGear(inventorySlotOne);
        Debug.Log(investigatorState.GetInvestigatorName() + " took " + inventorySlotOne);

        // if (!inventorySlotOne.Equals("") && inventorySlotTwo.Equals(""))
        // {
        //     inventorySlotTwo = groupInventory.GetRandomGear(inventorySlotOne);
        //     Debug.Log(investigatorState.GetInvestigatorName() + " took " + inventorySlotTwo);
        //     return;
        // }

        // if (!inventorySlotTwo.Equals(""))
        // {
        //     groupInventory.PutItem(inventorySlotOne);
        //     inventorySlotOne = inventorySlotTwo;
        //     inventorySlotTwo = groupInventory.GetRandomGear(inventorySlotOne);            
        //     Debug.Log(investigatorState.GetInvestigatorName() + " took " + inventorySlotTwo);
        // }

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

    public ActionList RunAway()
    {
        return new ActionList(investigatorState, currentRoom, "Hide");
    }
}
