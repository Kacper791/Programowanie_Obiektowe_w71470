using System;

namespace ComplexNumbers
{
    //2. Interfejs IModular
    public interface IModular
    {
        double Module();
    }

    //1. Klasa ComplexNumber
    public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular
    {
        // Prywatne pola
        private double re; // część rzeczywista
        private double im; // część urojona

        // Publiczne właściwości
        public double Re
        {
            get { return re; }
            set { re = value; }
        }

        public double Im
        {
            get { return im; }
            set { im = value; }
        }

        // Konstruktor z parametrami
        public ComplexNumber(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        // Przeciążenie ToString() – poprawny zapis liczby zespolonej
        public override string ToString()
        {
            string sign = im >= 0 ? "+" : "-";
            return $"{re} {sign} {Math.Abs(im)}i";
        }

        // Operatory binarne +, -, *

        // Dodawanie liczb zespolonych
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.re + b.re, a.im + b.im);
        }

        // Odejmowanie liczb zespolonych
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.re - b.re, a.im - b.im);
        }

        // Mnożenie liczb zespolonych
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            double real = a.re * b.re - a.im * b.im;
            double imag = a.re * b.im + a.im * b.re;
            return new ComplexNumber(real, imag);
        }

        // Operator unarny - (sprzężenie zespolone)
        public static ComplexNumber operator -(ComplexNumber a)
        {
            return new ComplexNumber(a.re, -a.im);
        }

        // Implementacja interfejsu ICloneable
        public object Clone()
        {
            return new ComplexNumber(this.re, this.im);
        }

        // Implementacja interfejsu IEquatable
        public bool Equals(ComplexNumber? other)
        {
            if (other == null)
                return false;
            return this.re == other.re && this.im == other.im;
        }

        // Przeciążenie metody Equals(object)
        public override bool Equals(object? obj)
        {
            if (obj is ComplexNumber other)
                return Equals(other);
            return false;
        }

        // GetHashCode — potrzebny do poprawnego działania Equals
        public override int GetHashCode()
        {
            return HashCode.Combine(re, im);
        }

        // Operator ==
        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a is null || b is null)
                return false;
            return a.Equals(b);
        }

        // Operator !=
        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return !(a == b);
        }

        // Implementacja interfejsu IModular
        public double Module()
        {
            return Math.Sqrt(re * re + im * im);
        }
    }

    // 4. Klasa Program z metodą Main()
    class Program
    {
        public static void Main(string[] args)
        {
            // Tworzenie przykładowych liczb zespolonych
            ComplexNumber z1 = new ComplexNumber(3, 4);
            ComplexNumber z2 = new ComplexNumber(1, -2);

            Console.WriteLine($"z1 = {z1}");
            Console.WriteLine($"z2 = {z2}");
            Console.WriteLine();

            // Testowanie przeciążonych operatorów
            Console.WriteLine($"z1 + z2 = {z1 + z2}");
            Console.WriteLine($"z1 - z2 = {z1 - z2}");
            Console.WriteLine($"z1 * z2 = {z1 * z2}");
            Console.WriteLine($"Sprzężenie z1: {-z1}");
            Console.WriteLine();

            // Test interfejsu IModular
            Console.WriteLine($"|z1| = {z1.Module():F2}");
            Console.WriteLine($"|z2| = {z2.Module():F2}");
            Console.WriteLine();

            // Test porównania i klonowania
            ComplexNumber z3 = (ComplexNumber)z1.Clone();
            Console.WriteLine($"Kopia z1 → z3: {z3}");
            Console.WriteLine($"z1 == z3 ? {z1 == z3}");
            Console.WriteLine($"z1 != z2 ? {z1 != z2}");
        }
    }
}
