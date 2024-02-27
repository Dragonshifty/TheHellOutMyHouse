using System.Collections;
using System.Collections.Generic;
using PersonalitySpace;
using Unity.VisualScripting;
using UnityEngine;

public class Coordination 
{
    private static Dictionary<InvestigatorState, ActionList> actionLists = new Dictionary<InvestigatorState, ActionList>();
    private static Dictionary<string, Personality> investigatorRoles = new Dictionary<string, Personality>();
    private static Dictionary<string, bool> roomsSearched = new Dictionary<string, bool>
    {
        { "Outside", true },
        { "LivingRoom", false },
        { "Hallway", false },
        { "Kitchen", false },
        { "Bedroom1", false },
        { "Bedroom2", false }
    };

    public static bool HasRoomBeenSearch(string room)
    {
        if (roomsSearched.ContainsKey(room))
        {
            return roomsSearched[room];
        } else
        {
            Debug.LogError ("Can't check if room searched, key error");
            return false;
        }
    }

    public static void SetRoomSearched(string room, bool searched)
    {
        if (roomsSearched.ContainsKey(room))
        {
            roomsSearched[room] = searched;
        } else
        {
            Debug.LogError("Can't set room searched, key error");
        }
    }

    public static void SetRole(string name, Personality personality)
    {
        investigatorRoles[name] = personality;
    }

    public static void SetActionList(ActionList actionList)
    {
        actionLists[actionList.InvestigatorState] = actionList;
    }

    public static bool CheckSameAction(InvestigatorState investigatorState)
    {
        if (actionLists != null)
        {
            foreach (KeyValuePair<InvestigatorState, ActionList> entry in actionLists)
            {
                if (!entry.Key.GetInvestigatorName().Equals(investigatorState.GetInvestigatorName()))
                {
                    if (actionLists[investigatorState].Room.Equals(entry.Value.Room) && actionLists[investigatorState].Action.Equals(entry.Value.Action))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static Personality GetPersonality(string investigatorName)
    {
        return investigatorRoles[investigatorName];
    }
}
