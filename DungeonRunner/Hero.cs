using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRunner
{
    class Hero
    {
        public int health;
        public int strength;
        public int magic;
        public int armor;
        public int healthpotions;
        public int key;
        public int gold;
        public int heroLevel;


        public void UPDATEHERO()
        {
            magic = (int)(15) + heroLevel;
            health = (int)(190) + heroLevel * 10;
            strength = (int)(19) + heroLevel;
        }

        public Hero()
        {
            UPDATEHERO();


            armor = 5;
            healthpotions = 1;
            gold = 10;

        }

    }
}
