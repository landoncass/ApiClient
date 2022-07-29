using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Linq;

namespace ApiClient
{
    class Program
    {

        class Film
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("title")]
            public string FilmTitle { get; set; }

            [JsonPropertyName("original_title")]
            public string JapaneseTitle { get; set; }

            [JsonPropertyName("original_title_romanised")]
            public string PhoneticTitle { get; set; }

            [JsonPropertyName("description")]
            public string FilmDescription { get; set; }

            [JsonPropertyName("director")]
            public string Director { get; set; }

            [JsonPropertyName("producer")]
            public string Producer { get; set; }

            [JsonPropertyName("release_date")]
            public string YearReleased { get; set; }

            [JsonPropertyName("running_time")]
            public string RunningTimeInMinutes { get; set; }

            [JsonPropertyName("rt_score")]
            public string RottenTomatoScore { get; set; }
        }
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            var client = new HttpClient();

            var responseAsStream = await client.GetStreamAsync("https://ghibliapi.herokuapp.com/films");

            var films = await JsonSerializer.DeserializeAsync<List<Film>>(responseAsStream);

            Console.WriteLine();
            Console.WriteLine("Here is a list of all of the Studio Ghibli films");
            Console.WriteLine();
            foreach (var (film, index) in films.Select((item, index) => (item, index)))
            {
                Console.WriteLine($"{index + 1}. {film.FilmTitle} ({film.JapaneseTitle})");
            }

            Console.WriteLine();
            Console.WriteLine($"Which film would you like to learn more about? (1-{films.Count()})");
            var filmIndex = Int32.Parse(Console.ReadLine()) - 1;

            var selectedFilm = films[filmIndex];
            Console.WriteLine($"The film {selectedFilm.FilmTitle} ({selectedFilm.JapaneseTitle}), which is pronounced {selectedFilm.PhoneticTitle}, was released {selectedFilm.YearReleased}.");
            Console.WriteLine($"Description: {selectedFilm.FilmDescription}.");
            Console.WriteLine();
            Console.WriteLine($"Directed by {selectedFilm.Director} and produced by {selectedFilm.Producer}, it has a runtime of {selectedFilm.RunningTimeInMinutes} minutes and a Rotten Tomato score of {selectedFilm.RottenTomatoScore}.");
        }
    }
}
