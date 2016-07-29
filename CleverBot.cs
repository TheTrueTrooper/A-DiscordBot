using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace CleverBot
{
    public class CleverResponse
    {
        /// <summary>
        /// constructor but for internal use only
        /// </summary>
        /// <param name="_Status"></param>
        /// <param name="_Response"></param>
        internal CleverResponse(string _Status, string _Response)
        {
            Status = _Status;
            Response = _Response;
        }

        /// <summary>
        /// the full status message
        /// </summary>
        public string Status { private set; get; }
        /// <summary>
        /// the text response duuuu
        /// </summary>
        public string Response { private set; get; }
        /// <summary>
        /// a quick bool getter to see if we got a good anwser
        /// </summary>
        public bool bSuccess { get { if (Status == "success") return true; else return false; } }
    }

    /// <summary>
    /// Client to the Clever Bot Server Do not try to construct use static AsyncCleverBot() instead
    /// </summary>
    public class CleverBotClient : IDisposable
    {

        // these are self explanitory
        string Key = "";
        string User = "";
        string Nick = "";

        /// <summary>
        /// sends and recives data
        /// </summary>
        WebClient Client = new WebClient();
        
        /// <summary>
        /// constructor https://cleverbot.io/login to get the user and key
        /// </summary>
        /// <param name="User">Your account. You can get one at https://cleverbot.io/login </param>
        /// <param name="Key">Your Key. You can get one at https://cleverbot.io/login </param>
        /// <param name="Nick">your nick name</param>
        public CleverBotClient(string _User, string _Key, string _Nick = null)
        {
            if (_User == null && _Key == null) throw new ArgumentNullException("ether _User or _Key was null. You can get them at https://cleverbot.io/login");
            User = _User;
            Key = _Key;
            Nick = _Nick;
        }

        /// <summary>
        /// Sets the nick name
        /// </summary>
        /// <param name="_Nick"></param>
        /// <returns>if the nick name was set</returns>
        public bool SetNick(string _Nick = null)
        {
            if (_Nick != null) Nick = _Nick;
            bool worked = false;

                HttpWebRequest Request = WebRequest.Create(string.Format("https://cleverbot.io/1.0/create?user={0}&key={1}&nick={2}", User, Key, Nick)) as HttpWebRequest;
                using (HttpWebResponse Response = (Request.GetResponse() as HttpWebResponse))
                using (StreamReader Reader = new StreamReader(Response.GetResponseStream()))
                {
                    string ReturnedMessage = Reader.ReadToEnd();
                    string[] Answer = ReturnedMessage.Split('&');
                    if (Answer[0].Substring(Answer[0].IndexOf("=") + 1) == "success") worked = true;
                }

            return worked;
        }

        /// <summary>
        /// Sends a Message to Clever Bot
        /// </summary>
        /// <param name="Message">the string to ask from</param>
        /// <returns>A class wrapped response</returns>
        public async Task<CleverResponse> Ask(string Message)
        {
            
            CleverResponse CleverResponseOut = null;
            try
            {
                HttpWebRequest Request = WebRequest.Create(string.Format("https://cleverbot.io/1.0/create?user={0}&key={1}&nick={2}", User, Key, Nick)) as HttpWebRequest;
                Request.Method = "POST";
                using (HttpWebResponse Response = (await Request.GetResponseAsync() as HttpWebResponse))
                using (StreamReader Reader = new StreamReader( Response.GetResponseStream()))
                {
                    string ReturnedMessage = await Reader.ReadToEndAsync();
                    string[] Answer = ReturnedMessage.Split('&');
                    CleverResponseOut = new CleverResponse(Answer[0].Substring(Answer[0].IndexOf("=") + 1), Answer[1].Substring(Answer[1].IndexOf("=") + 1));
                }
            }
            catch
            {

            }
            return CleverResponseOut;
        }

        /// <summary>
        /// Just calls the lesser disposes
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
