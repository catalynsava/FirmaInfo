namespace FirmaInfo;

class Program
{
    static async Task Main()
    {
        Console.Write("Introdu CUI: ");
        string? cui = Console.ReadLine();

        var firma = await FirmaService.GetFirmaInfoAsync(cui);

        if (firma != null)
        {
            Console.WriteLine($"Denumire: {firma.Denumire}");
            Console.WriteLine($"Adresă: {firma.Adresa}");
            Console.WriteLine($"TVA: {(firma.PlatitorTVA ? "DA" : "NU")}");
            Console.WriteLine($"CAEN: {firma.CodCAEN}");
        }
        else
        {
            Console.WriteLine("Nu s-au găsit date pentru acest CUI.");
        }
    }
}
