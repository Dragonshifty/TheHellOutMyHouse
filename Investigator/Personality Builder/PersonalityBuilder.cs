using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersonalitySpace
{
    public class PersonalityBuilder
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

        public PersonalityBuilder SetInvestigatorType(string investigatorType)
        {
            this.investigatorType = investigatorType;
            return this;
        }

        public PersonalityBuilder SetHidingLevel(int hidingLevel)
        {
            this.hidingLevel = hidingLevel;
            return this;
        }

        public PersonalityBuilder SetInvestigativeSkillLevel(int investigativeSkillLevel)
        {
            this.investigativeSkillLevel = investigativeSkillLevel;
            return this;
        }

        public PersonalityBuilder SetGearLevel(int gearLevel)
        {
            this.gearLevel = gearLevel;
            return this;
        }

        public PersonalityBuilder SetMovementSpeed(int movementSpeed)
        {
            this.movementSpeed = movementSpeed;
            return this;
        }

        public PersonalityBuilder SetFearLevel(int fearLevel)
        {
            this.fearLevel = fearLevel;
            return this;
        }

        public PersonalityBuilder SetPlotArmour(bool plotArmour)
        {
            this.plotArmour = plotArmour;
            return this;
        }

        public PersonalityBuilder SetStaysInVan(bool staysInVan)
        {
            this.staysInVan = staysInVan;
            return this;
        }

        public PersonalityBuilder SetReckless(bool reckless)
        {
            this.reckless = reckless;
            return this;
        }

        public PersonalityBuilder SetLoneWolf(bool loneWolf)
        {
            this.loneWolf = loneWolf;
            return this;
        }

        public Personality Build()
        {
            return new Personality(investigatorType, hidingLevel, investigativeSkillLevel, gearLevel,
                                    movementSpeed, fearLevel, plotArmour, staysInVan, reckless, loneWolf);
        }
    }
}
