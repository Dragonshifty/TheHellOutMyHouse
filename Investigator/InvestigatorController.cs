using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using PersonalitySpace;
using UnityEngine;

public class InvestigatorController : MonoBehaviour
{
    private InvestigatorState samState;
    private InvestigatorState simonState;
    
    [SerializeField] Transform frontDoor;
    
    GameObject samObject;
    GameObject simonObject;
    // ToDoList samToDoList;
    // ToDoList simonToDoList;
    Dictionary<GameObject, ToDoList> investigatorToDoLists = new Dictionary<GameObject, ToDoList>();
    // string samCurrentRoom = "Outside";
    // string simonCurrentRoom = "Outside";
    // private bool samIdle = true;
    // private bool simonIdle = true;
    
    // private bool samInRoom;
    // private bool simonInRoom;
    // private bool nRoom;
    
    private void Awake() 
    {
        samObject = GameObject.FindGameObjectWithTag("Sam");
        simonObject = GameObject.FindGameObjectWithTag("Simon");
        samState = samObject.GetComponent<InvestigatorState>();
        simonState = simonObject.GetComponent<InvestigatorState>();

        investigatorToDoLists[samObject] = new ToDoList("Sam", samState);
        investigatorToDoLists[simonObject] = new ToDoList("Simon", simonState);
    }
    void Start()
    {
        EventManager.HaveFinishedTask += gameObject => SetIdle(gameObject);
        EventManager.HaveEnteredRoom += gameObject => GetRoomInfo(gameObject);
        EventManager.HaveChangedLights += gameObject => UpdateLights(gameObject);
        StartInvestigation();
    }

    

    private void StartInvestigation()
    {
        IssueOrder(investigatorToDoLists[samObject]);
        IssueOrder(investigatorToDoLists[simonObject]);
    }

    private void IssueOrder(ToDoList investigatorToDoList)
    {
        
        ActionList nextAction = investigatorToDoList.GetNextAction();
        
        if (nextAction != null)
        {
            if (nextAction.Action.Equals("Travel"))
            {
                nextAction.Investigator.ChangeRoom(nextAction.Room);
            }

            if (nextAction.Action.Equals("Light"))
            {
                Debug.Log("Turning on the light.");
                nextAction.Investigator.TurnOnLight(nextAction.Room);                
            }
        }
    }

    private void SetIdle(GameObject investigator)
    {
        IssueOrder(investigatorToDoLists[investigator]);
    }

    private void GetRoomInfo(GameObject investigator)
    {
        RoomKnowledge roomKnowledge = investigator.GetComponent<RoomKnowledge>();
        string room = roomKnowledge.GetRoomName();
        string investigatorName = investigator.GetComponent<InvestigatorState>().GetInvestigatorName();
        bool lightStatus = roomKnowledge.GetLightStatus();

        investigatorToDoLists[investigator].UpdateRoomStatus(roomKnowledge);
        Debug.Log(investigatorName + room + " Light is " + lightStatus);
    }

    private void UpdateLights(GameObject investigator)
    {
        RoomKnowledge roomKnowledge = investigator.GetComponent<RoomKnowledge>();
        bool onOrOff = roomKnowledge.GetLightStatus();

        if (!onOrOff)
        {
            roomKnowledge.FlickLightswitch(true);
        } else
        {
            roomKnowledge.FlickLightswitch(onOrOff);
        }

        investigatorToDoLists[investigator].UpdateRoomStatus(roomKnowledge);

        Debug.Log(roomKnowledge.GetRoomName() + " " + "light is " + roomKnowledge.GetLightStatus());
    }
}
