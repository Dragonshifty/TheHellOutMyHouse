using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckTemperature : MonoBehaviour, IActivate
{
    List<Transform> roomWaypoints;
    NavMeshAgent investigatorAgent;
    RoomKnowledge roomKnowledge;
    HouseInfo houseInfo;
    private Coroutine currentCoroutine;
    private int counter;
    private Dictionary<string, Transform> houseRoomWaypoints;
    private List<string> checkedRooms = new List<string>();
    private List<string> rooms = new List<string>();
    private string targetRoom;
    private bool tempsFound;

    private void Start()
    {
        investigatorAgent = GetComponent<NavMeshAgent>();
        roomKnowledge = GetComponent<RoomKnowledge>();
        houseInfo = FindObjectOfType<HouseInfo>();
        if (houseInfo != null)
        {
            houseRoomWaypoints = houseInfo.GetRoomWaypoints();
        }
        else
        {
            Debug.LogError("HouseInfo component is null!");
        }

        foreach (KeyValuePair<string, Transform> entry in houseRoomWaypoints)
        {
            if (!entry.Key.Equals("Outside"))
            {
                rooms.Add(entry.Key);
            }
        }
    }
    public void DoYourThing(Transform position, string room)
    {  
        PickNewRoom();
    }

    private void PickNewRoom()
    {
        counter = UnityEngine.Random.Range(3, 10);
        if (rooms.Count == 0)
        {
            rooms = checkedRooms;
            checkedRooms.Clear();
            EndTask();
            return;
        }

        int index = UnityEngine.Random.Range(0, rooms.Count);
        
        targetRoom = rooms[index];
        checkedRooms.Add(rooms[index]);
        rooms.Remove(targetRoom);
        MoveToNewRoom(targetRoom);
    }

    private void MoveToNewRoom(string roomName)
    {
        Vector3 destination = houseRoomWaypoints[roomName].position;
        investigatorAgent.destination = destination;

        currentCoroutine = StartCoroutine(CheckForDestinationReached(destination));
    }

    public void StartSearch()
    {
        roomWaypoints = roomKnowledge.GetRoomPoints();
        MoveToRoomWaypoint();
    }

    private void MoveToRoomWaypoint()
    {
        int index = UnityEngine.Random.Range(0, roomWaypoints.Count);

        if (roomWaypoints.Count == 0)
        {
            Debug.LogError("No waypoints available.");
        }
        Vector3 destination = roomWaypoints[index].position;
        investigatorAgent.destination = destination;

        currentCoroutine = StartCoroutine(CheckForDestinationReached(destination));
    }

    private IEnumerator CheckForDestinationReached(Vector3 destination)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            if (CheckDistance(destination) < 1.5f)
            {
                // EndTask();
                DestinationReached();
                yield break;
            }
        }
    }

    private float CheckDistance(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination);
    }

    private void DestinationReached()
    {
        counter--;
        if (counter > 0)
        {
            // MoveToDestination();
            currentCoroutine = StartCoroutine(GetTemperatureReading());
        }
        else
        {
            PickNewRoom();
        }
    }

    private void EndTask()
    {
        CancelAll();
        EventManager.FinishedTask(gameObject);
    }

    private IEnumerator GetTemperatureReading()
    {
        int holdtime = UnityEngine.Random.Range(1, 3);
        float roomTemp = roomKnowledge.GetRoomTemperature();
        string nameOfRoom = roomKnowledge.GetRoomName();
        Debug.Log(nameOfRoom + " is at " + roomTemp + "°");
        yield return new WaitForSeconds(holdtime);
        
        if (roomKnowledge.GetRoomTemperature() < 10)
        {
            Coordination.CurrentColdRoom = nameOfRoom;
            Debug.Log("Cold Room");
            if (roomTemp < 0)
            {
                Coordination.SetNewEvidence("thermometer");
                Debug.Log("Found EVIDENCE");
            }
            
            EndTask();
            yield break;
        } else
        {
            StartSearch();
            yield break;
        } 
    }



    public void CancelAll()
    {
        if (currentCoroutine != null)
        {
            StopAllCoroutines();
            currentCoroutine = null;
        }
    }
}


