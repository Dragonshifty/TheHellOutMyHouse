using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    [SerializeField] Transform target;
 
    void ConstructTree()
    {
        var travelToNode = new TravelToNode(target);

        var sequenceRun = new SequenceRun(new List<INode> { travelToNode});

        var result = sequenceRun.Evaluate();
    }
}
