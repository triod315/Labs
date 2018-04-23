using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
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

    /// <summary>
    /// Class for question with short text answer
    /// </summary>
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

    /// <summary>
    /// class test 
    /// </summary>
    class Test
    {
        protected QuestionsCollection<SingleChoiseQuestion> single_choise_questions;

        public QuestionsCollection<SingleChoiseQuestion> Single_choise_questions
        {
            set
            {
                if (value != null)
                    single_choise_questions = value;
                else
                    throw new Exception("Wrong format of QuestionsCollections");
            }
            get => single_choise_questions;
        }

        protected QuestionsCollection<ShortAnswerQuestion> short_answer_questions;

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

        protected string name;
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

        protected double max_mark;
        public virtual double Max_Mark => max_mark;

        /// <summary>
        /// Default constructor
        /// </summary>

        public Test() : this("Unnamed test", 0, 0) { }

        /// <summary>
        /// Create test
        /// </summary>
        /// <param name="test_name">test name</param>
        /// <param name="saq_count">Count of short answer question</param>
        /// <param name="siq_count">Count of single answer question</param>
        public Test(string test_name, int saq_count, int siq_count)
        {
            name = test_name;
            max_mark = 0;
            single_choise_questions = new QuestionsCollection<SingleChoiseQuestion>(siq_count);
            short_answer_questions = new QuestionsCollection<ShortAnswerQuestion>(saq_count);
        }

        public override string ToString() => $"{this.name}";

    }

    class Module : Test
    {
        private string theme;
        /// <summary>
        /// Test theme
        /// </summary>
        public string Theme
        {
            get => theme;
            set
            {
                if (value.Length != 0)
                    theme = value;
                else
                    throw new Exception("Illegal thme format");
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Module() : this("Test","test",0,0) { }

        /// <summary>
        /// Create module test
        /// </summary>
        /// <param name="test_theme">test topic</param>
        /// <param name="test_name">test name</param>
        /// <param name="saq_count">Count of short answer questions</param>
        /// <param name="siq_count">Count of single answer questions</param>
        public Module(string test_theme,string test_name, int saq_count, int siq_count):base(test_name,saq_count,siq_count)
        {
            Theme = test_theme;
        }
    }

    interface ITakeExam
    {
        double TakeOrally(Answer[] scq_answers, string[] saq_answers, double students_sem_mark);
        double TakeInWriting(Answer[] scq_answers, string[] saq_answers, double students_sem_mark);
    }

    class Exam:Test,ITakeExam
    {
        private double semester_mark;

        public double Semester_mark
        {
            get => semester_mark;
            set
            {
                if (value >= 0) semester_mark = value;
                else throw new Exception("Unreal semester mark");
            }
        }
        private double exam_max_mark;

        public double Exam_max_mark
        {
            get => exam_max_mark;
        }

        public override double Max_Mark => exam_max_mark + semester_mark;


        /// <summary>
        /// Take exam orally
        /// </summary>
        /// <param name="scq_answers">Single choise question answers</param>
        /// <param name="saq_answers">Short answer question answers</param>
        /// <param name="students_sem_mark">Student`s semester mark</param>
        /// <returns>Exam result(exam mark)</returns>
        public double TakeOrally(Answer[] scq_answers, string[] saq_answers,double students_sem_mark) => TakeInWriting(scq_answers, saq_answers,students_sem_mark);

        /// <summary>
        /// Take exam in writing
        /// </summary>
        /// <param name="scq_answers">Single choise question answers</param>
        /// <param name="saq_answers">Short answer question answers</param>
        /// <param name="students_sem_mark">Student`s semester mark</param>
        /// <returns>Exam result(exam mark)</returns>
        public double TakeInWriting(Answer[] scq_answers, string[] saq_answers,double students_sem_mark )
        {
            double mark = 0;
            for (int i = 0; i < scq_answers.Length; i++)
            {
                mark += Single_choise_questions[i].CheckAnswer(scq_answers[i]);
            }

            for (int i = 0; i < saq_answers.Length; i++)
            {
                mark +=Short_answer_questions[i].CheckAnswer(saq_answers[i]);
            }
            return mark+semester_mark;
        }



        public Exam() : this("Exam", 0, 0, 0) { }
        /// <summary>
        /// Create exam
        /// </summary>
        /// <param name="test_name">exam name</param>
        /// <param name="saq_count">Count of short answer questions</param>
        /// <param name="scq_count">Count of single answer questions</param>
        /// <param name="sem_mark">Maximal value of semester mark</param>
        public Exam(String test_name, int saq_count, int scq_count,double sem_mark):base (test_name,saq_count,scq_count)
        {
            Semester_mark = sem_mark;
        }

    }

    class StudentsExam
    {
        
        private string students_name;
        public string Students_name
        {
            get => students_name;
            set
            {
                if (value.Length != 0) students_name = value;
            }
        }

        private double semester_mark;
        public double Semester_mark
        {
            get => semester_mark;
            set => semester_mark=value;
        }

        private Exam s_exam;

        public Exam S_exam => s_exam;

        private double mark;
        public double Mark
        {
            get => s_exam.TakeOrally(siq_answers, short_answers,semester_mark);
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


        public StudentsExam(string s_name, Exam exam)
        {
            students_name = s_name;
            s_exam = exam;
            siq_answers = new Answer[exam.Single_choise_questions.Length];
            short_answers = new string[exam.Short_answer_questions.Length];
        }

        public override string ToString() => $"\n\nResult\nStudents name:{students_name} \nTest: {s_exam.Name}\nResult: {Mark}\n\n";
    }


    class Program
    {
        static Exam CreateExam(string test_name, string[] q_texts, string[] correct_saq_answer, Answer[] correct)
        {
            Exam exam = new Exam(test_name, correct_saq_answer.Length, correct.Length, 60);
            
            for (int i = 0; i < correct_saq_answer.GetLength(0); i++)
            {
                exam.Short_answer_questions[i] = new ShortAnswerQuestion(q_texts[i], correct_saq_answer[i], 10);
            }
            for (int i = 0; i < correct.Length; i++)
            {
                exam.Single_choise_questions[i] = new SingleChoiseQuestion(q_texts[i + correct_saq_answer.GetLength(0)], correct[i], 5);
            }
            Console.WriteLine("Exam '" + exam.ToString() + "' created successfully");
            return exam;
        }


        static StudentsExam DoExam(Exam exam, string user_name)
        {
            Console.WriteLine("----------------------------Exam begin-------------------------------------");
            Console.WriteLine(exam.ToString());

            StudentsExam s_test = new StudentsExam(user_name, exam);

            Answer[] sicansw = new Answer[exam.Single_choise_questions.Length];//single choise user answer

            for (int i = 0; i < exam.Single_choise_questions.Length; i++)
            {
                Console.WriteLine(exam.Single_choise_questions[i].ToString());
                s_test.Siq_answes[i] = exam.Single_choise_questions[i].Correct_answer;
            }

            string[] saq_answers = new string[exam.Short_answer_questions.Length];

            for (int i = 0; i < exam.Short_answer_questions.Length; i++)
            {
                Console.WriteLine(exam.Short_answer_questions[i].ToString());
                s_test.Short_answers[i] = exam.Short_answer_questions[i].Correct_answer;
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
            Exam students_pain = CreateExam("Programing", tasks1, test1_correct_answ, test1);

            StudentsExam s_test1 = DoExam(students_pain, "Sacrifice");

            string[] tasks2 = { "8/4", "1/2" };
            string[] test2_correct_answ = { "2", "0.5" };
            Answer[] test2 = new Answer[0];

            Exam highwaytohell = CreateExam("Statistics", tasks2, test2_correct_answ, test2);
            StudentsExam s_test2 = DoExam(highwaytohell, "Sacrafice");

            /*Test fatality = highwaytohell + students_pain;

            StudentsExam s_test3 = DoTest(fatality, "Sacrafice");*/


        }
    }
}
