using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class Travel : MonoBehaviour, IActivate
{
    Vector3 currentDestination;
    NavMeshAgent investigator;
    string investigatorName;
    RoomInfo roomInfo;


    private void Awake() 
    {
        investigator = GetComponent<NavMeshAgent>();
        roomInfo = new RoomInfo();
    }

    public void DoYourThing(string name, string room)
    {
        investigatorName = name;
        #pragma warning disable 4014
        StartMoving();
    }

    private async Task StartMoving()
    {
        await Task.Delay(500);
        MoveToDestination();
        
    }

    private float[] GetTargetCoords()
    {
        float xValue = Random.Range(-14f, -1.49f);
        float zValue = Random.Range(1.5f, 14.99f);

        float[] coords = new float[2];
        coords[0] = xValue;
        coords[1] = zValue;
        // Debug.Log(coords[0] + " " + coords[1]);

        return coords;
    }

    private void MoveToDestination()
    {
        // float[] coords = GetTargetCoords();   
        float[] coords = roomInfo.GetRandomCoords("D");
        currentDestination = new Vector3(coords[0], 0, coords[1]);
        investigator.destination = currentDestination; 

        StartCoroutine(CheckForDestinationReached());
    }

    private IEnumerator CheckForDestinationReached()
    {
        // Debug.Log("Started co");
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            // Debug.Log(CheckDistance());
            if (CheckDistance() < 1.5f)
            {
                
                DestinationReached();
                yield break;
            }
        }
    }

    private void DestinationReached()
    {
        // EventManager.ArrivedAtDestination(gameObject);
        EventManager.FinishedTask(gameObject);
    }

    private float CheckDistance()
    {
        return Vector3.Distance(transform.position, currentDestination);
    }
}