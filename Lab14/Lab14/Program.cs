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


        public override string ToString() => $"({first_char}, {second_char}, {third_char}, {fourth_char}, {fifth_char})";

        public string FieldsToString() => $"{first_char}{second_char}{third_char}{fourth_char}{fifth_char}";

        private static int CompareTwoChars(char char1, char char2)
        {
            if (char1 > char2) return 1;
            if (char1 < char2) return -1;
            return 0;
        }

        public static int CompareByFirst(RestrictedCharTuple struct1, RestrictedCharTuple struct2) => CompareTwoChars(struct1.first_char, struct2.first_char);

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

    class RestrictedCharTupleCollection
    {
        private RestrictedCharTuple[] tuples;

        public int Length;

        public RestrictedCharTupleCollection(int n)
        {
            if (n < 0) throw new Exception("count of elements can't be below 0");
            tuples = new RestrictedCharTuple[n];

        }

        public RestrictedCharTuple this[int index]
        {
            get
            {
                if (index > 0 && index < tuples.Length) return tuples[index];
                else throw new Exception("Index out of range");
            }
            set
            {
                if (index > 0 && index < tuples.Length) tuples[index] = value;
                else throw new Exception("Index out of range");
            }
        }

        public void AddElemnt(RestrictedCharTuple tuple)
        {
            int i = 0;

            while (tuples[i].FieldsToString() != "#####" && i < tuples.Length) i++;

            if (i != tuples.Length - 1) tuples[i] = tuple;

        }

        public void RemoveElement(int index)
        {
            if (index > tuples.Length || index < 0) throw new Exception("index out of range");

            for (int i = index; i < tuples.Length - 1; i++)
                tuples[i] = tuples[i + 1];

        }

        public void PrintToConsole()
        {
            for (int i = 0; i < tuples.Length; i++)
                Console.WriteLine($"Tuple {i}: {tuples[i].ToString()}");
        }

        public delegate int CompareMethod(RestrictedCharTuple rct1, RestrictedCharTuple rct2);

        private static CompareMethod compareMethod=RestrictedCharTuple.CompareByFirst;

        public static CompareMethod Compare_Method { get => compareMethod; set => compareMethod = value; }


        private static void Swap<T>(ref T a, ref T b)
        {
            T temp;
            temp = a;
            a = b;
            b = temp;
        }

        private static int Partition(RestrictedCharTuple[] a, int p, int q)
        {
            RestrictedCharTuple x = a[p];//опорний елемент
            int i = p;
            int j = q;
            while (i < j)
            {
                while (compareMethod(a[i],x)<0 /*a[i] < x*/) i++;
                while (compareMethod(a[j],x)>0/*a[j] > x*/) j--;
                if (i < j)
                {
                    //Console.WriteLine($"{i}<->{j}");
                    Swap<RestrictedCharTuple>(ref a[i], ref a[j]);
                    i++; j--;
                }
            }
            return j;
        }

        /// <summary>
        /// Quick sorting of array
        /// </summary>
        /// <param name="a">array</param>
        /// <param name="p">begin of array</param>
        /// <param name="q">end of array</param>
        private static void QuickSort(RestrictedCharTuple[] a, int p, int q)
        {
            if (p < q)
            {
                int r = Partition(a, p, q);
                QuickSort(a, p, r);
                QuickSort(a, r + 1, q);
            }
        }

    }

class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
