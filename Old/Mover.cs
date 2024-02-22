using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    // [SerializeField] Transform target;
    private int counter;
    Vector3 currentDestination;
    // bool isMoving = true;
    private void Start() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();    
        StartCoroutine(MoveToDestination());
    }

    private void Update() 
    {
        CheckDistance();    
    }
    private float[] GetRandomCoords()
    {
        float xValue = Random.Range(-14f, -1.49f);
        float zValue = Random.Range(1.5f, 15f);

        float[] coords = new float[2];
        coords[0] = xValue;
        coords[1] = zValue;
        Debug.Log(coords[0] + " " + coords[1]);

        return coords;
    }

    private IEnumerator MoveToDestination()
    {
            yield return new WaitForSeconds(2);

            float[] coords = GetRandomCoords();   

            currentDestination = new Vector3(coords[0], 0, coords[1]);

            navMeshAgent.destination = currentDestination;  
    }

    private float CheckDistance()
    {
        return Vector3.Distance(transform.position, currentDestination);
    }

    // private void 

}
