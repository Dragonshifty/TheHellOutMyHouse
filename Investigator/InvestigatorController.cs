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
    ToDoList samToDoList;
    ToDoList simonToDoList;
    string samCurrentRoom = "Outside";
    string simonCurrentRoom = "Outside";
    private bool samIdle = true;
    private bool simonIdle = true;
    
    private bool samInRoom;
    private bool simonInRoom;
    private bool nRoom;
    
    private void Awake() 
    {
        samObject = GameObject.FindGameObjectWithTag("Sam");
        simonObject = GameObject.FindGameObjectWithTag("Simon");
        samState = samObject.GetComponent<InvestigatorState>();
        simonState = simonObject.GetComponent<InvestigatorState>();

        samToDoList = new ToDoList("Sam", samState);
        simonToDoList = new ToDoList("Simon", simonState);
    }
    void Start()
    {
        EventManager.HaveFinishedTask += gameObject => SetIdle(gameObject);
        EventManager.HaveEnteredRoom += gameObject => GetRoomInfo(gameObject);
        EventManager.HaveChangedLights += gameObject => UpdateLights(gameObject);
        StartInvestigation();

        PersonalityList personalityList = new PersonalityList();
        Personality personality = personalityList.GetAmateur();
        Debug.Log(personality.GetInvestigatorType() + personality.GetGearLevel());
    }

    

    // void Update()
    // {
    //     if (samIdle)
    //     {
    //         IssueOrder(samToDoList);
    //         samIdle = false;
    //     }

    //     if (simonIdle)
    //     {
    //         IssueOrder(simonToDoList);
    //         simonIdle = false;
    //     }
    // }

    private void StartInvestigation()
    {
        // samState.ChangeRoom("LivingRoom");
        // simonState.ChangeRoom("Bedroom1");
        IssueOrder(samToDoList);
        // IssueOrder(simonToDoList);
    }

    private void IssueOrder(ToDoList investigatorToDoList)
    {
        
        ActionList nextAction = investigatorToDoList.GetNextAction();
        
        if (nextAction != null)
        {
            string room;
            if (nextAction.Investigator.GetInvestigatorName().Equals("Sam"))
            {
                samCurrentRoom = nextAction.Room;
                room = samCurrentRoom;
            } 
            if (nextAction.Investigator.GetInvestigatorName().Equals("Simon"))
            {
                simonCurrentRoom = nextAction.Room;
                room = simonCurrentRoom;
            } 

            if (nextAction.Action.Equals("Travel"))
            {
                nextAction.Investigator.ChangeRoom(nextAction.Room);
            }
            Debug.Log("Got to here");

            if (nextAction.Action.Equals("Light"))
            {
                Debug.Log("Light");
                nextAction.Investigator.TurnOnLight(nextAction.Room);                
            }
        }

    }

    private void SetIdle(GameObject investigator)
    {

        if (investigator == samObject)
        {
            IssueOrder(samToDoList);
            samIdle = true;
        }
        if (investigator == simonObject) 
        {
            IssueOrder(simonToDoList);
            simonIdle = true;
        }
        // Debug.Log("Finshed Task");
    }

    private void GetRoomInfo(GameObject investigator)
    {
        if (investigator == samObject) 
        {
            samCurrentRoom = samObject.GetComponent<RoomKnowledge>().GetRoomName();
            Debug.Log("Sam in" + samCurrentRoom);
        }

        if (investigator == simonObject)
        {
            simonCurrentRoom = simonObject.GetComponent<RoomKnowledge>().GetRoomName();
            Debug.Log("Simon in" + simonCurrentRoom);
        }
    }

    private void UpdateLights(GameObject investigator)
    {
        bool onOrOff = investigator.GetComponent<RoomKnowledge>().GetLightStatus();

        if (!onOrOff)
        {
            investigator.GetComponent<RoomKnowledge>().FlickLightswitch(onOrOff);
        } else
        {
            // investigator.GetComponent<RoomKnowledge>().FlickLightswitch(onOrOff);
        }

        if (investigator == samObject)
        {
            samCurrentRoom = samObject.GetComponent<RoomKnowledge>().GetRoomName();
            Debug.Log("Sam in" + samCurrentRoom);
        }
    }
}
