using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroupInventory : MonoBehaviour
{
    private Dictionary<string, int> gearLocker = new Dictionary<string, int>
    {
        { "flashLight", 2},
        { "UV", 2 },
        { "EMF", 2 },
        { "thermometer", 2}
    };

    public bool GetItem(string item)
    {
        switch (item)
        {
            default:
                Debug.Log("Get Item defaulted");
                return false;
            case "flashLight":
                return --gearLocker["flashLight"] >= 0;
            case "UV":
                return --gearLocker["UV"] >= 0;
            case "EMF":
                return --gearLocker["EMF"] >= 0;
            case "thermometer":
                return --gearLocker["thermometer"] >= 0;
        }
    }
    
    public void PutItem(string item)
    {
        switch (item)
        {
            default:
                Debug.Log("put Item defaulted");
                break;
            case "flashLight":
                gearLocker["flashLight"]++;
                break;
            case "UV":
                gearLocker["UV"]++;
                break;
            case "EMF":
                gearLocker["EMF"]++;
                break;
            case "thermometer":
                gearLocker["thermometer"]++;
                break;
        }
    }

    public string GetRandomGear()
    {
        List<string> gearList = new List<string>();

        foreach (KeyValuePair<string, int> entry in gearLocker)
        {
            if (entry.Key != "flashLight" && entry.Value > 0)
            {
                gearList.Add(entry.Key);
            }
        }
        int gearListCount = gearList.Count;

        if (gearListCount == 0) return "Nothing";
        if (gearListCount == 1)
        {
            if (GetItem(gearList[0])) return gearList[0];
            else return "nothing";
        } 

        int index = UnityEngine.Random.Range(0, gearListCount);

        if (GetItem(gearList[index]))
        {
            return gearList[index];
        };

        return "Nothing";
    }
}
