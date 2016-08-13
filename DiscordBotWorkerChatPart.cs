using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Discord;
using Discord.Audio;
using Discord.API;
using Discord.Commands;
using Discord.ETF;
using Discord.Modules;
using Discord.Net;
using System.Timers;
using Discord.API.Converters;
using CleverBot;


namespace DiscordBot2._0
{
    // the chat part for
    partial class DiscordBotWorker
    {
        CleverBotClient CleverResponder;

        /// <summary>
        /// chat tree
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool ChatTree(MessageEventArgs e)
        {

            string[] Args = ArgMaker(e.Message.Text.Remove(0, 2));
            if (Args[0] == "help")
                e.Channel.SendMessage("Thanks for asking!:kissing_heart:\nFor chat Commands.:love_letter:\nslap -> slap a person(s)\nsmite -> smite a person(s)");
            else if (Args.Count() > 1 && Args[0].ToLower() == "slap")
            {
                if (e.Message.MentionedUsers.Count() > 1)
                {
                    string users = "";
                    foreach (User u in e.Message.MentionedUsers)
                    {
                        users += " " + u.Mention;
                    }
                    e.Channel.SendMessage(e.User.Mention + " circle slaps" + users + "!");
                }
                else if (e.Message.MentionedUsers.Count() > 0 && e.Message.MentionedUsers?.First()?.Name == e.User.Name)
                {
                    e.Channel.SendMessage(e.User.Mention + " slaps him self.");
                }
                else if (e.Message.MentionedUsers.Count() > 0)
                {
                    e.Channel.SendMessage(e.User.Mention + " slaps " + e.Message.MentionedUsers.First().Mention + "!");
                }
                else
                {
                    e.Channel.SendMessage(e.User.Mention + " slaps but misses!");
                }
            }
            else if (Args[0].ToLower() == "smite")
            {
                if (e.Message.MentionedUsers.Count() > 1)
                {
                    string users = "";
                    foreach (User u in e.Message.MentionedUsers)
                    {
                        users += " " + u.Mention;
                    }
                    e.Channel.SendFile(PathGetter.GetImagePath("Smite.jpg"));
                    Thread.Sleep(20);
                    e.Channel.SendMessage(e.User.Mention + " multi smites " + users + " with the power of a thousand paper fans!");
                }
                else if (e.Message.MentionedUsers.Count() > 0 && e.Message.MentionedUsers?.First()?.Name == e.User.Name)
                {
                    e.Channel.SendFile(PathGetter.GetImagePath("Smite.jpg"));
                    Thread.Sleep(20);
                    e.Channel.SendMessage(e.User.Mention + " Smites him self with the power of a thousand paper fans!");
                }
                else if (e.Message.MentionedUsers.Count() > 0)
                {
                    e.Channel.SendFile(PathGetter.GetImagePath("Smite.jpg"));
                    Thread.Sleep(20);
                    e.Channel.SendMessage(e.User.Mention + " Smites " + e.Message.MentionedUsers.First().Mention + " with the power of a thousand paper fans!");
                }
                else
                {
                    e.Channel.SendFile(PathGetter.GetImagePath("Smite.jpg"));
                    Thread.Sleep(20);
                    e.Channel.SendMessage(e.User.Mention + " Smites with a paper fan, but misses!");
                }
            }
            else if (Args[0].ToLower() == "chat")
            {
                //TopTalk(e);
            }
            else return false;
            return true;
        }

        //async void TopTalk(MessageEventArgs e)
        //{
        //    CleverResponse Answer = await CleverResponder.Ask(e.Message.Text.Remove(0, 7));
        //    if (Answer.bSuccess)
        //        await e.Channel.SendMessage(Answer.Response);
        //    else
        //        ConsoleWrite(Answer.Status);
        //}


    }
}
