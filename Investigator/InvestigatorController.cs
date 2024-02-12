using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class InvestigatorController : MonoBehaviour
{
    private InvestigatorState samState;
    private InvestigatorState simonState;
    private RoomKnowledge roomKnowledge;
    [SerializeField] Transform frontDoor;
    
    GameObject samObject;
    GameObject simonObject;
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
    }
    void Start()
    {
        EventManager.HaveFinishedTask += gameObject => SetIdle(gameObject);
        EventManager.HaveEnteredRoom += gameObject => GetRoomInfo(gameObject);
        EventManager.HaveChangedLights += gameObject => UpdateLights(gameObject);
        StartInvestigation();
    }

    

    void Update()
    {
        
    }

    private void StartInvestigation()
    {
        samState.ChangeRoom("LivingRoom");
        simonState.ChangeRoom("Bedroom1");
    }

    private void SetIdle(GameObject investigator)
    {

        if (investigator == samObject) samIdle = true;

        if (investigator == simonObject) simonIdle = true;
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

        if (onOrOff)
        {
            investigator.GetComponent<RoomKnowledge>().FlickLightswitch(!onOrOff);
        } else
        {
            investigator.GetComponent<RoomKnowledge>().FlickLightswitch(onOrOff);
        }
    }
}
