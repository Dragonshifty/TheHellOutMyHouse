using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;
using TMPro;
using System;


public class Investigating : MonoBehaviour, IActivate 
{
    [SerializeField] int chanceOfDiscovery = 30;
    [SerializeField] TextMeshProUGUI status;
    
    string investigatorName;
    private int timesDiscovered;

    public void DoYourThing(string name, string room)
    {
        investigatorName = name;
        #pragma warning disable 4014
        StartInvestigation();
    }

    public async Task StartInvestigation()
    {
        await Investigate();
        // EventManager.FinishedInvestigating(gameObject);
        EventManager.FinishedTask(gameObject);
    }

    private async Task Investigate()
    {
        int timer = UnityEngine.Random.Range(3, 10);
        bool isSuccessful = false;
        
        while (timer >= 0)
        {
            await Task.Delay(1000);
            // Debug.Log($"{investigatorName} Investigating for {timer}");
            status.text = $"{investigatorName} Investigating for {timer}";
            if (EvidenceController.simonInRange && gameObject.tag.Equals("Simon"))
            {
                if (Detection()){ isSuccessful = true; break; }
            } 
            else if (EvidenceController.samInRange && gameObject.tag.Equals("Sam"))
            {
                if (Detection()) { isSuccessful = true; break; }
            }
            timer--;
        }   
        if (!isSuccessful) status.text = $"{investigatorName} Finished investigating";
        // Debug.Log($"{investigatorName} Finished investigating");
    }

    private bool Detection()
    {
        int randomTick = UnityEngine.Random.Range(0, 101);
        if (randomTick <= chanceOfDiscovery)
            {
                // Debug.Log($"Success! {investigatorName} Successful detections: {++timesDiscovered}");
                status.text = $"Success! {investigatorName} Successful detections: {++timesDiscovered}";
                return true;
            }
        return false;
    }
}