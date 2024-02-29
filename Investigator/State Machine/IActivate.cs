using System;
using UnityEngine;

public interface IActivate
{
    void DoYourThing(Transform position, string room);
    void CancelAll();
}