using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fear : MonoBehaviour
{
    private int fear;
    [SerializeField] public int multiplier = 1;
    [SerializeField] TextMeshProUGUI fearPercentage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FearLevels());
    }

    // Update is called once per frame
    void Update()
    {
        fearPercentage.text = $"Fear: {fear}%";
    }

    private IEnumerator FearLevels()
    {
        while (true)
        {
            float waitTime = Mathf.Max(10 - multiplier, 0);
            yield return new WaitForSeconds(waitTime);
            fear++;
        }
    }
}
