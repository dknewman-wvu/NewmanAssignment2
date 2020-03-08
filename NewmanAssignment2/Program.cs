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
        public static string quizFilePath;

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
            Console.WriteLine("1. Set Question File Location");
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
                        Console.Clear();
                        Console.WriteLine("Set the path to the question file");
                        quizFilePath = Console.ReadLine();
                        FileProcessor.ReadFile();
                        Console.Clear();
                        ShowMenu();
                        break;

                    case MainMenu.SETTIMER:
                        Console.WriteLine("TIMER");
                        break;

                    case MainMenu.STARTQUIZ:
                        Console.Clear();
                        Console.WriteLine("Question");
                        Console.WriteLine(" ");
                        QuizService.SetQuizQuestions();
                        string newQuestion = QuizService.question.Trim();
                        Console.Write(Regex.Replace(newQuestion, "^[0-9]+", string.Empty) + "\n");
                        isQuizStarted = true;
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");
                        GenerateAnswers();
                        Console.WriteLine("\n");
                        Console.WriteLine("Please select your answer or type EXIT to quit.");
                        Console.ReadLine();
                        
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

        public static void GenerateAnswers()
        {

            int num = 1;

            foreach (string answer in QuizService.answer.Skip(1))
            {
                Console.WriteLine(" " + num + ": " + answer);
                num = num + 1;
            }

            getAnswerKey = QuizService.answerKey.ToString();
            Debug.WriteLine("ANSWER KEY: " + getAnswerKey);
            var answerPick = new object();

        }





    }
}
