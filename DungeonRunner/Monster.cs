using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRunner
{
    class Monster
    {
        public int Health;
        public int Strength;
        public int Armor;
        public int Gold;
        public int MagicResist;


        public Monster(int level)
        {
            Health = 100;
            Strength = level;
            Armor = (level / 5)*3;
            Gold = 10 * level;
            MagicResist = level / 3;
        }
    }
}
