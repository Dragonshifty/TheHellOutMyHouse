using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using PersonalitySpace;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;

public class InvestigatorController : MonoBehaviour
{
    private InvestigatorState samState;
    private InvestigatorState simonState;
    private BubbleText bubbleText;
    
    [SerializeField] Transform frontDoor;
    
    GameObject samObject;
    GameObject simonObject;
    Dictionary<GameObject, ToDoList> investigatorToDoLists = new Dictionary<GameObject, ToDoList>();

    private void Awake() 
    {
        samObject = GameObject.FindGameObjectWithTag("Sam");
        simonObject = GameObject.FindGameObjectWithTag("Simon");
        samState = samObject.GetComponent<InvestigatorState>();
        simonState = simonObject.GetComponent<InvestigatorState>();

        GroupInventory groupInventory = FindObjectOfType<GroupInventory>();
        investigatorToDoLists[samObject] = new ToDoList("Sam", samState, groupInventory);
        investigatorToDoLists[simonObject] = new ToDoList("Simon", simonState, groupInventory);

        bubbleText = FindObjectOfType<BubbleText>();
    }
    void Start()
    {
        EventManager.HaveFinishedTask += gameObject => SetIdle(gameObject);
        EventManager.HaveEnteredRoom += gameObject => GetRoomInfo(gameObject);
        EventManager.HaveChangedLights += gameObject => UpdateLights(gameObject);
        EventManager.HaveFoundHidingSpot += gameObject => UpdateHidingSpot(gameObject);
        EventManager.HaveStartedMinorEvent += position => MinorEvent(position);
        EventManager.HaveCollectedGear += gameObject => CollectedGear(gameObject);
        StartInvestigation();
    }

        private void StartInvestigation()
    {
        investigatorToDoLists[samObject].InitialiseInvestigator();
        investigatorToDoLists[simonObject].InitialiseInvestigator();
        IssueOrder(investigatorToDoLists[samObject]);
        IssueOrder(investigatorToDoLists[simonObject]);
    }

    private void IssueOrder(ToDoList investigatorToDoList)
    {
        
        ActionList nextAction = investigatorToDoList.GetNextAction();
        
        if (nextAction != null)
        {
            SendMessageToBubbles(nextAction);

            switch (nextAction.Action)
            {
                default:
                    Debug.LogError("Action List Error. String typo?");
                    break;
                case "Travel":
                    nextAction.InvestigatorState.ChangeRoom(nextAction.Room);
                    break;
                case "Light":
                    nextAction.InvestigatorState.TurnOnLight(nextAction.Room);
                    break;
                case "Search":
                    nextAction.InvestigatorState.SearchRoom(nextAction.Room);
                    break;
                case "FindHiding":
                    nextAction.InvestigatorState.FindHiding(nextAction.Room);
                    break;
                case "Clash":
                    IssueOrder(investigatorToDoLists[nextAction.InvestigatorState.gameObject]);
                    break;
                case "GrabEvidence":
                    nextAction.InvestigatorState.GrabNewEvidence(nextAction.Room);
                    break;
            }
        }
    }

    private void SendMessageToBubbles(ActionList nextAction)
    {
        string name = nextAction.InvestigatorState.GetInvestigatorName();
        string room = nextAction.Room;
        string task = nextAction.Action;
        bubbleText.ShowMessage(new MessageCarrier(name, room, task));
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
        // Debug.Log(investigatorName + room + " Light is " + lightStatus);
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

        // Debug.Log(roomKnowledge.GetRoomName() + " " + "light is " + roomKnowledge.GetLightStatus());
    }

    private void UpdateHidingSpot(GameObject investigator)
    {
        investigatorToDoLists[investigator].UpdateHidingSpot();
    }

    private void MinorEvent(Transform eventPosition)
    {
        foreach (KeyValuePair<GameObject, ToDoList> entry in investigatorToDoLists)
        {
            if (Vector3.Distance(entry.Key.transform.position, eventPosition.position) < 10f)
            {              
                MinorEventAction(entry.Value, eventPosition);
            }
        }
    }

    private void MinorEventAction(ToDoList investigatorToDoList, Transform eventPosition)
    {
        ActionList nextAction = investigatorToDoList.GetMinorEventAction();

        SendMessageToBubbles(nextAction);
        nextAction.InvestigatorState.StopAction();
        nextAction.InvestigatorState.InvestigateMinorEvent(eventPosition, nextAction.Room);
    }

    private void CollectedGear(GameObject investigator)
    {
        investigatorToDoLists[investigator].GetGear();
    }
}
