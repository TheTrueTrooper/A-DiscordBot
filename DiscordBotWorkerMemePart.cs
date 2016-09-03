using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.API;
using Discord.Commands;
using Discord.ETF;
using Discord.Modules;
using Discord.Net;
using System.Timers;
using Discord.API.Converters;

namespace DiscordBot2._0
{
    partial class DiscordBotWorker
    {
        Memer _Memer = new Memer();

        Dictionary<string, string> Memes = new Dictionary<string, string>() {{ "benderbdaycard", "BenderBirthDayParty.jpg" } };

        private bool MemeTree(MessageEventArgs e)
        {
            string[] Args = ArgMaker(e.Message.Text.Remove(0, 2));
            if (Args[0].ToLower() == "help")
            {
                e.Channel.SendMessage("Thanks for asking!:kissing_heart:\nFor Memes Commands.:japanese_goblin:\nmeme [key] [TopText] [BottomText] -> selects a photo on key and places text on top and bottom.");
            }
            else if (Args[0].ToLower() == "meme")
            {
                if (Args.Count() == 4)
                {
                    if (Memes.ContainsKey(Args[1].ToLower()))
                        SendMeme(e, Args);
                }
            }
            else return false;
            return true;
        }

        async void SendMeme(MessageEventArgs e, string[] Args)
        {
            _Memer.DrawText(Args[2], Args[3], PathGetter.GetMemePath(Memes[Args[1].ToLower()])).Save(PathGetter.TempImage);
            await e.Channel.SendFile(PathGetter.TempImage);
        }
    }
}
