// using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomInfo 
{
    [SerializeField] int[] roomA;

    private Dictionary<string, float> GetRoomBounderies(string room)
    {
        Dictionary<string, float> roomBounderies = new Dictionary<string, float>();
        switch (room)
        {
            case "A":
                
                break;
            case "B":
                break;
            case "C":
                break;
            case "D":
                roomBounderies.Add("Min X", -14f);
                roomBounderies.Add("Max X", (-1.5f) + .1f);
                roomBounderies.Add("Min Z", 1.5f);
                roomBounderies.Add("Max Z", (14f) + .5f);
                break;
        } 

        // Debug.Log(roomBounderies["Min X"]);

        return roomBounderies;
    }

    public float[] GetRandomCoords(string roomKey)
    {
        Dictionary<string, float> coordsDict = GetRoomBounderies(roomKey);

        float xValue = Random.Range(coordsDict["Min X"], coordsDict["Max X"]);
        float zValue = Random.Range(coordsDict["Min Z"], coordsDict["Max Z"]);

        float[] coords = {xValue, zValue};
        
        return coords;
    }

}
