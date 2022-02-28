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
            int newHighScore = hero.gold + hero.health + (hero.healthpotions * 100)
            + (hero.heroLevel * 100);
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

            while (key.KeyChar != 'x' && monster.health > 0)
            {
                Console.Clear();

                Console.WriteLine("\n Hero:");
                Console.WriteLine(" Health: " + hero.health);
                Console.WriteLine("\n Monster:");
                Console.WriteLine(" Health: " + monster.health + "%");

                Console.WriteLine("\n");
                Console.WriteLine("" + happened);
                Console.WriteLine("\n");

                Console.WriteLine("(F) = Hit Monster");
                Console.WriteLine("(T) = Cast Spell On Monster");
                Console.WriteLine("(G) = Steal From Monster");
                Console.WriteLine("(H) = Drink Health Potion" + " " + hero.healthpotions);


                if (hero.health <= 0)
                {
                    Console.Clear();
                    break;
                }

                key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case 'f':
                        monster.health -= (int)(hero.strength - monster.armor);
                        hero.health -= (int)(monster.strength - hero.armor);
                        happened = "you hit the monster but it hit you back";
                        break;
                    case 'h':
                        if (hero.healthpotions >= 1)
                        {
                            happened = "you used a Health Potion. You feel Refreshed";
                            hero.health += 100;
                            hero.healthpotions -= 1;
                        }
                        break;
                    case 'g':
                        if (monster.gold >= 10)
                        {
                            monster.gold -= (int)(10);
                            hero.gold += (int)(10);
                            hero.health -= (int)(monster.strength - hero.armor);
                            happened = "you tryed to steal from the monster and found some gold but it hit you";
                            if (monster.gold <= 10)
                            {

                                happened = "you tryed to steal from the monster but couldent find any gold on it, but it hit you";
                                hero.health -= (int)(monster.strength - hero.armor);

                            }
                        }
                        break;
                    case 't':
                        monster.health -= (int)(hero.magic - monster.magicResist);
                        hero.health -= (int)(monster.strength - hero.armor);
                        happened = "you cast a spell on the monster and it hit you back";
                        break;
                }
                Console.Clear();
            }
        }

        public static void printMap(int[,] maze, int hX, int hY, Hero hero)
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
                            case 1: c = '#'; break;
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
            Console.WriteLine("   Options     Level:" + hero.heroLevel);
            Console.WriteLine("   A = Move Left       D = Move Right");
            Console.WriteLine("   S = Move Down       W = Move Up");
            Console.Write("   HeroStats: " + "Strenght  " + hero.strength + "   " + "Health  " + hero.health);
            Console.WriteLine("\n" + "   Gold: " + hero.gold + "   press b to buy a level for 100 Gold");
            Console.WriteLine("\n " + " ? = player // # = wall // @ = healthpotion // % = key // * = locked door ");
            Console.WriteLine("\n");
            Console.WriteLine("   X To Exit Game");
        }

        static void Main(string[] args)
        {
            // Console.ForegroundColor = ConsoleColor.White;
            //   Console.ForegroundColor = ConsoleColor.Black; 


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
                    hero.healthpotions += 1;
                    maze[heroY, heroX] = 0;
                }

                if (maze[heroY, heroX] == 3)
                {
                    hero.key += 1;
                    maze[heroY, heroX] = 0;
                }

                if (maze[heroY, heroX] == 4 && hero.key <= 1)
                {
                    Console.Clear();
                    Console.WriteLine("You Won The Game!");
                    Console.WriteLine("Your Score has ben saved to your binFolder");
                    Console.WriteLine("Press Enter to exit");
                    highScore(hero);

                    Console.ReadLine();
                    break;
                }
                if (hero.health <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("\n The Monster Killed you!");
                    Console.WriteLine("You Lost The GAME!");
                    Console.WriteLine("\n Press Enter To Exit");
                    Console.ReadLine();
                    break;
                }

                printMap(maze, heroX, heroY, hero);
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar == 'w' && maze[heroY - 1, heroX] != 1 && maze[heroY + 1, heroX] != 5) heroY--;
                if (key.KeyChar == 'a' && maze[heroY, heroX - 1] != 1 && maze[heroY + 1, heroX] != 5) heroX--;
                if (key.KeyChar == 's' && maze[heroY + 1, heroX] != 1 && maze[heroY + 1, heroX] != 5) heroY++;
                if (key.KeyChar == 'd' && maze[heroY, heroX + 1] != 1 && maze[heroY + 1, heroX] != 5) heroX++;
                if (key.KeyChar == 'b' && hero.gold >= 100) { hero.gold -= 100; hero.heroLevel++; hero.UPDATEHERO(); }

                if (key.KeyChar == 'x')
                    break;

            }
        }
    }
}
