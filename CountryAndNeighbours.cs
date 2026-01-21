using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CountryNeighborsApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Country Neighbor Finder ===");

            while (true)
            {
                Console.Write("\nEnter Country Code (Example: IN, US, NZ) or type 'q' to quit: ");
                string countryCode = Console.ReadLine()?.Trim().ToUpper();

                if (countryCode == "Q")
                {
                    Console.WriteLine("Exiting application. Goodbye!");
                    break;
                }

                try
                {
                    CountryDetails countryDetails =
                        await GetCountryAndNeighborsAsync(countryCode);

                    if (string.IsNullOrWhiteSpace(countryDetails.FullName))
                    {
                        Console.WriteLine("Invalid country code. Please try again.");
                        continue;
                    }

                    Console.WriteLine($"\nCountry: {countryDetails.FullName} ({countryCode})");

                    if (countryDetails.Neighbors.Count > 0)
                    {
                        Console.WriteLine("Neighboring countries:");
                        foreach (string neighbor in countryDetails.Neighbors)
                        {
                            Console.WriteLine($"- {neighbor}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No neighboring countries found.");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Network error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }

        public class CountryDetails
        {
            public string FullName { get; set; }
            public List<string> Neighbors { get; set; } = new List<string>();
        }

        static async Task<CountryDetails> GetCountryAndNeighborsAsync(string countryCode)
        {
            CountryDetails countryDetails = new CountryDetails();

            using HttpClient httpClient = new HttpClient();

            try
            {
                string countryUrl = $"https://restcountries.com/v3.1/alpha/{countryCode}";
                HttpResponseMessage response = await httpClient.GetAsync(countryUrl);

                if (!response.IsSuccessStatusCode)
                {
                    return countryDetails;
                }

                string countryResponse = await response.Content.ReadAsStringAsync();
                JArray countryData = JArray.Parse(countryResponse);

                countryDetails.FullName =
                    countryData[0]["name"]?["common"]?.ToString();

                JArray borders = countryData[0]["borders"] as JArray;

                if (borders == null)
                {
                    return countryDetails;
                }

                foreach (var borderCode in borders)
                {
                    string neighborUrl =
                        $"https://restcountries.com/v3.1/alpha/{borderCode}";

                    HttpResponseMessage neighborResponse =
                        await httpClient.GetAsync(neighborUrl);

                    if (!neighborResponse.IsSuccessStatusCode)
                    {
                        continue;
                    }

                    string neighborResponseContent =
                        await neighborResponse.Content.ReadAsStringAsync();

                    JArray neighborData = JArray.Parse(neighborResponseContent);

                    string neighborName =
                        neighborData[0]["name"]?["common"]?.ToString();

                    if (!string.IsNullOrWhiteSpace(neighborName))
                    {
                        countryDetails.Neighbors.Add(neighborName);
                    }
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing country data: {ex.Message}");
            }

            return countryDetails;
        }
    }
}
