using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToNode : INode
{   
    private Transform location;
    public TravelToNode(Transform location)
    {
        this.location = location;
    }
    public NodeState Evaluate()
    {
        return NodeState.SUCCESS;
    }
}
