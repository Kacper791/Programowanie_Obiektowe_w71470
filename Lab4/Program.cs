namespace Lab4
{
    // Interfejs IModular
    public interface IModular
    {
        double Module();
    }

    // Klasa reprezentująca liczbę zespoloną
    public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular, IComparable<ComplexNumber>
    {
        public double Re { get; set; }
        public double Im { get; set; }

        public ComplexNumber(double re, double im)
        {
            Re = re;
            Im = im;
        }

        public override string ToString() => $"{Re} {(Im >= 0 ? "+" : "-")} {Math.Abs(Im)}i";

        // Operatory
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.Re + b.Re, a.Im + b.Im);
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.Re - b.Re, a.Im - b.Im);
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.Re * b.Re - a.Im * b.Im, a.Re * b.Im + a.Im * b.Re);
        public static ComplexNumber operator -(ComplexNumber a) => new ComplexNumber(a.Re, -a.Im);

        // Clone i Equals
        public object Clone() => new ComplexNumber(Re, Im);
        public bool Equals(ComplexNumber? other) => other != null && Re == other.Re && Im == other.Im;
        public override bool Equals(object? obj) => obj is ComplexNumber other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Re, Im);
        public static bool operator ==(ComplexNumber a, ComplexNumber b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(ComplexNumber a, ComplexNumber b) => !(a == b);

        // Moduł
        public double Module() => Math.Sqrt(Re * Re + Im * Im);

        // Porównanie po module
        public int CompareTo(ComplexNumber? other)
        {
            if (other == null) return 1;
            return Module().CompareTo(other.Module());
        }
    }

    class Program
    {
        static void Print<T>(IEnumerable<T> col)
        {
            foreach (var item in col)
                Console.WriteLine(item);
            Console.WriteLine();
        }

        static void Main()
        {
            

            // 2. TABLICA
            
            ComplexNumber[] arr =
            {
                new ComplexNumber(4,6),
                new ComplexNumber(2,-5),
                new ComplexNumber(-2,6),
                new ComplexNumber(3,-4),
                new ComplexNumber(0,2)
            };

            Console.WriteLine("2a) Tablica (foreach):");
            Print(arr);

            Console.WriteLine("2b) Sortowanie po module:");
            Array.Sort(arr);
            Print(arr);

            Console.WriteLine("2c) Min:");
            Console.WriteLine(arr.Min());
            Console.WriteLine("Max:");
            Console.WriteLine(arr.Max());
            Console.WriteLine();

            Console.WriteLine("2d) Filtrowanie Im < 0:");
            Print(arr.Where(z => z.Im < 0));

            
            // 3. LISTA
            
            List<ComplexNumber> list = new()
            {
                new ComplexNumber(4,7),
                new ComplexNumber(2,-3),
                new ComplexNumber(7,2),
                new ComplexNumber(-4,-1),
                new ComplexNumber(0,8)
            };

            Console.WriteLine("Lista - sortowanie:");
            list.Sort();
            Print(list);

            Console.WriteLine("Min:");
            Console.WriteLine(list.Min());
            Console.WriteLine("Max:");
            Console.WriteLine(list.Max());
            Console.WriteLine();

            Console.WriteLine("Filtrowanie Im < 0:");
            Print(list.Where(z => z.Im < 0));

            Console.WriteLine("3a) Usuń drugi element:");
            list.RemoveAt(1);
            Print(list);

            Console.WriteLine("3b) Usuń najmniejszy element:");
            list.Remove(list.Min());
            Print(list);

            Console.WriteLine("3c) Usuń wszystkie elementy:");
            list.Clear();
            Print(list);

            
            // 4. HASHSET
            
            HashSet<ComplexNumber> set = new()
            {
                new ComplexNumber(6,7),   // z1
                new ComplexNumber(1,2),   // z2
                new ComplexNumber(6,7),   // z3
                new ComplexNumber(1,-2),  // z4
                new ComplexNumber(-5,9)   // z5
            };

            Console.WriteLine("4a) zawartość:");
            Print(set);

            Console.WriteLine("4b) Min / Max / Sort / Filter:");
            Console.WriteLine("Min: " + set.Min());
            Console.WriteLine("Max: " + set.Max());
            Console.WriteLine();

            Console.WriteLine("Sortowanie po module:");
            Print(set.OrderBy(z => z.Module()));

            Console.WriteLine("Filtrowanie Im < 0:");
            Print(set.Where(z => z.Im < 0));


            // 5. SŁOWNIK

            Dictionary<string, ComplexNumber> dict = new()
            {
                {"z1", new ComplexNumber(6,7)},
                {"z2", new ComplexNumber(1,2)},
                {"z3", new ComplexNumber(6,7)},
                {"z4", new ComplexNumber(1,-2)},
                {"z5", new ComplexNumber(-5,9)},
            };

            Console.WriteLine("5a) Słownik (klucz, wartość):");
            foreach (var p in dict)
                Console.WriteLine($"{p.Key} = {p.Value}");
            Console.WriteLine();

            Console.WriteLine("5b) Klucze:");
            Print(dict.Keys);
            Console.WriteLine("Wartości:");
            Print(dict.Values);

            Console.WriteLine("5c) Czy istnieje klucz z6?");
            Console.WriteLine(dict.ContainsKey("z6") + "\n");

            Console.WriteLine("5d) Min/Max i filtracja:");
            Console.WriteLine("Min: " + dict.Values.Min());
            Console.WriteLine("Max: " + dict.Values.Max());
            Console.WriteLine();

            Console.WriteLine("Filtrowanie Im < 0:");
            foreach (var v in dict.Values.Where(v => v.Im < 0))
                Console.WriteLine(v);
            Console.WriteLine();

            Console.WriteLine("5e) Usuń z3:");
            dict.Remove("z3");

            Console.WriteLine("5f) Usuń drugi element:");
            dict.Remove(dict.ElementAt(1).Key);

            Console.WriteLine("Aktualny słownik:");
            Print(dict);

            Console.WriteLine("5g) Wyczyść słownik:");
            dict.Clear();
            Print(dict);

        }
    }
}
