using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreatureSpace
{
    public class Creature 
    {
        private string creatureType;
        private float speed;

        public Creature(string creatureType, float speed)
        {
            this.creatureType = creatureType;
            this.speed = speed;
        }
    }
}

