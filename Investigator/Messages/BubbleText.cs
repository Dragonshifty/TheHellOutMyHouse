using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleText 
{
    private string goingTo = " going to ";
    private string lightSwitch = " switching ";
    private string searching = " searching ";

    public void ShowMessage(MessageCarrier message)
    {
        TextMeshPro bubbleText = message.Bubble;
        string name = message.Name;
        string room = message.Room;
        string task = message.Task;

        switch (task)
        {
            default:
                bubbleText.text = "No Message";
                break;
            case "Travel":
                bubbleText.text = $"{name} {goingTo} {room}";
                break;
            case "Light":
                bubbleText.text = $"{name} {lightSwitch} {room} on";
                break;
            case "Search":
                bubbleText.text = $"{name} {searching} {room}";
                break;
        }
    }
}
