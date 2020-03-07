using NewmanAssignment1.QuizData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewmanAssignment1.Helpers
{
    public partial class FileProcessor
    {
        public static List<string> questionBank;
        public static List<string> answerBank;
        public static List<string> answerBlock;
        public static List<string> answerKey;
        public static List<QuizData.DataQuiz> popQuiz = new List<QuizData.DataQuiz>();

        public static void ReadFile()
        {
            try
            {
                using (var reader = new StreamReader(Form1.openFileDialog1.FileName))
                {
                    Form1._quiz = new List<string>();
                    var textInBetween = new List<string>();
                    bool startTagFound = false;

                    while (!reader.EndOfStream)
                    {
                        string path = @"C:\quiz.json";
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        var line = reader.ReadLine();
                        if (String.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                        if (!startTagFound)
                        {
                            startTagFound = line.StartsWith("@Q");
                            Form1._quiz.Add(line);

                            continue;

                        }


                        bool endTagFound = line.StartsWith("@E");
                        if (endTagFound)
                        {
                            textInBetween.Clear();
                            Form1._quiz.Add(line);
                            continue;
                        }

                        textInBetween.Add(line);
                        Form1._quiz.Add(line);

                    }


                    PopulateQuestions();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
            }
        }

        public static void PopulateQuestions()
        {
            try
            {
                // Populate the Question Bank
                var questionIndex = new List<string>();
                var answerIndex = new List<string>();
                string result = string.Join(" ", Form1._quiz.ToArray());
                questionBank = new List<string>();
                answerKey = new List<string>();
                answerBank = Form1._quiz;
                MatchCollection questionMatchIndex = Regex.Matches(result, "@Q");


                var answerIndex2 = Enumerable.Range(0, answerBank.Count)
                 .Where(i => answerBank[i] == "@A")
                 .ToList();

                var endIndex = Enumerable.Range(0, answerBank.Count)
                .Where(i => answerBank[i] == "@E")
                .ToList();


                foreach (Match m in questionMatchIndex)
                {
                    questionIndex.Add(m.Index.ToString());
                    Debug.WriteLine(m.Index);


                }

                MatchCollection answerMatchIndex = Regex.Matches(result, "@A");

                foreach (Match m in answerMatchIndex)
                {
                    Console.WriteLine(m.Index);
                    answerIndex.Add(m.Index.ToString());

                }

                for (var i = 0; i < questionIndex.Count; i++)
                {
                    DataQuiz quiz = new DataQuiz();
                    var start = questionIndex[i];
                    int iStart = Int32.Parse(start.ToString());
                    var idCount = i + 1;
                    Debug.WriteLine("iStart = " + iStart);

                    var end = answerIndex[i];
                    int iEnd = Int32.Parse(end.ToString());

                    //Combined the string to get the answer
                    int pFrom = result.IndexOf("@Q", iStart) + "@Q".Length;
                    int pTo = result.IndexOf("@A", iEnd);
                    string pResult = result.Substring(pFrom, pTo - pFrom);
                    questionBank.Add(result.Substring(pFrom, pTo - pFrom));
                    quiz.Question = pResult;
                    quiz.QuestionID = idCount.ToString();
                    Debug.WriteLine("");


                    //poulate answers

                    var addAnswer = answerBank.Skip(answerIndex2[i] + 1).Take(endIndex[i] - (answerIndex2[i] + 1));
                    answerBlock = new List<string>();

                    Debug.WriteLine("");

                    foreach (string item in addAnswer)
                    {
                        answerBlock.Add(item);
                        Debug.WriteLine("");


                    }
                    string[] popAnswers = answerBlock.Select(c => c.ToString()).ToArray();
                    quiz.Answers = popAnswers;
                    answerKey.Add(answerBlock[0]);
                    quiz.AnswerKey = answerBlock[0];



                    popQuiz.Add(quiz);
                    Debug.WriteLine("");

                }

                string json = JsonConvert.SerializeObject(popQuiz, Formatting.Indented);
                string path = @"C:\quiz.json";
                if (File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine(json.ToString());
                        tw.Close();
                    }
                }
                else if (!File.Exists(path))
                {

                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine(json.ToString());
                        tw.Close();
                    }
                }

                Debug.WriteLine("");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                   $"Details:\n\n{ex.StackTrace}");
            }




        }
    }
}
