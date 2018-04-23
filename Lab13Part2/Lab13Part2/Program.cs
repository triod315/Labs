using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13Part2
{

    abstract class Animal
    {
        protected string name;
        public string Name { get => name; set => name = value; }

        private DateTime date_of_birth;
        public DateTime Age => new DateTime(date_of_birth.Year + DateTime.Now.Year, date_of_birth.Month + DateTime.Now.Month, date_of_birth.Day + DateTime.Now.Day);

        protected double weight;
        public double Weight => weight;

        public abstract string Speak();

        public Animal() : this(DateTime.Now,"Babaika") { }

        public Animal(DateTime birth_date, string animal_name)
        {
            Name = animal_name;
            date_of_birth = birth_date;
            weight =0;
        }

        public void Eat(double Food_calories) => weight += Food_calories;
    }

    abstract class Mammal : Animal
    {
        private int intelligense_level;

        public int Intelligense_level { get => intelligense_level; set => intelligense_level = value; }

        public Mammal() : this(0, DateTime.Now, "Mammal") { }

        public Mammal(int level, DateTime birth_date, string mammal_name) : base(birth_date, mammal_name) => Intelligense_level = level;

        public override string Speak() => "AAAAAAAAAAAAAAAAAAAAA";    
    }

    class Artiodactyla:Mammal
    {
        public new void Eat(double Food_calories) => weight += 0.5 * Weight;
        public new string Speak() => "IaIaIaIaIaIaIaIaIaIaIa";

        public Artiodactyla():base(0,DateTime.Now,"human") { }
        public Artiodactyla(int level, DateTime birth_date, string mammal_name):base(level,birth_date,mammal_name) { }
    }

    class Aves : Animal
    {
        private double wingspan;

        public double Wingspan { get => wingspan; set => wingspan = value; }

        public Aves() : base(DateTime.Now, "bird") { }

        public Aves(double aves_wingspan, DateTime birth_date, string aves_name) : base(birth_date, aves_name) { }

        public override string Speak() => "kukariku";

    }



    class Program
    {
        static void Main(string[] args)
        {
            Animal[] animals = new Animal[2];
            animals[0] = new Artiodactyla();
            animals[1] = new Aves();

            for (int i = 0; i < animals.Length; i++)
                Console.WriteLine("Animal name: " + animals[i].Name+" - " +animals[i].Speak());
            Artiodactyla h = new Artiodactyla();
            Console.WriteLine(h.Speak());
        }
    }
}
