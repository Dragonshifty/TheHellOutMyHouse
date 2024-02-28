using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;

    private void Update() 
    {
        if (Input.GetMouseButton(0))
        {
           MoveTo();  
        }
        if (transform.position.y > 1)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = 1;
            transform.position = newPosition;
        }

        // Vector3 newRotation = transform.eulerAngles;
        // newRotation.z = Mathf.Clamp(newRotation.z, 0, 0);
        // transform.eulerAngles = newRotation;
    }
    private void MoveTo()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
        if (hasHit)
        {
            Vector3 target = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);
            transform.position = target;
        }
    }

    private Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
