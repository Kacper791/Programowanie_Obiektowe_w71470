using System;

namespace Lab2
{
    // 1. Klasa Zwierze
    class Zwierze
    {
        protected string nazwa;

        public Zwierze(string nazwa)
        {
            this.nazwa = nazwa;
        }

        public virtual void DajGlos()
        {
            Console.WriteLine("...");
        }
    }

    // 2. Klasa Pies
    class Pies : Zwierze
    {
        public Pies(string nazwa) : base(nazwa) { }

        public override void DajGlos()
        {
            Console.WriteLine($"{nazwa} robi woof woof!");
        }
    }

    // 3. Klasa Kot
    class Kot : Zwierze
    {
        public Kot(string nazwa) : base(nazwa) { }

        public override void DajGlos()
        {
            Console.WriteLine($"{nazwa} robi miau miau!");
        }
    }

    // 4. Klasa Waz
    class Waz : Zwierze
    {
        public Waz(string nazwa) : base(nazwa) { }

        public override void DajGlos()
        {
            Console.WriteLine($"{nazwa} robi ssssssss!");
        }
    }

    // 6. Globalna metoda powiedz_cos
    static class ZwierzeHelper
    {
        public static void PowiedzCos(Zwierze z)
        {
            z.DajGlos();
            Console.WriteLine("Typ obiektu: " + z.GetType().Name);
        }
    }

    // 8. Abstrakcyjna klasa Pracownik
    abstract class Pracownik
    {
        public abstract void Pracuj();
    }

    // 9. Klasa Piekarz
    class Piekarz : Pracownik
    {
        public override void Pracuj()
        {
            Console.WriteLine("Trwa pieczenie...");
        }
    }

    // 12. Klasa A
    class A
    {
        public A()
        {
            Console.WriteLine("To jest konstruktor A");
        }
    }

    // 13. Klasa B
    class B : A
    {
        public B()
        {
            Console.WriteLine("To jest konstruktor B");
        }
    }

    // 14. Klasa C
    class C : B
    {
        public C()
        {
            Console.WriteLine("To jest konstruktor C");
        }
    }

    // 7, 10, 11, 15
    class Program
    {
        static void Main(string[] args)
        {
            // Punkt 7
            Console.WriteLine("--- Zwierzęta ---");
            Zwierze z = new Zwierze("Zwierzak");
            Pies p = new Pies("Pimpek");
            Kot k = new Kot("Ryszard");
            Waz w = new Waz("Waldemar");

            ZwierzeHelper.PowiedzCos(z);
            ZwierzeHelper.PowiedzCos(p);
            ZwierzeHelper.PowiedzCos(k);
            ZwierzeHelper.PowiedzCos(w);

            // Punkt 10
            Console.WriteLine("\n--- Piekarz ---");
            Piekarz piekarz = new Piekarz();
            piekarz.Pracuj();

            // Punkt 11
            Console.WriteLine("\n--- Próba utworzenia obiektu klasy abstrakcyjnej ---");
            // Pracownik pracownik = new Pracownik(); Błąd kompilacji – klasa abstrakcyjna

            Console.WriteLine("Nie można utworzyć instancji klasy abstrakcyjnej Pracownik.");

            // Punkt 15
            Console.WriteLine("\n--- Konstruktory ---");
            A a = new A();
            B b = new B();
            C c = new C();
        }
    }
}
