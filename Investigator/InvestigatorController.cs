using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using PersonalitySpace;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;

public class InvestigatorController : MonoBehaviour
{
    private Camera mainCamera;
    private InvestigatorState samState;
    private InvestigatorState simonState;
    private BubbleText bubbleText = new BubbleText();
    
    [SerializeField] Transform frontDoor;
    
    GameObject samObject;
    GameObject simonObject;
    TextMeshPro samText;
    TextMeshPro simonText;
    Dictionary <InvestigatorState, TextMeshPro> textBubbles = new Dictionary<InvestigatorState, TextMeshPro>();
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

        samText = samObject.GetComponentInChildren<TextMeshPro>();
        simonText = simonObject.GetComponentInChildren<TextMeshPro>();
        textBubbles[samState] = samText;
        textBubbles[simonState] = simonText;
    }
    void Start()
    {
        mainCamera = Camera.main;
        EventManager.HaveFinishedTask += gameObject => SetIdle(gameObject);
        EventManager.HaveEnteredRoom += gameObject => GetRoomInfo(gameObject);
        EventManager.HaveChangedLights += gameObject => UpdateLights(gameObject);
        EventManager.HaveFoundHidingSpot += gameObject => UpdateHidingSpot(gameObject);
        StartInvestigation();
    }

    

    private void StartInvestigation()
    {
        investigatorToDoLists[samObject].GetPersonality();
        investigatorToDoLists[simonObject].GetPersonality();
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

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Cancel");
            EventManager.CancelActions();
            // samState.ChangeRoom("LivingRoom");
            // simonState.ChangeRoom("LivingRoom");
        }    
    }

    private void SendMessageToBubbles(ActionList nextAction)
    {
        string name = nextAction.InvestigatorState.GetInvestigatorName();
        string room = nextAction.Room;
        string task = nextAction.Action;
        TextMeshPro bubble = textBubbles[nextAction.InvestigatorState];
        bubbleText.ShowMessage(new MessageCarrier(name, room, task, bubble));
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
}
