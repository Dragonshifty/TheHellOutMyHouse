using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreatureSpace
{
    public class CreatureBuilder 
    {
        private string creatureType;
        private float speed;

        public CreatureBuilder SetCreatureType (string creatureType)
        {
            this.creatureType = creatureType;
            return this;
        }

        public CreatureBuilder SetSpeed (float speed)
        {
            this.speed = speed;
            return this;
        }

        public Creature Build()
        {
            return new Creature(creatureType, speed);
        }
    }
}

