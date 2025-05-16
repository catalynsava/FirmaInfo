using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
namespace FirmaInfo;

public class FirmaInfo
{
    public string? Denumire { get; set; }
    public string? Adresa { get; set; }
    public string? StareInregistrare { get; set; }
    public bool PlatitorTVA { get; set; }
    public string? CodPostal { get; set; }
    public string? NumarRegCom { get; set; }
    public string? Telefon { get; set; }
    public string? CodCAEN { get; set; }
}

public class FirmaService
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task<FirmaInfo?> GetFirmaInfoAsync(string cui)
    {
        try
        {
            string url = $"https://api.openapi.ro/api/validate/cif/{cui.Trim()}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Eroare: {response.StatusCode}");
                return null;
            }

            string json = await response.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            return new FirmaInfo
            {
                Denumire = root.GetProperty("denumire").GetString(),
                Adresa = root.GetProperty("adresa").GetString(),
                StareInregistrare = root.GetProperty("stare_inregistrare").GetString(),
                PlatitorTVA = root.GetProperty("tvaincasare").GetBoolean(),
                CodPostal = root.GetProperty("cod_postal").GetString(),
                NumarRegCom = root.GetProperty("nr_reg_com").GetString(),
                Telefon = root.TryGetProperty("telefon", out var tel) ? tel.GetString() : "",
                CodCAEN = root.TryGetProperty("activitate", out var act) ? act.GetString() : ""
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("Eroare la interogare API: " + ex.Message);
            return null;
        }
    }
}

