using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionList 
{
    private InvestigatorState investigatorState;
    private string room;
    private string action;

    public ActionList (InvestigatorState investigatorState, string room, string action)
    {
        this.investigatorState = investigatorState;
        this.room = room;
        this.action = action;
    }

    public InvestigatorState InvestigatorState { get { return investigatorState; } }
    public string Room { get { return room; } }
    public string Action { get { return action; } }
   
}
