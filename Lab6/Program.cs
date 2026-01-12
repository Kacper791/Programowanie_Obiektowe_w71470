using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

public class Student
{
    public int StudentId { get; set; }
    public string Imie { get; set; } = "";
    public string Nazwisko { get; set; } = "";
    public List<Ocena> Oceny { get; set; } = new();
}

public class Ocena
{
    public int OcenaId { get; set; }
    public double Wartosc { get; set; }
    public string Przedmiot { get; set; } = "";
    public int StudentId { get; set; }
}

public class Program
{
    static string connectionString =
        "Data Source=10.200.2.28;" +
        "Initial Catalog=studenci_71470;" +
        "Integrated Security=True;" +
        "Encrypt=True;" +
        "TrustServerCertificate=True";

    public static void Main()
    {
        // Zadanie 4
        WyswietlStudentow();

        // Zadanie 5
        WyswietlStudentaPoId(1);

        // Zadanie 6
        var studenci = PobierzStudentowZOcenami();
        WyswietlStudentowZOcenami(studenci);

        // Zadanie 7
        DodajStudenta(new Student { Imie = "Jan", Nazwisko = "Kowalski" });

        // Zadanie 8
        DodajOcene(new Ocena { StudentId = 1, Przedmiot = "Matematyka", Wartosc = 4.5 });

        // Zadanie 9
        UsunOcenyZGeografii();

        // Zadanie 10
        AktualizujOcene(1, 5);
    }

    // ZADANIE 4 
    static void WyswietlStudentow()
    {
        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new("SELECT * FROM Student", conn);
        SqlDataReader r = cmd.ExecuteReader();

        while (r.Read())
            Console.WriteLine($"{r["student_id"]} {r["imie"]} {r["nazwisko"]}");
    }

    // ZADANIE 5 
    static void WyswietlStudentaPoId(int id)
    {
        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new("SELECT imie, nazwisko FROM Student WHERE student_id=@id", conn);
        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader r = cmd.ExecuteReader();
        if (r.Read())
            Console.WriteLine($"{r["imie"]} {r["nazwisko"]}");
        else
            Console.WriteLine("Nie znaleziono studenta.");
    }

    // ZADANIE 6
    static List<Student> PobierzStudentowZOcenami()
    {
        List<Student> lista = new();

        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new(@"
            SELECT s.student_id, s.imie, s.nazwisko,
                   o.ocena_id, o.wartosc, o.przedmiot
            FROM Student s
            LEFT JOIN Ocena o ON s.student_id = o.student_id", conn);

        SqlDataReader r = cmd.ExecuteReader();

        while (r.Read())
        {
            int id = (int)r["student_id"];
            Student? s = lista.Find(x => x.StudentId == id);

            if (s == null)
            {
                s = new Student
                {
                    StudentId = id,
                    Imie = r["imie"].ToString()!,
                    Nazwisko = r["nazwisko"].ToString()!
                };
                lista.Add(s);
            }

            if (r["ocena_id"] != DBNull.Value)
            {
                s.Oceny.Add(new Ocena
                {
                    OcenaId = (int)r["ocena_id"],
                    Wartosc = Convert.ToDouble(r["wartosc"]),
                    Przedmiot = r["przedmiot"].ToString()!,
                    StudentId = id
                });
            }
        }
        return lista;
    }

    static void WyswietlStudentowZOcenami(List<Student> studenci)
    {
        foreach (var s in studenci)
        {
            Console.WriteLine($"{s.Imie} {s.Nazwisko}");
            foreach (var o in s.Oceny)
                Console.WriteLine($"  {o.Przedmiot}: {o.Wartosc}");
        }
    }

    // ZADANIE 7 
    static void DodajStudenta(Student s)
    {
        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new("INSERT INTO Student (imie, nazwisko) VALUES (@i,@n)", conn);
        cmd.Parameters.AddWithValue("@i", s.Imie);
        cmd.Parameters.AddWithValue("@n", s.Nazwisko);

        cmd.ExecuteNonQuery();
    }

    // ZADANIE 8
    static void DodajOcene(Ocena o)
    {
        if (!PoprawnaOcena(o.Wartosc))
        {
            Console.WriteLine("Niepoprawna wartość oceny.");
            return;
        }

        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new("INSERT INTO Ocena (wartosc, przedmiot, student_id) VALUES (@w,@p,@s)", conn);
        cmd.Parameters.AddWithValue("@w", o.Wartosc);
        cmd.Parameters.AddWithValue("@p", o.Przedmiot);
        cmd.Parameters.AddWithValue("@s", o.StudentId);

        cmd.ExecuteNonQuery();
    }

    // ZADANIE 9
    static void UsunOcenyZGeografii()
    {
        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new("DELETE FROM Ocena WHERE przedmiot='Geografia'", conn);
        cmd.ExecuteNonQuery();
    }

    // ZADANIE 10
    static void AktualizujOcene(int ocenaId, double nowa)
    {
        if (!PoprawnaOcena(nowa))
        {
            Console.WriteLine("Niepoprawna wartość oceny.");
            return;
        }

        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new("UPDATE Ocena SET wartosc=@w WHERE ocena_id=@id", conn);
        cmd.Parameters.AddWithValue("@w", nowa);
        cmd.Parameters.AddWithValue("@id", ocenaId);

        cmd.ExecuteNonQuery();
    }

    // Walidacja oceny
    static bool PoprawnaOcena(double o)
    {
        if (o < 2 || o > 5) return false;
        if (o == 2.5) return false;
        if (o % 0.5 != 0) return false;
        return true;
    }
}
