using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
using UnityEngine;

public class LaySalt : MonoBehaviour, IActivate
{
    NavMeshAgent investigatorAgent;
    private TravelTo travelTo;
    [SerializeField] private GameObject salt;
    HouseInfo houseInfo;
    Coroutine currentCoroutine;
    List<Transform> saltedRoomWaypoints = new List<Transform>();
    List<Transform> unSaltedRoomWaypoints = new List<Transform>();

    
    private void Start()
    {
        Dictionary<string, Transform> roomWaypoints = new Dictionary<string, Transform>();
        investigatorAgent = GetComponent<NavMeshAgent>();
        houseInfo = FindObjectOfType<HouseInfo>();
        if (houseInfo != null)
        {
            roomWaypoints = houseInfo.GetRoomWaypoints();
        }
        else
        {
            Debug.LogError("HouseInfo component is null!");
        }

        foreach (KeyValuePair<string, Transform> entry in roomWaypoints)
        {
            if (!entry.Key.Equals("Outside"))
            {
                unSaltedRoomWaypoints.Add(entry.Value);
            }
        }
    }

    public void DoYourThing(Transform position, string room)
    {
        MoveToSaltyPosition();
    }

    private void MoveToSaltyPosition()
    {
        if (!unSaltedRoomWaypoints.Any())
        {
            DestinationReached();
            return;
        }

        int waypointCount = unSaltedRoomWaypoints.Count;

        if (waypointCount == 1)
        {
            TravelToWaypoint(unSaltedRoomWaypoints[0]);
            unSaltedRoomWaypoints.Clear();
        } else
        {
            int index = UnityEngine.Random.Range(0, waypointCount);

            TravelToWaypoint(unSaltedRoomWaypoints[index]);
            unSaltedRoomWaypoints.RemoveAt(index);
        } 
    }

    private void TravelToWaypoint(Transform target)
    {
        #pragma warning disable 4014
        travelTo = new TravelTo(investigatorAgent, target);
        travelTo.MoveToWaypoint(DestinationReached);
    }

    private void DestinationReached()
    {
        Instantiate(salt, gameObject.transform.position, Quaternion.identity);
        EventManager.FinishedTask(gameObject);
    }

    public void CancelAll()
    {
        StopAllCoroutines();
        travelTo.StopNavigation();
    }
}
