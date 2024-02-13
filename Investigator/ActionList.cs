using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionList 
{
    private InvestigatorState investigator;
    private string room;
    private string action;

    public ActionList (InvestigatorState investigator, string room, string action)
    {
        this.investigator = investigator;
        this.room = room;
        this.action = action;
    }

    public InvestigatorState Investigator { get {return investigator; } }
    public string Room { get { return room; } }
    public string Action { get { return action; } }
   
}
