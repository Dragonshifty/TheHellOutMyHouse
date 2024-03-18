using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceRun : INode
{
    private List<INode> childNodes; 

    public SequenceRun(List<INode> nodes)
    {
        childNodes = nodes;
    }
    public NodeState Evaluate()
    {
        foreach(var node in childNodes)
        {
            var result = node.Evaluate();
            if (result != NodeState.SUCCESS)
            {
                return result;
            }
        }
        return NodeState.SUCCESS;
    }
}
