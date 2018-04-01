using System;
using System.Collections.Generic;

namespace Lab12
{
    enum Answer {a,b,c,d};
    class Question
    {
        /// <summary>
        /// The text of quesion.
        /// </summary>
        private string text;
        public string Text
        {
            get
            {
                return text;
            }
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
            get
            {
                return mark;
            }   
            set 
            {
                if (value > 0)
                    mark = value;
                else
                    throw new Exception("Mark can`t be below 0");
            }
        }


        /// <summary>
        /// default constructor
        /// Initializes a new instance of the <see cref="Lab12.Question"/> class.
        /// </summary>
        public Question()
        {
            text="Question";
            mark = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lab12.Question"/> class.
        /// </summary>
        /// <param name="q_text">Question text</param>
        /// <param name="q_mark">Q mark.</param>
        /// <param name="q_type">Q type.</param>
        public Question(string q_text,double q_mark)
        {
            text = q_text;
            mark = q_mark;
        }
    }

    class SingleChoiseQuestion:Question
    {
        private Answer correct_answer;
        public Answer Correct_answer
        {
            get
            {
                return correct_answer;
            }
            set
            {
                correct_answer = value;
            }
        }

        /// <summary>
        /// Checks the answer.
        /// </summary>
        /// <returns>mark</returns>
        /// <param name="answer">Answer.</param>
        /// <param name="question">Question.</param>
        public static double CheckAnswer(Answer answer, SingleChoiseQuestion question)
        {
            if (answer == question.Correct_answer)
                return question.Mark;
            else
                return 0;
        }

        /// <summary>
        /// Default consturctor
        /// </summary>
        public SingleChoiseQuestion():this("Question 1",Answer.a,0){ }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lab12.SingleChoiseQuestion"/> class.
        /// </summary>
        /// <param name="correct_answ">Correct answer.</param>
        public SingleChoiseQuestion(string q_text,Answer correct_answ, double q_mark)
        {
            Text = q_text;
            Mark = q_mark;
            Correct_answer = correct_answ;
        }

    }

    class ShortAnswerQuestion:Question
    {
        private string correct_answer;
        public string Correct_answer
        {
            get 
            {
                return correct_answer;
            }
            set
            {
                if (value.Length >= 0)
                    correct_answer = value;
                else
                {
                    Exception Incorrect_short_answer_exception = new Exception("Incorrect answer");
                    throw Incorrect_short_answer_exception;
                }
            }
        }

        /// <summary>
        /// Checks the answer.
        /// </summary>
        /// <returns>mark</returns>
        /// <param name="answer">Answer.</param>
        /// <param name="question">Question.</param>
        public static double CheckAnswer(String answer, ShortAnswerQuestion question)
        {
            if (answer == question.Correct_answer)
                return question.Mark;
            else
                return 0;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShortAnswerQuestion():this("Question","empty filed",0){}

        /// <summary>
        /// Initializes a new instance of the <see cref="Lab12.ShortAnswerQuestion"/> class.
        /// </summary>
        /// <param name="q_text">text of questin.</param>
        /// <param name="correct_answ">Correct answer.</param>
        /// <param name="q_mark">Q mark.</param>
        public ShortAnswerQuestion(string q_text,string correct_answ, double q_mark)
        {
            Text = q_text;
            Correct_answer = correct_answ;
            Mark = q_mark;
        }


    }

    class Test
    {
        private List<SingleChoiseQuestion> single_choise_questions;

        public List<SingleChoiseQuestion> Single_choise_questions
        {
            get 
            {
                return single_choise_questions;
            }
        }

        private List<ShortAnswerQuestion> short_answer_questions;

        public List<ShortAnswerQuestion> Short_answer_questions
        {
            get
            {
                return short_answer_questions;
            }
        }

        /// <summary>
        /// Adds the single choise question.
        /// </summary>
        /// <param name="question">Question.</param>
        public void Add_Question(SingleChoiseQuestion question)
        {
            if (question != null)
            {
                single_choise_questions.Add(question);
                max_mark += question.Mark;
            }
            else
            {
                Exception Wrong_scq_exc = new Exception("Wrong single choise format");
                throw Wrong_scq_exc;
            }
        }

