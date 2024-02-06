using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;


public class Investigating : MonoBehaviour, IActivate 
{
    [SerializeField] int chanceOfDiscovery = 30;
    private int timesDiscovered;

    public void DoYourThing(NavMeshAgent investigator)
    {
        StartInvestigation();
    }

    public async Task StartInvestigation()
    {
        await Investigate();
        EventManager.FinishedInvestigating();
    }

    private async Task Investigate()
    {
        int timer = Random.Range(3, 7);
        
        while (timer >= 0)
        {
            await Task.Delay(1000);
            Debug.Log($"Investigating for {timer}");
            if (EvidenceController.inRange)
            {
                int randomTick = Random.Range(0, 101);
                if (randomTick <= chanceOfDiscovery)
                {
                    Debug.Log($"Success! Successful detections: {++timesDiscovered}");                   
                }
            }
            timer--;
        }   
        Debug.Log("Finished investigating");
    }
}