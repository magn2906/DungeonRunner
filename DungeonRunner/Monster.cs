using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRunner
{
    class Monster
    {
        public int health;
        public int strength;
        public int armor;
        public int gold;
        public int magicResist;


        public Monster(int level)
        {
            health = 100;
            strength = 1 * level;
            armor = 2 + level;
            gold = 10 * level;
            magicResist = 30 - level * 2;
        }
    }
}
