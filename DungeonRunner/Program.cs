using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DungeonRunner
{
    class Program
    {
        public static void highScore(Hero hero)
        {
            int newHighScore = hero.Gold + hero.Health + (hero.HealthPotions * 100)
            + (hero.HeroLevel * 100);
            StreamWriter File = new StreamWriter("HighScore.txt");
            File.Write("" + newHighScore);
            File.Close();
        }
        public static void Fight(Hero hero, int level)
        {
            Random r = new Random();
            Monster monster = new Monster(level + r.Next(1, 15));


            string happened = "";
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (key.KeyChar != 'x' && monster.Health > 0)
            {
                Console.Clear();

                Console.WriteLine("\n Hero:");
                Console.WriteLine($" Health: {hero.Health}");
                Console.WriteLine("\n Monster:");
                Console.WriteLine($" Health: {monster.Health}%");

                Console.WriteLine($"\n{happened}\n");

                Console.WriteLine("(F) = Hit Monster");
                Console.WriteLine("(T) = Cast Spell On Monster");
                Console.WriteLine("(G) = Steal From Monster");
                Console.WriteLine($"(H) = Drink Health Potion {hero.HealthPotions}");


                if (hero.Health <= 0)
                {
                    Console.Clear();
                    break;
                }

                key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case 'f':
                        if (hero.Strength - monster.Armor >= 0 && monster.Strength - hero.Armor >= 0)
                        {
                            monster.Health -= hero.Strength - monster.Armor;
                            hero.Health -= monster.Strength - hero.Armor;
                            happened = "You hit the monster but it hit you back";
                        }
                        else if (hero.Strength - monster.Armor >= 0 && monster.Strength - hero.Armor < 0)
                        {
                            monster.Health -= hero.Strength - monster.Armor;
                            happened = "You hit the monster and it couldn't damage you back";
                        }
                        else if (hero.Strength - monster.Armor < 0 && monster.Strength - hero.Armor >= 0)
                        {
                            hero.Health -= monster.Strength - hero.Armor;
                            happened = "You couldn't hit the monster but the monster hit you";
                        }
                        else
                        {
                            happened = "You couldn't hit the monster and the monster couldn't hit you";
                        }
                        break;
                    case 'h':
                        if (hero.HealthPotions >= 1)
                        {
                            happened = "You used a Health Potion. You feel Refreshed, but the monster hit you";
                            hero.Health += 100;
                            hero.HealthPotions -= 1;
                            hero.Health -= monster.Strength - hero.Armor;
                        }
                        else
                        {
                            happened = "You don't have any Health Potions, and the monster hit you";
                            hero.Health -= monster.Strength - hero.Armor;
                        }
                        break;
                    case 'g':
                        if (monster.Gold >= 10)
                        {
                            monster.Gold -= 10;
                            hero.Gold += 10;
                            hero.Health -= monster.Strength - hero.Armor;
                            happened = "You tried to steal from the monster and found some gold but it hit you";
                        }
                        else
                        {
                            hero.Health -= monster.Strength - hero.Armor;
                            happened = "You tried to steal from the monster but couldn't find any gold on it, and it hit you";
                        }
                        break;
                    case 't':
                        if (hero.Magic - monster.MagicResist >= 0 && monster.Strength - hero.Armor >= 0)
                        {
                            monster.Health -= hero.Magic - monster.MagicResist;
                            hero.Health -= monster.Strength - hero.Armor;
                            happened = "You cast a spell on the monster but it hit you back";
                        }
                        else if (hero.Magic - monster.MagicResist >= 0 && monster.Strength - hero.Armor < 0)
                        {
                            monster.Health -= hero.Magic - monster.MagicResist;
                            happened = "You cast a spell on the monster and it couldn't damage you back";
                        }
                        else if (hero.Magic - monster.MagicResist < 0 && monster.Strength - hero.Armor >= 0)
                        {
                            hero.Health -= monster.Strength - hero.Armor;
                            happened = "You couldn't cast a spell on the monster but the monster hit you";
                        }
                        else
                        {
                            happened = "You couldn't cast a spell on the monster and the monster couldn't hit you";
                        }
                            
                        break;
                }
                Console.Clear();
            }
        }

        public static void PrintMap(int[,] maze, int hX, int hY, Hero hero)
        {
            char c = ' ';
            for (int y = 0; y < maze.GetLength(0); y++)
            {
                Console.Write("\n  ");
                for (int x = 0; x < maze.GetLength(1); x++)
                {
                    if (x == hX && y == hY)
                    {
                        Console.Write((char)2);
                    }
                    else
                    {
                        switch (maze[y, x])
                        {
                            case 0: c = ' '; break;
                            case 1: c = '|'; break;
                            case 2: c = '@'; break;
                            case 3: c = '%'; break;
                            case 4: c = '*'; break;
                            case 5: c = ' '; break;
                            case 6: c = ' '; break;
                        }
                        Console.Write(c);
                    }
                }
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("   Options     Level:" + hero.HeroLevel);
            Console.WriteLine("   A = Move Left       D = Move Right");
            Console.WriteLine("   S = Move Down       W = Move Up");
            Console.Write("   HeroStats: " + "Strenght  " + hero.Strength + "   " + "Health  " + hero.Health);
            Console.WriteLine("\n" + "   Gold: " + hero.Gold + "   press b to buy a level for 100 Gold");
            Console.WriteLine("\n " + " ? = player // # = wall // @ = healthpotion // % = key // * = locked door ");
            Console.WriteLine("\n");
            Console.WriteLine("   X To Exit Game");
        }

        static void Main(string[] args)
        {
            Hero hero = new Hero();

            int heroX = 1;
            int heroY = 1;

            int[,] maze =
{                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                 { 1,0,1,6,0,0,0,0,0,0,0,6,0,0,0,0,0,0,6,1, },
                 { 1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1, },
                 { 1,0,1,0,1,1,0,0,0,0,0,0,0,1,0,0,0,1,0,1, },
                 { 1,6,0,0,2,1,0,1,1,1,1,1,0,1,0,1,0,1,0,1, },
                 { 1,1,1,1,1,1,0,1,0,6,0,1,0,0,0,1,2,0,0,1, },
                 { 1,0,0,0,0,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1, },
                 { 1,0,1,1,0,0,0,1,0,1,0,1,1,1,1,1,0,0,0,1, },
                 { 1,6,1,1,2,1,6,0,0,1,0,0,0,6,0,0,0,1,6,1, },
                 { 1,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1, },
                 { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5,1, },             };
            while (true)
            {
                Console.Clear();
                if (maze[heroY, heroX] == 6)
                {
                    Fight(hero, (int)(maze[heroY, heroX]));
                    maze[heroY, heroX] = 0;
                }

                if (maze[heroY, heroX] == 2)
                {
                    hero.HealthPotions += 1;
                    maze[heroY, heroX] = 0;
                }

                if (maze[heroY, heroX] == 3)
                {
                    hero.Key += 1;
                    maze[heroY, heroX] = 0;
                }

                if (maze[heroY, heroX] == 4 && hero.Key <= 1)
                {
                    Console.Clear();
                    Console.WriteLine("You Won The Game!");
                    Console.WriteLine("Your Score has ben saved to your binFolder");
                    Console.WriteLine("Press Enter to exit");
                    highScore(hero);

                    Console.ReadLine();
                    break;
                }
                if (hero.Health <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("\n The Monster Killed you!");
                    Console.WriteLine("You Lost The GAME!");
                    Console.WriteLine("\n Press Enter To Exit");
                    Console.ReadLine();
                    break;
                }

                PrintMap(maze, heroX, heroY, hero);
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar == 'w' && maze[heroY - 1, heroX] != 1 && maze[heroY + 1, heroX] != 5) heroY--;
                if (key.KeyChar == 'a' && maze[heroY, heroX - 1] != 1 && maze[heroY + 1, heroX] != 5) heroX--;
                if (key.KeyChar == 's' && maze[heroY + 1, heroX] != 1 && maze[heroY + 1, heroX] != 5) heroY++;
                if (key.KeyChar == 'd' && maze[heroY, heroX + 1] != 1 && maze[heroY + 1, heroX] != 5) heroX++;
                if (key.KeyChar == 'b' && hero.Gold >= 100) { hero.Gold -= 100; hero.HeroLevel++; hero.UPDATEHERO(); }

                if (key.KeyChar == 'x')
                    break;
            }
        }
    }
}
