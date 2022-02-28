using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRunner
{
    class Hero
    {
        public int Health;
        public int Strength;
        public int Magic;
        public int Armor;
        public int HealthPotions;
        public int Key;
        public int Gold;
        public int HeroLevel;


        public void UPDATEHERO()
        {
            Magic = 15 + HeroLevel;
            Health = 190 + HeroLevel * 10;
            Strength = 19 + HeroLevel;
        }

        public Hero()
        {
            HeroLevel = 1;
            UPDATEHERO();

            Armor = 5;
            HealthPotions = 1;
            Gold = 10;
        }

    }
}
