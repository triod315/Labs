using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Tuple;


namespace Lab14
{

    delegate int CompareMethod(RestrictedCharTuple rct1, RestrictedCharTuple rct2);

    struct RestrictedCharTuple
    {

        private int StringNumberToInt(string number)
        {
            switch (number)
            {
                case "first":return 0;
                case "second": return 1;
                case "third": return 2;
                case "fourth": return 3;
                case "fifth": return 4;
                default: throw new Exception("Invalid number in string");
            }
        }

        private char[] fields;

        public char this[string index]
        {
            get => fields[StringNumberToInt(index)];
            set => fields[StringNumberToInt(index)] = value;
        }
        
        public RestrictedCharTuple(char char1 = '#', char char2 = '#', char char3 = '#', char char4 = '#', char char5 = '#')
        {
            fields = new char[] { char1,char2,char3,char4,char5 };
        }

        public static char[] Allowed_chars = { 'a', 'b', 'c', 'd', 'e', '1', '2', '3' };

        public static RestrictedCharTuple Create(char first_char = '#', char second_char = '#', char third_char = '#', char fourth_char = '#', char fifth_char = '#')
        {
            RestrictedCharTuple r = new RestrictedCharTuple(first_char, second_char, third_char, fourth_char, fifth_char);
            return r;
        }

        public static RestrictedCharTuple Create(char[] field, string command = "")
        {
            if (field.Length > 5) throw new Exception("Count of fields must be betwen 0 and 5");

            if (command == "REVERSE")
                Array.Reverse(field);
            if (command == "SORT")
                Array.Sort(field);

            RestrictedCharTuple result = new RestrictedCharTuple();
            result.fields = new char[5];


            for (int i = 0; i < field.Length; i++)
            {
                //if (!Allowed_chars.Contains(field[i])) throw new Exception("Restricted char");
                result.fields[i] = field[i];
            }

            for (int i = field.Length; i < result.fields.Length; i++)
                result.fields[i] = '#';

            return result;
            

        }

        public static RestrictedCharTuple Create(RestrictedCharTuple structure) => Create(structure.fields);


        private static int CompareTwoChars(char char1, char char2)
        {
            if (char1 > char2) return 1;
            if (char1 < char2) return -1;
            return 0;
        }

        public static CompareMethod CompareByFirst=(RestrictedCharTuple struct1, RestrictedCharTuple struct2) => CompareTwoChars(struct1.fields[0], struct2.fields[0]);

        public static CompareMethod CompareBySecond=(RestrictedCharTuple struct1, RestrictedCharTuple struct2) => CompareTwoChars(struct1.fields[1], struct2.fields[1]);

        public static CompareMethod CompareByThird=(RestrictedCharTuple struct1, RestrictedCharTuple struct2) => CompareTwoChars(struct1.fields[2], struct2.fields[2]);

        public static CompareMethod CompareByFourth=(RestrictedCharTuple struct1, RestrictedCharTuple struct2) => CompareTwoChars(struct1.fields[3], struct2.fields[3]);

        public static CompareMethod CompareByFifth=(RestrictedCharTuple struct1, RestrictedCharTuple struct2) => CompareTwoChars(struct1.fields[4], struct2.fields[4]);

        public void ChangeCode(int x)
        {
            for (int i = 0; i < fields.Length; i++) fields[i] += (char)x;
        }

        public string InsertString(string str)
        {
            string s="";
            for (int i = 0; i < fields.Length-1; i++) s += fields[i] + str;
            return s + fields[fields.Length - 1];
        }

        public override string ToString()
        {
            string s = "(";

            for (int i = 0; i < fields.Length - 1; i++) s += fields[i] + ",";

            return s + fields[fields.Length - 1] + ")";
        }

        public string FieldsToString() => new string(fields);

    }

    class RestrictedCharTupleCollection
    {
        private RestrictedCharTuple[] tuples;

