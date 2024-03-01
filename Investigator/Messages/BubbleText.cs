using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleText : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] TextMeshPro samText;
    [SerializeField] TextMeshPro simonText;
    [SerializeField] Transform samTextTransform;
    [SerializeField] Transform simonTextTransform;
    Dictionary<string, TextMeshPro> textBubbles = new Dictionary<string, TextMeshPro>();
    private string goingTo = " going to ";
    private string lightSwitch = " switching ";
    private string searching = " searching ";

    private void Start()
    {
        mainCamera = Camera.main;
        textBubbles = new Dictionary<string, TextMeshPro>()
        {
            { "Sam", samText},
            { "Simon", simonText}
        };
    }

    private void LateUpdate()
    {
        if (samTextTransform != null)
        {
            samTextTransform.LookAt(samTextTransform.position + mainCamera.transform.rotation * Vector3.forward,
                mainCamera.transform.rotation * Vector3.up);
        }

        if (simonTextTransform != null)
        {
            simonTextTransform.LookAt(simonTextTransform.position + mainCamera.transform.rotation * Vector3.forward,
                mainCamera.transform.rotation * Vector3.up);
        }
    }

    public void ShowMessage(MessageCarrier message)
    {
        string name = message.Name;
        string room = message.Room;
        string task = message.Task;

        switch (task)
        {
            default:
                textBubbles[name].text = "No Message";
                break;
            case "Travel":
                textBubbles[name].text = $"{name} {goingTo} {room}";
                break;
            case "Light":
                textBubbles[name].text = $"{name} {lightSwitch} {room} on";
                break;
            case "Search":
                textBubbles[name].text = $"{name} {searching} {room}";
                break;
            case "FindHiding":
                textBubbles[name].text = "Finding Hiding Spot";
                break;
            case "GrabEvidence":
                textBubbles[name].text = "Grabbing new evidence";
                break;
            case "MinorEvent":
                textBubbles[name].text = "Minor Event";
                break;
        }
    }
}
