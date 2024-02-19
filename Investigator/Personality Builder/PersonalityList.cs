using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersonalitySpace
{
    public struct PersonalityList
    {
        public Personality GetStudent()
        {
            Personality student = new PersonalityBuilder()
                .SetInvestigatorType("Student")
                .SetHidingLevel(2)
                .SetInvestigativeSkillLevel(2)
                .SetGearLevel(3)
                .SetMovementSpeed(7)
                .SetFearLevel(8)
                .SetPlotArmour(false)
                .SetStaysInVan(false)
                .SetReckless(true)
                .SetLoneWolf(true)
                .Build();
            return student;
        }

        public Personality GetAmateur()
        {
            Personality amateur = new PersonalityBuilder()
                .SetInvestigatorType("Amateur")
                .SetHidingLevel(4)
                .SetInvestigativeSkillLevel(4)
                .SetGearLevel(6)
                .SetMovementSpeed(6)
                .SetFearLevel(7)
                .SetPlotArmour(false)
                .SetStaysInVan(false)
                .SetReckless(false)
                .SetLoneWolf(false)
                .Build();
            return amateur;
        }

        public Personality GetEstateAgent()
        {
            Personality estateAgent = new PersonalityBuilder()
                .SetInvestigatorType("Estate Agent")
                .SetHidingLevel(2)
                .SetInvestigativeSkillLevel(2)
                .SetGearLevel(1)
                .SetMovementSpeed(5)
                .SetFearLevel(2)
                .SetPlotArmour(true)
                .SetStaysInVan(false)
                .SetReckless(false)
                .SetLoneWolf(false)
                .Build();
            return estateAgent;
        }

        public Personality GetExperienced()
        {
            Personality experienced = new PersonalityBuilder()
                .SetInvestigatorType("Experienced")
                .SetHidingLevel(6)
                .SetInvestigativeSkillLevel(6)
                .SetGearLevel(6)
                .SetMovementSpeed(6)
                .SetFearLevel(5)
                .SetPlotArmour(false)
                .SetStaysInVan(false)
                .SetReckless(false)
                .SetLoneWolf(false)
                .Build();
            return experienced;
        }

        public Personality GetVanMonitor()
        {
            Personality vanMonitor = new PersonalityBuilder()
                .SetInvestigatorType("Van Monitor")
                .SetHidingLevel(5)
                .SetInvestigativeSkillLevel(6)
                .SetGearLevel(6)
                .SetMovementSpeed(4)
                .SetFearLevel(7)
                .SetPlotArmour(false)
                .SetStaysInVan(true)
                .SetReckless(false)
                .SetLoneWolf(false)
                .Build();
            return vanMonitor;
        }

        public Personality GetProfessional()
        {
            Personality professional = new PersonalityBuilder()
                .SetInvestigatorType("Professional")
                .SetHidingLevel(10)
                .SetInvestigativeSkillLevel(9)
                .SetGearLevel(9)
                .SetMovementSpeed(8)
                .SetFearLevel(2)
                .SetPlotArmour(true)
                .SetStaysInVan(false)
                .SetReckless(false)
                .SetLoneWolf(true)
                .Build();
            return professional;
        }

        public Personality GetTrickster()
        {
            Personality trickster = new PersonalityBuilder()
                .SetInvestigatorType("Trickster")
                .SetHidingLevel(6)
                .SetInvestigativeSkillLevel(6)
                .SetGearLevel(6)
                .SetMovementSpeed(7)
                .SetFearLevel(3)
                .SetPlotArmour(false)
                .SetStaysInVan(false)
                .SetReckless(true)
                .SetLoneWolf(true)
                .Build();
            return trickster;
        }
    }
}
