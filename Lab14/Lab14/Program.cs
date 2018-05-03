using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Tuple;


namespace Lab14
{

    struct RestrictedCharTuple
    {
        char first_char;
        char second_char;
        char third_char;
        char fourth_char;
        char fifth_char;

        

        public RestrictedCharTuple(char char1 = '#', char char2 = '#', char char3 = '#', char char4 = '#', char char5 = '#')
        {
            first_char = char1;
            second_char = char2;
            third_char = char3;
            fourth_char = char4;
            fifth_char = char5;
        }

        public static char[] Allowed_chars= { 'a','b','c','d','e','1','2','3' };

        static RestrictedCharTuple Create(char first_char='#', char second_char = '#', char third_char = '#', char fourth_char = '#', char fifth_char = '#')
        {
            RestrictedCharTuple r = new RestrictedCharTuple(first_char, second_char, third_char, fourth_char, fifth_char);
            return r;
        }

        static RestrictedCharTuple Create(char[] field,string command="")
        {
            if (field.Length > 5) throw new Exception("Count of fields must be betwen 0 and 5");

            if (command == "REVERSE")
                Array.Reverse(field);
            if (command == "SORT")
                Array.Sort(field);

            for (int i = 0; i < field.Length; i++)
                if (!Allowed_chars.Contains(field[i])) throw new Exception("Restricted char");

            switch (field.Length)
            {
                case 1:
                    return new RestrictedCharTuple(field[0]);
                case 2:
                    return new RestrictedCharTuple(field[0],field[1]);
                case 3:
                    return new RestrictedCharTuple(field[0], field[1],field[2]);
                case 4:
                    return new RestrictedCharTuple(field[0], field[1], field[2],field[3]);
                case 5:
                    return new RestrictedCharTuple(field[0], field[1], field[2], field[3],field[4]);
                default:
                    {
                        throw new Exception("Restricted count of fields(empty array field or length of fields bigger than 5)");
                    }
            }

        }

        static RestrictedCharTuple Create(RestrictedCharTuple structure)
        {
            return structure;
        }


        public override string ToString() => $"Tuple: ({first_char}, {second_char}, {third_char}, {fourth_char}, {first_char})";


        private static int CompareTwoChars(char char1, char char2)
        {
            if (char1 > char2) return 1;
            if (char1 < char2) return -1;
            return 0;
        }

        static int CompareByFirst(RestrictedCharTuple struct1, RestrictedCharTuple struct2) => CompareTwoChars(struct1.first_char, struct2.first_char);

        static int CompareBySecond(RestrictedCharTuple struct1, RestrictedCharTuple struct2) => CompareTwoChars(struct1.second_char, struct2.second_char);

        static int CompareByThird(RestrictedCharTuple struct1, RestrictedCharTuple struct2) => CompareTwoChars(struct1.third_char, struct2.third_char);

        static int CompareByFourth(RestrictedCharTuple struct1, RestrictedCharTuple struct2) => CompareTwoChars(struct1.fourth_char, struct2.fourth_char);

        static int CompareByFifth(RestrictedCharTuple struct1, RestrictedCharTuple struct2) => CompareTwoChars(struct1.fifth_char, struct2.fifth_char);

        static int CompareByTh(RestrictedCharTuple struct1, RestrictedCharTuple struct2)
        {
            if (struct1.second_char > struct2.second_char) return 1;
            if (struct1.second_char < struct2.second_char) return -1;
            return 0;
        }

    }


class Program
    {

       

        static void Main(string[] args)
        {
        }
    }
}
