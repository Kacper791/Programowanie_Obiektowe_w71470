public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("To jest cwiczenie 1");
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("To jest cwiczenie 1");

        Zwierze[] zwierzeta = new Zwierze[3];

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"\nPodaj dane dla zwierzęcia {i + 1}:");

            Console.Write("Nazwa: ");
            string nazwa = Console.ReadLine();

            Console.Write("Gatunek: ");
            string gatunek = Console.ReadLine();

            Console.Write("Liczba nóg: ");
            int liczbaNog = int.Parse(Console.ReadLine());

            zwierzeta[i] = new Zwierze(nazwa, gatunek, liczbaNog);
        }

        Zwierze klon = new Zwierze(zwierzeta[0]); 
        klon.Nazwa = "Klon_" + zwierzeta[0].Nazwa;

        Console.WriteLine("\nInformacje o zwierzętach:");
        foreach (Zwierze z in zwierzeta)
        {
            Console.WriteLine(z.ToString());
            z.DajGlos();
        }

        Console.WriteLine(klon.ToString());
        klon.DajGlos();

        Console.WriteLine($"\nLiczba utworzonych zwierząt: {Zwierze.GetLiczbaZwierzat()}");
    }
}

public class Zwierze
{

    private string nazwa;
    private string gatunek;
    private int liczbaNog;
    private static int liczbaZwierzat = 0;


    public string Nazwa
    {
        get { return nazwa; }
        set { nazwa = value; }
    }


    public string GetGatunek() => gatunek;
    public int GetLiczbaNog() => liczbaNog;

    public void SetNazwa(string nowaNazwa)
    {
        nazwa = nowaNazwa;
    }

    public Zwierze()
    {
        nazwa = "Rex";
        gatunek = "Pies";
        liczbaNog = 4;
        liczbaZwierzat++;
    }

    public Zwierze(string nazwa, string gatunek, int liczbaNog)
    {
        this.nazwa = nazwa;
        this.gatunek = gatunek;
        this.liczbaNog = liczbaNog;
        liczbaZwierzat++;
    }

    public Zwierze(Zwierze inne)
    {
        this.nazwa = inne.nazwa;
        this.gatunek = inne.gatunek;
        this.liczbaNog = inne.liczbaNog;
        liczbaZwierzat++;
    }

    public void DajGlos()
    {
        switch (gatunek.ToLower())
        {
            case "kot":
                Console.WriteLine($"{nazwa}: Miau!");
                break;
            case "krowa":
                Console.WriteLine($"{nazwa}: Muuu!");
                break;
            case "pies":
                Console.WriteLine($"{nazwa}: Hau hau!");
                break;
            default:
                Console.WriteLine($"{nazwa}: (Brak dźwięku dla gatunku: {gatunek})");
                break;
        }
    }

    public static int GetLiczbaZwierzat()
    {
        return liczbaZwierzat;
    }

    public override string ToString()
    {
        return $"Zwierzę: {nazwa}, Gatunek: {gatunek}, Liczba nóg: {liczbaNog}";
    }

    ~Zwierze()
    {
        Console.WriteLine($"Zwierzę {nazwa} zostało zniszczone.");
    }
}
