using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;

public class Program
{
    public static void Main()
    {
        // Tutaj można wywoływać poszczególne zadania:
        // Task2_SaveUserInput();
        // Task3_ReadUserInput();
        // Task4_AppendUserInput();
        // Task6_JsonSerializeStudents();
        // Task7_JsonDeserializeStudents();
        // Task8_XmlSerializeStudents();
        // Task9_XmlDeserializeStudents();
        // Task10_ReadIrisCsv();
        // Task11_IrisColumnAverages();
        // Task12_FilterIrisCsv();
    }

    // 
    // ZADANIE 2 — Wczytaj od użytkownika kilka linii i zapisz do pliku
    // 
    static void Task2_SaveUserInput()
    {
        List<string> lines = new();

        Console.Write("Podaj liczbę linii do wpisania: ");
        int count = int.Parse(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Console.Write($"Linia {i + 1}: ");
            lines.Add(Console.ReadLine());
        }

        File.WriteAllLines("user_input.txt", lines);
        Console.WriteLine("Zapisano dane do pliku user_input.txt");
    }

    // 
    // ZADANIE 3 — Odczytaj i wypisz dane z pliku
    // 
    static void Task3_ReadUserInput()
    {
        if (!File.Exists("user_input.txt"))
        {
            Console.WriteLine("Plik user_input.txt nie istnieje.");
            return;
        }

        foreach (var line in File.ReadAllLines("user_input.txt"))
            Console.WriteLine(line);
    }

    // 
    // ZADANIE 4 — Dopisywanie nowych danych do tego samego pliku
    // 
    static void Task4_AppendUserInput()
    {
        Console.WriteLine("Wprowadzaj tekst (pusta linia kończy dopisywanie):");

        using StreamWriter sw = new("user_input.txt", append: true);

        while (true)
        {
            string text = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(text))
                break;

            sw.WriteLine(text);
        }

        Console.WriteLine("Dane dopisane do pliku user_input.txt.");
    }

    // 
    // ZADANIE 5 — Klasa Student
    // 
    public class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public List<int> Oceny { get; set; }
    }

    // 
    // ZADANIE 6 — Serializacja JSON
    // 
    static void Task6_JsonSerializeStudents()
    {
        List<Student> students = new()
        {
            new Student { Imie = "Jan", Nazwisko = "Kowalski", Oceny = new() {5,4,3} },
            new Student { Imie = "Anna", Nazwisko = "Nowak", Oceny = new() {5,5,5} },
            new Student { Imie = "Piotr", Nazwisko = "Wiśniewski", Oceny = new() {3,4,4} }
        };

        string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("students.json", json);

        Console.WriteLine("Zapisano dane studentów do JSON (students.json).");
    }

    // 
    // ZADANIE 7 — Deserializacja JSON
    // 
    static void Task7_JsonDeserializeStudents()
    {
        if (!File.Exists("students.json"))
        {
            Console.WriteLine("Brak pliku students.json.");
            return;
        }

        string json = File.ReadAllText("students.json");
        List<Student> list = JsonSerializer.Deserialize<List<Student>>(json);

        foreach (var s in list)
            Console.WriteLine($"{s.Imie} {s.Nazwisko} — Oceny: {string.Join(", ", s.Oceny)}");
    }

    // 
    // ZADANIE 8 — Serializacja XML
    // 
    static void Task8_XmlSerializeStudents()
    {
        List<Student> students = new()
        {
            new Student { Imie = "Jan", Nazwisko = "Kowalski", Oceny = new() {5,4,3} },
            new Student { Imie = "Anna", Nazwisko = "Nowak", Oceny = new() {5,5,5} }
        };

        XmlSerializer xml = new(typeof(List<Student>));

        using FileStream fs = new("students.xml", FileMode.Create);
        xml.Serialize(fs, students);

        Console.WriteLine("Zapisano dane studentów do XML (students.xml).");
    }

    // 
    // ZADANIE 9 — Deserializacja XML
    // 
    static void Task9_XmlDeserializeStudents()
    {
        if (!File.Exists("students.xml"))
        {
            Console.WriteLine("Brak pliku students.xml.");
            return;
        }

        XmlSerializer xml = new(typeof(List<Student>));

        using FileStream fs = new("students.xml", FileMode.Open);
        List<Student> students = (List<Student>)xml.Deserialize(fs);

        foreach (var s in students)
            Console.WriteLine($"{s.Imie} {s.Nazwisko} — Oceny: {string.Join(", ", s.Oceny)}");
    }

    // 
    // ZADANIE 10 — Odczyt pliku CSV Iris
    // 
    static void Task10_ReadIrisCsv()
    {
        if (!File.Exists("iris.csv"))
        {
            Console.WriteLine("Brak pliku iris.csv");
            return;
        }

        foreach (var line in File.ReadAllLines("iris.csv"))
            Console.WriteLine(line);
    }

    // 
    // ZADANIE 11 — Średnie kolumn numerycznych
    // 
    static void Task11_IrisColumnAverages()
    {
        if (!File.Exists("iris.csv"))
        {
            Console.WriteLine("Brak pliku iris.csv");
            return;
        }

        var lines = File.ReadAllLines("iris.csv");
        string[] header = lines[0].Split(',');

        var data = lines.Skip(1).Select(x => x.Split(',')).ToList();

        string[] numericColumns = { "sepal_length", "sepal_width", "petal_length", "petal_width" };

        Console.WriteLine("Średnie wartości kolumn numerycznych:");

        foreach (var col in numericColumns)
        {
            int index = Array.IndexOf(header, col);
            double avg = data.Average(r => double.Parse(r[index]));
            Console.WriteLine($"{col}: {avg}");
        }
    }

    // 
    // ZADANIE 12 — Filtrowanie CSV do iris_filtered.csv
    // 
    static void Task12_FilterIrisCsv()
    {
        if (!File.Exists("iris.csv"))
        {
            Console.WriteLine("Brak pliku iris.csv");
            return;
        }

        var lines = File.ReadAllLines("iris.csv");
        var header = lines[0].Split(',');

        int idxLength = Array.IndexOf(header, "sepal_length");
        int idxWidth = Array.IndexOf(header, "sepal_width");
        int idxClass = Array.IndexOf(header, "class");

        var filtered = lines
            .Skip(1)
            .Select(x => x.Split(','))
            .Where(row => double.Parse(row[idxLength]) < 5)
            .Select(row => $"{row[idxLength]},{row[idxWidth]},{row[idxClass]}");

        List<string> output = new()
        {
            "sepal_length,sepal_width,class"
        };
        output.AddRange(filtered);

        File.WriteAllLines("iris_filtered.csv", output);

        Console.WriteLine("Zapisano filtrowany zbiór danych do iris_filtered.csv.");
    }
}
