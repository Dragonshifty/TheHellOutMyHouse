using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UV : MonoBehaviour, IEvidence
{
    public void ActionEvidence()
    {
        throw new System.NotImplementedException();
    }

    public void Cancel()
    {
        StopAllCoroutines();
    }
}
