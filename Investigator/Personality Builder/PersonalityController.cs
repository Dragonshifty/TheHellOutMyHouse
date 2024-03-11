using System.Collections;
using System.Collections.Generic;
using PersonalitySpace;
using UnityEngine;

public class PersonalityController 
{
    public Personality GetPersonality()
    {
        PersonalityList personalityList = new PersonalityList();

        string[] personalityListEasy = new string[] {"Student", "Amateur", "Experienced", "Professional"};
        string[] personalityListFull = new string[] 
        {
            "Student", 
            "Amateur",
            "Experienced",
            "Professional",
            "Van Monitor",
            "Trickster",
            "Estate Agent"
        };

        string choice = personalityListEasy[UnityEngine.Random.Range(0, 3)];

        switch (choice)
        {
            case "Student":
                return personalityList.GetStudent();
            case "Amateur":
                return personalityList.GetAmateur();     
            case "Experienced":
                return personalityList.GetExperienced();
            case "Professional":
                return personalityList.GetProfessional();
        }
        return null;
    }

    public bool InvestigatorLevelCheck(Personality personality)
    {
        int searchLevel = personality.GetInvestigativeSkillLevel();
        if (searchLevel >= UnityEngine.Random.Range(0, 11)) return true;
        
        return false;
    }

    public bool HidingSpotLevelCheck(Personality personality)
    {
        int hidingLevel = personality.GetHidingLevel();
        if (hidingLevel >= UnityEngine.Random.Range(0, 11)) return true;

        return false;
    }

    public bool FearLevelCheck(Personality personality)
    {
        int fearLevel = personality.GetFearLevel();
        if (fearLevel <= UnityEngine.Random.Range(0, 11)) return true;
        return false;
    }
}
