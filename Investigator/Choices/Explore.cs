using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore 
{
    public string ChooseNewRoom(Dictionary<string, bool> roomsVisited)
    {
        bool allExplored = true;
        List <string> allRooms = new List<string>();
        List<string> unexplored = new List<string>();

        foreach (KeyValuePair<string, bool> entry in roomsVisited)
        {
            allRooms.Add(entry.Key);
            if (entry.Value == false)
            {
                unexplored.Add(entry.Key);
                allExplored = false;
            }
        }

        if (!allExplored)
        {
            if (unexplored.Count != 0)
            {
                if (unexplored.Count == 1) return unexplored[0];
                int index = UnityEngine.Random.Range(0, unexplored.Count);
                return unexplored[index];
            }
        }

        int roomsIndex = UnityEngine.Random.Range(1, allRooms.Count);
        return allRooms[roomsIndex];
    }
}
