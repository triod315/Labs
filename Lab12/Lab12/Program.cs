using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12_Console
{

      enum Answer { a, b, c, d };
        class Question
        {
            /// <summary>
            /// The text of quesion.
            /// </summary>
            private string text;
            public string Text
            {
                get => text;
                set
                {
                    if (value != "")
                        text = value;
                    else
                    {
                        throw new Exception("Incorrect format of text in question");
                    }
                }
            }

            private double mark;
            public double Mark
            {
                get => mark;
                set
                {
                    if (value > 0)
                        mark = value;
                    else
                        throw new Exception("Mark can`t be below 0");
                }
            }

            public override string ToString() => $"Question {text} \n({mark} points)";


        }

        class SingleChoiseQuestion : Question
        {
            private Answer correct_answer;
            public Answer Correct_answer
            {
                get => correct_answer;
                set => correct_answer = value;
            }

            /// <summary>
            /// Checks the answer.
            /// </summary>
            /// <returns>mark</returns>
            /// <param name="answer">Answer.</param>
            public double CheckAnswer(Answer answer)
            {
                if (answer == Correct_answer)
                    return Mark;
                return 0;
            }

            /// <summary>
            /// Default consturctor
            /// </summary>
            public SingleChoiseQuestion() : this("Question 1", Answer.a, 0) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="Lab12.SingleChoiseQuestion"/> class.
            /// </summary>
            /// <param name="correct_answ">Correct answer.</param>
            public SingleChoiseQuestion(string q_text, Answer correct_answ, double q_mark)
            {
                Text = q_text;
                Mark = q_mark;
                Correct_answer = correct_answ;
            }
        }

        class ShortAnswerQuestion : Question
        {
            private string correct_answer;
            public string Correct_answer
            {
                get => correct_answer;
                set
                {
                    if (value.Length >= 0)
                        correct_answer = value;
                    else
                        throw new Exception("Incorrect answer format");
                }
            }

            /// <summary>
            /// Checks the answer.
            /// </summary>
            /// <returns>mark</returns>
            /// <param name="answer">Answer.</param>
            /// <param name="question">Question.</param>
            public double CheckAnswer(String answer)
            {
                if (answer == Correct_answer)
                    return Mark;
                return 0;
            }

            /// <summary>
            /// Default constructor
            /// </summary>
            public ShortAnswerQuestion() : this("Question", "empty filed", 0) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="Lab12.ShortAnswerQuestion"/> class.
            /// </summary>
            /// <param name="q_text">text of questin.</param>
            /// <param name="correct_answ">Correct answer.</param>
            /// <param name="q_mark">Q mark.</param>
            public ShortAnswerQuestion(string q_text, string correct_answ, double q_mark)
            {
                Text = q_text;
                Correct_answer = correct_answ;
                Mark = q_mark;
            }


        }

        class QuestionsCollection<T>
        {
            private T[] arr;
            private int length;
            /// <summary>
            /// Constructor of colection
            /// </summary>
            /// <param name="size">count of elements in collection</param>
            public QuestionsCollection(int size)
            {
                arr = new T[size];
                length = size;
            }


            /// <summary>
            /// Length of collection
            /// </summary>
            public int Length => length;

            /// <summary>
            /// indexator
            /// </summary>
            /// <param name="i">index</param>
            /// <returns>QuestionCollection element</returns>
            public T this[int i]
            {
                get
                {
                    if (i >= 0 && i < length)
                        return arr[i];
                    else
                        throw new Exception("Question not found (index out of range)");
                }
                set
                {
                    if (i >= 0 && i < length && value != null)
                        arr[i] = value;
                    else
                        throw new Exception("Wrong format of question");
                }
            }
        }

        class Test
        {
            private QuestionsCollection<SingleChoiseQuestion> single_choise_questions;

            public QuestionsCollection<SingleChoiseQuestion> Single_choise_questions
            {
                set
                {
                    if (value != null)
                        single_choise_questions = value;
                    else
                        throw new Exception("Wrong format of QuestionsCollections");
                }
                get=>single_choise_questions;
            }

            private QuestionsCollection<ShortAnswerQuestion> short_answer_questions;

            public QuestionsCollection<ShortAnswerQuestion> Short_answer_questions
            {
                set
                {
                    if (value != null)
                        short_answer_questions = value;
                    else
                        throw new Exception("Wrong format of QuestionsCollections");
                }
                get => short_answer_questions;
            }

            private string name;
            public string Name
            {
                get => name;
                set
                {
                    if (value.Length != 0)
                        name = value;
                    else
                        name = "Empty_name";
                }
            }

            private double max_mark;
            public double Max_Mark => max_mark;

            /// <summary>
            /// Default constructor
            /// </summary>

            public Test() : this("Unnamed test", 0, 0) { }

            public Test(string test_name, int saq_count, int siq_count)
            {
                name = test_name;
                max_mark = 0;
                single_choise_questions = new QuestionsCollection<SingleChoiseQuestion>(siq_count);
                short_answer_questions = new QuestionsCollection<ShortAnswerQuestion>(saq_count);
            }

            public static Test operator +(Test test1, Test test2)
            {
                Test test = new Test(test1.name + test2.name, test1.short_answer_questions.Length + test2.short_answer_questions.Length, test1.single_choise_questions.Length + test2.single_choise_questions.Length);
                test.name = test1.name + " " + test2.name;

                for (int i = 0; i < test1.single_choise_questions.Length; i++)
                {
                    test.single_choise_questions[i] = test1.single_choise_questions[i];
                }

                for (int i = 0; i < test2.single_choise_questions.Length; i++)
                {
                    test.single_choise_questions[i + test1.single_choise_questions.Length] = test2.single_choise_questions[i];
                }

                for (int i = 0; i < test1.short_answer_questions.Length; i++)
                {
                    test.short_answer_questions[i] = test1.short_answer_questions[i];
                }

                for (int i = 0; i < test2.short_answer_questions.Length; i++)
                {
                    test.short_answer_questions[i + test1.short_answer_questions.Length] = test2.short_answer_questions[i];
                }

                test.max_mark = test1.max_mark + test2.max_mark;
                return test;
            }

            public override string ToString() => $"{this.name}";

        }

        class StudentsTest
        {
            private string students_name;
            public string Students_name
            {
                get =>students_name;
                set
                {
                    if (value.Length != 0) students_name = value;
                }
            }
            private Test s_test;

            public Test S_test => s_test;

            private double mark;
            public double Mark
            {
                get
                {
                    mark = 0;
                    for (int i = 0; i < siq_answers.Length; i++)
                    {
                        mark += s_test.Single_choise_questions[i].CheckAnswer(siq_answers[i]);
                    }

                    for (int i = 0; i < short_answers.Length; i++)
                    {
                        mark += s_test.Short_answer_questions[i].CheckAnswer(short_answers[i]);
                    }
                    return mark;
                }
            }

            private Answer[] siq_answers;
            public Answer[] Siq_answes
            {
                get => siq_answers;
                set => siq_answers = value;
            }

            private string[] short_answers;
            public string[] Short_answers
            {
                get => short_answers; 
                set => short_answers = value;
            }

            public StudentsTest(string s_name, Test test)
            {
                students_name = s_name;
                s_test = test;
                siq_answers = new Answer[test.Single_choise_questions.Length];
                short_answers = new string[test.Short_answer_questions.Length];
            }

            public override string ToString() => $"\n\nResult\nStudents name:{students_name} \nTest: {s_test.Name}\nResult: {Mark}\n\n";
        }


        class Program
        {
            static Test CreateTest(string test_name, string[] q_texts, string[] correct_saq_answer, Answer[] correct)
            {
                Test test = new Test(test_name, correct_saq_answer.Length, correct.Length);
                Console.WriteLine("Test'" + test.ToString() + "' created successfully");
                for (int i = 0; i < correct_saq_answer.GetLength(0); i++)
                {
                    test.Short_answer_questions[i] = new ShortAnswerQuestion(q_texts[i], correct_saq_answer[i], 10);
                }
                for (int i = 0; i < correct.Length; i++)
                {
                    test.Single_choise_questions[i] = new SingleChoiseQuestion(q_texts[i + correct_saq_answer.GetLength(0)], correct[i], 5);
                }
                return test;
            }


            static StudentsTest DoTest(Test test, string user_name)
            {
                Console.WriteLine("----------------------------Exam begin-------------------------------------");
                Console.WriteLine(test.ToString());

                StudentsTest s_test = new StudentsTest(user_name, test);

                Answer[] sicansw = new Answer[test.Single_choise_questions.Length];//single choise user answer

                for (int i = 0; i < test.Single_choise_questions.Length; i++)
                {
                    Console.WriteLine(test.Single_choise_questions[i].ToString());
                    s_test.Siq_answes[i] = test.Single_choise_questions[i].Correct_answer;
                }

                string[] saq_answers = new string[test.Short_answer_questions.Length];

                for (int i = 0; i < test.Short_answer_questions.Length; i++)
                {
                    Console.WriteLine(test.Short_answer_questions[i].ToString());
                    s_test.Short_answers[i] = test.Short_answer_questions[i].Correct_answer;
                }

                Console.WriteLine("-------------------------------Exam end------------------------------------\n");

                Console.WriteLine(s_test.ToString());
                return s_test;
            }
            public static void Main(string[] args)
            {

                string[] tasks1 = { "Are you ready for exam? ", "Are you ready to cry?", "Secelct your weapoon" };
                string[] test1_correct_answ = { "Yes", "No" };
                Answer[] test1 = { Answer.a };
                Test students_pain = CreateTest("Programing", tasks1, test1_correct_answ, test1);

                StudentsTest s_test1 = DoTest(students_pain, "Sacrifice");

                string[] tasks2 = { "8/4", "1/2" };
                string[] test2_correct_answ = { "2", "0.5" };
                Answer[] test2 = new Answer[0];

                Test highwaytohell = CreateTest("Statistics", tasks2, test2_correct_answ, test2);
                StudentsTest s_test2 = DoTest(highwaytohell, "Sacrafice");

                Test fatality = highwaytohell + students_pain;

                StudentsTest s_test3 = DoTest(fatality, "Sacrafice");


            }
        }
}