        private int length;
        public int Length => length;

        public RestrictedCharTupleCollection(int n)
        {
            if (n < 0) throw new Exception("count of elements can't be below 0");
            tuples = new RestrictedCharTuple[n];

            for (int i = 0; i < n; i++)
                tuples[i] = RestrictedCharTuple.Create();
            length = 0;
        }

        public ref RestrictedCharTuple this[int index]
        {
            get
            {
                if (index > 0 && index < tuples.Length)  return ref tuples[index];
                else throw new Exception("Index out of range");
            }
        }

        public void AddElemnt(RestrictedCharTuple tuple)
        {
            int i = 0;
            
            while (tuples[i].FieldsToString() != "#####" && i < tuples.Length-1) i++;

            if (i != tuples.Length) tuples[i] = tuple;
            length++;
        }

        public void RemoveElement(int index)
        {
            if (index > tuples.Length || index < 0) throw new Exception("index out of range");

            for (int i = index; i < tuples.Length - 1; i++)
                tuples[i] = tuples[i + 1];

            length--;

        }

        public void PrintToConsole()
        {
            for (int i = 0; i < length; i++)
                Console.WriteLine($"Tuple {i}: {tuples[i].ToString()}");
        }


        private static CompareMethod compareMethod = RestrictedCharTuple.CompareByFirst;

        public static CompareMethod Compare_Method { get => compareMethod; set => compareMethod = value; }
       

        private static void Swap<T>(ref T a, ref T b)
        {
            T temp;
            temp = a;
            a = b;
            b = temp;
        }

        private int Partition(int p, int q)
        {
            RestrictedCharTuple x = tuples[p];//опорний елемент
            int i = p;
            int j = q;
            while (i < j)
            {
                while (compareMethod(tuples[i],x)<0 /*a[i] < x*/) i++;
                while (compareMethod(tuples[j],x)>0/*a[j] > x*/) j--;
                if (i < j)
                {
                    //Console.WriteLine($"{i}<->{j}");
                    Swap(ref tuples[i], ref tuples[j]);
                    i++; j--;
                }
            }
            return j;
        }

        /// <summary>
        /// Quick sorting of array
        /// </summary>
        /// <param name="tuples">array</param>
        /// <param name="p">begin of array</param>
        /// <param name="q">end of array</param>
        public void QuickSort(int p, int q)
        {
            if (p < q)
            {
                int r = Partition(p, q);
                QuickSort(p, r);
                QuickSort(r + 1, q);
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            char[] tup1 = { 'e', 'd', 'c', 'b', 'a' };
            RestrictedCharTuple tuple1 = RestrictedCharTuple.Create(tup1);
            tuple1.ChangeCode(1);
            Console.WriteLine(tuple1.ToString());
            char[] tup2 = { 'a', 'b', 'c', 'd', 'e' };
            RestrictedCharTuple tuple2 = RestrictedCharTuple.Create(tup2);
            char[] tup3 = { 'k', 'l', 'm', 'a', 'o' };
            RestrictedCharTuple tuple3 = RestrictedCharTuple.Create(tup3);

            RestrictedCharTupleCollection tupleCollection = new RestrictedCharTupleCollection(3);
            tupleCollection.AddElemnt(tuple1);
            tupleCollection.AddElemnt(tuple2);
            tupleCollection.AddElemnt(tuple3);

            Console.WriteLine("tuple with str " + tuple1.InsertString("_"));

            Console.WriteLine("--------------");
            tupleCollection.PrintToConsole();

            Console.WriteLine("--------------");

            RestrictedCharTupleCollection.Compare_Method = RestrictedCharTuple.CompareByFourth;
            tupleCollection.QuickSort(0, tupleCollection.Length - 1);

            tupleCollection.PrintToConsole();

            Console.WriteLine("--------------");

            tupleCollection.RemoveElement(1);
            tupleCollection.PrintToConsole();


        }
    }
}
