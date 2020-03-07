using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewmanAssignment1.Services
{
    class QuizService
    {
        public static List<QuizData.DataQuiz> quizItems;
        public static int count = 0;
        public static int randomNumber;
        public static string correctAnswer;
        public static string question { get; set; }
        public static int answerKey { get; set; }
        public static string[] answer { get; set; }


        public static void SetQuizQuestions()
        {
            using (StreamReader r = new StreamReader(@"c:\quiz.json"))
            {
                string json = r.ReadToEnd();
                quizItems = JsonConvert.DeserializeObject<List<QuizData.DataQuiz>>(json);
                count = GetCount();
                Random rnd = new Random();
                randomNumber = rnd.Next(count);
                answerKey = GetAnswerKey();
                correctAnswer = GetCorrectAnswer();
                question = GetQuestion();
                answer = GetAnswer();
                Debug.WriteLine(randomNumber);

            }

        }


        public static string GetQuestion()
        {
            var pollQuestion = quizItems[randomNumber].Question;
            return pollQuestion;

        }

        public static string[] GetAnswer()
        {
            string[] pollAnswer = quizItems[randomNumber].Answers;
            return pollAnswer;

        }

        public static string GetCorrectAnswer()
        {
            string GetCorrectAnswer = quizItems[randomNumber].Answers[answerKey];
            Console.WriteLine(GetCorrectAnswer);
            return GetCorrectAnswer;
        }
        public static int GetAnswerKey()
        {
            int pollAnswerKey = Int32.Parse(quizItems[randomNumber].AnswerKey);
            return pollAnswerKey;

        }

        private static int GetCount()
        {
            count = quizItems.Count();
            return count;
        }

        public static void populateAnswers()
        {
            answer = GetAnswer();
            Console.WriteLine("");

        }

    }



}
