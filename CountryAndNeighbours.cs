using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CountryNeighboursApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Country Neighbour Finder ===");

            while (true)
            {
                Console.Write("\nEnter Country Code (Example: IN, US, NZ) or type 'q' to quit: ");
                string inputCountryCode = Console.ReadLine().Trim().ToUpper();

                if (inputCountryCode == "Q")
                {
                    Console.WriteLine("Exiting application. Goodbye!");
                    break;
                }

                try
                {
                    var countryInfo = await GetCountryAndNeighboursAsync(inputCountryCode);

                    if (!string.IsNullOrEmpty(countryInfo.FullName))
                    {
                        Console.WriteLine($"\nCountry: {countryInfo.FullName} ({inputCountryCode})");

                        if (countryInfo.Neighbours.Count > 0)
                        {
                            Console.WriteLine("Neighbouring countries:");
                            foreach (var neighbour in countryInfo.Neighbours)
                            {
                                Console.WriteLine($"- {neighbour}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No neighbouring countries found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid country code. Please try again.");
                    }
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("Invalid country code or network error. Please try again.");
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
            public List<string> Neighbours { get; set; } = new List<string>();
        }

        static async Task<CountryDetails> GetCountryAndNeighboursAsync(string countryCode)
        {
            CountryDetails countryDetails = new CountryDetails();

            using (HttpClient httpClient = new HttpClient())
            {
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

                    countryDetails.FullName = countryData[0]["name"]["common"].ToString();

                    JArray borders = (JArray)countryData[0]["borders"];

                    if (borders != null)
                    {
                        foreach (var borderCode in borders)
                        {
                            string neighbourUrl = $"https://restcountries.com/v3.1/alpha/{borderCode}";
                            HttpResponseMessage neighbourResponseMsg = await httpClient.GetAsync(neighbourUrl);

                            if (neighbourResponseMsg.IsSuccessStatusCode)
                            {
                                string neighbourResponse = await neighbourResponseMsg.Content.ReadAsStringAsync();
                                JArray neighbourData = JArray.Parse(neighbourResponse);
                                string neighbourName = neighbourData[0]["name"]["common"].ToString();
                                countryDetails.Neighbours.Add(neighbourName);
                            }
                        }
                    }
                }
                catch
                {
                    return countryDetails;
                }
            }
            return countryDetails;
        }
    }
}

 
