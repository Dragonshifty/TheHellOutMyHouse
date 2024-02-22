using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageCarrier 
{
    private string name;
    private string room;
    private string task;
    private TextMeshPro bubble;

    public MessageCarrier (string name, string room, string task, TextMeshPro bubble)
    {
        this.name = name;
        this.room = room;
        this.task = task;
        this.bubble = bubble;
    }

    public string Name { get { return name; } }
    public string Room { get { return room; } }
    public string Task { get { return task; } }
    public TextMeshPro Bubble { get { return bubble; } }
}
