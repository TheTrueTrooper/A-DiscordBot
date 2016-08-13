using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions;
using RestSharp.Deserializers;

namespace DiscordBot2._0
{
    [DeserializeAs(Name = "entry")]
    public class Anime
    {
        public int id { get; set; }
        public string title { get; set; }
        public string english { get; set; }
        public string synonyms { get; set; }
        public int episodes { get; set; }
        public string score { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string synopsis { get; set; }
        public string image { get; set; }
    }

    internal class AnimeResult
    {
        internal AnimeResult(List<Anime> ResultsIn)
        {
            Animes = ResultsIn;
        }

        public List<Anime> Animes;

        public string GetResult(int Index)
        {
            return Animes != null && Index < Animes.Count() ?
                "Title: " + Animes[Index].title +
                "\nEnglish: " + Animes[Index].english +
                "\nSynonyms: " + Animes[Index].synonyms +
                "\nEpisodes: " + Animes[Index].episodes +
                "\nScore: " + Animes[Index].score +
                "\nType: " + Animes[Index].type +
                "\nStatus: " + Animes[Index].status +
                "\nStart Date: " + Animes[Index].start_date +
                "\nEnd Date: " + Animes[Index].end_date +
                "\nSynopsis: " + Animes[Index].synopsis.Replace("<br />", "").Replace("[i]", "").Replace("[/i]", "").HtmlDecode() +
                "\nhttp://myanimelist.net/anime/" + Animes[Index].id
                : null;
        }

        public string GetResults(int Number)
        {
            string Results = "";
            for (int i = 0; i < Number && i < Animes.Count(); i++)
            {
                Results += "\n" + i + "." + GetResult(i);
            }
            return Results;
        }
    }

    [DeserializeAs(Name = "entry")]
    public class Manga
    {
        public int id { get; set; }
        public string title { get; set; }
        public string english { get; set; }
        public string synonyms { get; set; }
        public int chapters { get; set; }
        public int volumes { get; set; }
        public string score { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string synopsis { get; set; }
        public string image { get; set; }
    }

    internal class MangaResult
    {
        internal MangaResult(List<Manga> ResultsIn)
        {
            Mangas = ResultsIn;
        }

        public List<Manga> Mangas;

        public string GetResult(int Index)
        {
            return Mangas != null && Index < Mangas.Count() ?
                "Title: " + Mangas[Index].title +
                "\nEnglish: " + Mangas[Index].english +
                "\nSynonyms: " + Mangas[Index].synonyms +
                "\nChapters: " + Mangas[Index].chapters +
                "\nVolumes: " + Mangas[Index].volumes +
                "\nScore: " + Mangas[Index].score +
                "\nType: " + Mangas[Index].type +
                "\nStatus: " + Mangas[Index].status +
                "\nStart Date: " + Mangas[Index].start_date +
                "\nEnd Date: " + Mangas[Index].end_date +
                "\nSynopsis: " + Mangas[Index].synopsis.Replace("<br />", "").Replace("[i]", "").Replace("[/i]", "").HtmlDecode() +
                "\nhttp://myanimelist.net/manga/" + Mangas[Index].id
                : null;
        }

        public string GetResults(int Number)
        {
            string Results = "";
            for (int i = 0; i < Number && i < Mangas.Count(); i++)
            {
                Results += "\n" + i + "." + GetResult(i);
            }
            return Results;
        }
    }


    class MyAnimeListGetter
    {
        static RestClient _Client = new RestClient("http://myanimelist.net");

        public MyAnimeListGetter()
        {
            _Client.Authenticator = new HttpBasicAuthenticator("The_True_Trooper", "Jim456852,.,");
            _Client.PreAuthenticate = false;
        }

        public MyAnimeListGetter(string Account, string Password)
        {
            _Client.Authenticator = new HttpBasicAuthenticator(Account, Password);
            _Client.PreAuthenticate = false;
        }

        public async Task<AnimeResult> SearchAnime(string Name)
        {
            RestRequest RestRequest = new RestRequest("/api/anime/search.xml", Method.GET);
            RestRequest.AddParameter("q", Name, ParameterType.QueryString);

            IRestResponse<List<Anime>> Response = await _Client.ExecuteGetTaskAsync<List<Anime>>(RestRequest);

            if (Response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                throw new ApplicationException(message, Response.ErrorException);
            }

            return new AnimeResult(Response.Data);
        }

        public async Task<string> GetAnime(string Name, int Number = 0)
        {
            AnimeResult _AnimeResult = await SearchAnime(Name);
            if (_AnimeResult.GetResult(Number) != null)
                return "\n" + (Number + 1) + "."  + _AnimeResult.GetResult(Number);
            return null;
        }

        public async Task<MangaResult> SearchManga(string Name)
        {
            RestRequest RestRequest = new RestRequest("/api/manga/search.xml", Method.GET);
            RestRequest.AddParameter("q", Name, ParameterType.QueryString);

            IRestResponse<List<Manga>> Response = await _Client.ExecuteGetTaskAsync<List<Manga>>(RestRequest);

            if (Response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                throw new ApplicationException(message, Response.ErrorException);
            }

            return new MangaResult(Response.Data);
        }

        public async Task<string> GetManga(string Name, int Number = 0)
        {
            MangaResult _MangaResult = await SearchManga(Name);
            if (_MangaResult.GetResult(Number) != null)
                return "\n" + (Number + 1) + "." + _MangaResult.GetResult(Number);
            return null;
        }

    }
}