        /// <summary>
        /// Adds the short answer question.
        /// </summary>
        /// <param name="question">Question.</param>
        public void Add_Question(ShortAnswerQuestion question)
        {
            if (question != null)
            {
                short_answer_questions.Add(question);
                max_mark += question.Mark;
            }
            else
            {
                Exception Wrong_saq_exc = new Exception("Wrong short answer format");
                throw Wrong_saq_exc;
            }
                
        }

        /// <summary>
        /// Checks the test.
        /// </summary>
        /// <returns>real mark</returns>
        /// <param name="test">Test.</param>
        /// <param name="short_answers">Short answers.</param>
        /// <param name="scq_answers">Single answer question answers.</param>
        public static double CheckTest(Test test,string[] short_answers, Answer[] scq_answers)
        {
            double mark = 0;

            for (int i=0;i<short_answers.GetLength(0);i++)
                mark+=ShortAnswerQuestion.CheckAnswer(short_answers[i],test.short_answer_questions[i]);
                    
            for (int i = 0; i < scq_answers.Length; i++)
            {
                mark += SingleChoiseQuestion.CheckAnswer(scq_answers[i], test.single_choise_questions[i]);
            }
            return mark;
        }

        public static double CheckTest(Test test,string[] short_answers)
        {
            double mark = 0;

            for (int i=0;i<short_answers.GetLength(0);i++)
                mark+=ShortAnswerQuestion.CheckAnswer(short_answers[i],test.short_answer_questions[i]);
            return mark;
        }

        private string name;
        public string Name
        {
            get 
            {
                return name;
            }
            set 
            {
                if (value.Length != 0)
                    name = value;
                else
                    name = "Empty_name";
            }
        }

        private double max_mark;
        public double Max_Mark
        {
            get 
            {
                max_mark = 0;
                for (int i = 0; i < single_choise_questions.Count; i++)
                    max_mark += single_choise_questions[i].Mark;
                for (int i = 0; i < short_answer_questions.Count; i++)
                    max_mark += short_answer_questions[i].Mark;
                return max_mark;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>

        public Test():this("Unnamed test"){}

        public Test(string test_name)
        {
            name=test_name;
            max_mark = 0;
            single_choise_questions = new List<SingleChoiseQuestion>();
            short_answer_questions = new List<ShortAnswerQuestion>();
        }

        public static Test operator +(Test test1, Test test2)
        {
            Test test = new Test();
            test.name = test1.name +" "+ test2.name;

            test.single_choise_questions = test1.single_choise_questions;
            for (int i = 0; i < test2.single_choise_questions.Count; i++)
            {
                test.single_choise_questions.Add(test2.single_choise_questions[i]);
            }

            test.short_answer_questions = test1.short_answer_questions;
            for (int i = 0; i < test2.short_answer_questions.Count; i++)
            {
                test.short_answer_questions.Add(test2.short_answer_questions[i]);
            }

            test.max_mark = test1.max_mark + test2.max_mark;
            return test;
        }

        public override string ToString() => $"{this.name}, max mark:{this.Max_Mark}";

    }



    class MainClass
    {

       

        public static void Main(string[] args)
        {
            Test students_pain = new Test("Programing exam");
            students_pain.Add_Question(new ShortAnswerQuestion("Are you ready for exam?", "Yes", 5));
            students_pain.Add_Question(new ShortAnswerQuestion("Pain 2", "o no o no o no no no", 10));
            Console.WriteLine(students_pain.Short_answer_questions[0].Text+" "+students_pain.Short_answer_questions[0].Correct_answer);

            string[] answer = { "babaika", "o no o no o no no "};
            double mark = Test.CheckTest(students_pain, answer);
            if (mark != students_pain.Max_Mark)
                Console.WriteLine("You are in the army now");
            

            Test highwaytohell = new Test("Statistics exam");
            highwaytohell.Add_Question(new ShortAnswerQuestion("Are you alive?","Yes",5));

            Test fatality = highwaytohell + students_pain;
            //Console.WriteLine(fatality.Max_Mark);
            Console.WriteLine(fatality.ToString());

        }
    }
}
