using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NewmanAssignment2.Helpers;
using NewmanAssignment2.Services;

namespace NewmanAssignment2
{
    public  class Program
    {
        public static List<string> _quiz;
        public static string getAnswerKey;
        public static string answserChosen;
        public static bool isQuizStarted;

        static void Main(string[] args)
        {
            ShowMenu();

        }

        private static void ShowMenu()
        {
            Console.WriteLine("CSS 533 Quiz!");
            Console.WriteLine();
            Console.WriteLine("STUDENT: David Newman");
            Console.WriteLine();
            Console.WriteLine("1. Upload Question File");
            Console.WriteLine("2. Set the timer");
            Console.WriteLine("3. Start Quiz");
            Console.WriteLine("4. QUIT");

            MainMenu mmChoice = MainMenu.UNASSIGNED;
            string sInput = Console.ReadLine();
            if (Enum.TryParse(sInput, out mmChoice))
            {
                switch (mmChoice)
                {
                    case MainMenu.LOADQUIZ:
                        Console.WriteLine("LOAD");
                        Console.Clear();
                        ShowMenu();
                        break;
                    case MainMenu.SETTIMER:
                        Console.WriteLine("TIMER");
                        break;
                    case MainMenu.STARTQUIZ:
                        Console.WriteLine("START");
                        break;
                    case MainMenu.QUIT:
                        Console.WriteLine("QUIT");
                        break;
                    case MainMenu.UNASSIGNED:
                        break;
                }
            }
        }

        private enum MainMenu
        {
            LOADQUIZ = 1,
            SETTIMER,
            STARTQUIZ,
            QUIT,
            UNASSIGNED
        }




    }
}
