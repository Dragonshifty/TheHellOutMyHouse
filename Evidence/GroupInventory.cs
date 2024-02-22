using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupInventory : MonoBehaviour
{
    [SerializeField] private int flashLight = 2;
    [SerializeField] private int UV = 2;
    [SerializeField] private int EMF = 2;
    [SerializeField] private int thermometer = 2;

    public bool GetItem(string item)
    {
        switch (item)
        {
            default:
                Debug.Log("Get Item defaulted");
                return false;
            case "flasLight":
                return --flashLight >= 0;
            case "UV":
                return --UV >= 0;
            case "EMF":
                return --EMF >= 0;
            case "thermometer":
                return --thermometer >= 0;
        }
    }
    
    public void PutItem(string item)
    {
        switch (item)
        {
            default:
                Debug.Log("put Item defaulted");
                break;
            case "flasLight":
                flashLight++;
                break;
            case "UV":
                UV++;
                break;
            case "EMF":
                EMF++;
                break;
            case "thermometer":
                thermometer++;
                break;
        }
    }
}
