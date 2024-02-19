using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersonalitySpace
{
    public class Personality
    {
        public string investigatorType;
        private int hidingLevel;
        private int investigativeSkillLevel;
        private int gearLevel;
        private int movementSpeed;
        private int fearLevel;
        private bool plotArmour;
        private bool staysInVan;
        private bool reckless;
        private bool loneWolf;

        public Personality(string investigatorType, int hidingLevel, int investigativeSkillLevel, int gearLevel,
                            int movementSpeed, int fearLevel, bool plotArmour, bool staysInVan, bool reckless, bool loneWolf)
        {
            this.investigatorType = investigatorType;
            this.hidingLevel = hidingLevel;
            this.investigativeSkillLevel = investigativeSkillLevel;
            this.gearLevel = gearLevel;
            this.movementSpeed = movementSpeed;
            this.fearLevel = fearLevel;
            this.plotArmour = plotArmour;
            this.staysInVan = staysInVan;
            this.reckless = reckless;
            this.loneWolf = loneWolf;
        }

        public string GetInvestigatorType()
        {
            return investigatorType;
        }

        public int GetHidingLevel()
        {
            return hidingLevel;
        }

        public int GetInvestigativeSkillLevel()
        {
            return investigativeSkillLevel;
        }

        public int GetGearLevel()
        {
            return gearLevel;
        }

        public int GetMovementSpeed()
        {
            return movementSpeed;
        }

        public int GetFearLevel()
        {
            return fearLevel;
        }

        public bool GetPlotArmour()
        {
            return plotArmour;
        }

        public bool GetStaysInVan()
        {
            return staysInVan;
        }

        public bool GetReckless()
        {
            return reckless;
        }

        public bool GetLoneWolf()
        {
            return loneWolf;
        }
    }
}
