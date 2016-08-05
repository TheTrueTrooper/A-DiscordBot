using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions;

namespace DiscordBot2._0
{
    public class anime
    {
        public List<entry> entry;
    }

    public class entry
    {
        public int id;
        public string title;
        public string english;
        public string synonyms;
        public ushort episodes;
        public string type;
        public string status;
        public string start_date;
        public string end_date;
        public string synopsis;
        public string image;
    }

    internal class AnimeResult
    {
        internal AnimeResult(anime ResultsIn)
        {
            Animes = ResultsIn;
        }

        public anime Animes;

        public string GetResult(int Index)
        {
            return Index < Animes.entry.Count() ?
                "Title: " + Animes.entry[Index].title +
                "\nEnglish: " + Animes.entry[Index].english +
                "\nSynonyms: " + Animes.entry[Index].synonyms +
                "\nEpisodes: " + Animes.entry[Index].episodes +
                "\nType: " + Animes.entry[Index].type +
                "\nStatus: " + Animes.entry[Index].status +
                "\nStart Date: " + Animes.entry[Index].start_date +
                "\nEnd Date: " + Animes.entry[Index].end_date +
                "\nSynopsis: " + Animes.entry[Index].synopsis +
                "\nhttp://myanimelist.net/anime/" + Animes.entry[Index].id
                : "Sorry but there is no Index that high";
        }

        public string GetResults(int Number)
        {
            string Results = "";
            for (int i = 0; i < Number && i < Animes.entry.Count(); i++)
            {
                Results += "\n" + i + "."  + GetResult(i);
            }
            return Results;
        }
    }


    class MyAnimeListGetter
    {
        RestClient _Client = new RestClient("http://myanimelist.net");

        public MyAnimeListGetter()
        {
            _Client.Authenticator = new SimpleAuthenticator("user", "The_Trooper", "password", "Jim456852,.,");

        }

        public async Task<AnimeResult> SearchAnime(string Name)
        {
            RestRequest RestRequest = new RestRequest("/api/anime/search.xml", Method.GET);
            RestRequest.AddParameter("q", Name.Replace(' ', '+'));

            IRestResponse<anime> Response = await _Client.ExecuteGetTaskAsync<anime>(RestRequest);

            if (Response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                throw new ApplicationException(message, Response.ErrorException);
            }

            return new AnimeResult(Response.Data);
        }

        public async Task<string> GetAnime(string Name, int Number = 1)
        {
            AnimeResult _AnimeResult = await SearchAnime(Name);
            string Results = "";
            for (int i = 0; i<Number && i< _AnimeResult.Animes.entry.Count(); i++)
            {
                Results += "\n" + i + "."  + _AnimeResult.GetResult(i);
            }
            return Results;
        }

    }
}
