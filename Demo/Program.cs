using ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static Menu menu;

        static void Main(string[] args)
        {
            menu = new Menu()
            {
                new MenuItem("Colors")
                {
                    new MenuItem("Red", ()=>DrawLine(ConsoleColor.Red)),
                    new MenuItem("Green", ()=>DrawLine(ConsoleColor.Green)),
                    new MenuItem("Blue", ()=>DrawLine(ConsoleColor.Blue))
                },
                new MenuItem("Sounds")
                {
                    new MenuItem("Low", ()=>Beep(1000)),
                    new MenuItem("Medium", ()=>Beep(2000)),
                    new MenuItem("Hight", ()=>Beep(4000))
                },
                new MenuItem("Random number", PrintRandomNumber),
            };
            menu.Add(new MenuItem("Exit", () => menu.Hide()));

            menu["Colors"].Add(new MenuItem("Yellow", () => DrawLine(ConsoleColor.Yellow)));
            menu[1].Add(new MenuItem("Insane", () => Beep(15000)));

            var selfReferenceItem = new MenuItem("Abyss");
            selfReferenceItem.Add(selfReferenceItem);
            menu.Insert(3, selfReferenceItem);

            menu.ShowNavigationBar = true;
            menu.CyclicScrolling = true;
            menu.Show();
            Console.ReadKey(true);
        }

        static void Beep(int frequency)
        {
            Console.Beep(frequency, 750);
        }

        static void DrawLine(ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(new string('\u2588', 20));
            Console.ForegroundColor = oldColor;
        }

        static void PrintRandomNumber()
        {
            Console.WriteLine("Your number is " + new Random().Next(100));
            Console.ReadKey(true);
            menu.Show();
        }
    }
}
