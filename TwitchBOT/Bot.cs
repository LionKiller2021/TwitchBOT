using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;

namespace TwitchBOT
{
    public class Bot
    {
        TwitchClient client = new TwitchClient();
        ConnectionCredentials credentials = new ConnectionCredentials("botyarastreamer", "oauth:83aoeeykfnhvq0o1px7ay8a482kupi");
        private Random rnd = new Random();

        public Bot()
        {
            client.Initialize(credentials, "Lion_Killer123");

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;
            client.OnMessageReceived += Client_OnMessageReceived;

            client.Connect();
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (!e.ChatMessage.IsBroadcaster && !e.ChatMessage.IsModerator)
            {
                if (e.ChatMessage.Message.ToLower().Contains("плохой человек"))
                {
                    client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromSeconds(30), "Нет, ты плохой человек");
                }
            }
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            var command = e.Command.CommandText.ToLower();
            var now = DateTime.Now;

            if (command == "размергруди")
            {
                var result1 = rnd.Next(0, 6);
                client.SendMessage(e.Command.ChatMessage.Channel,
                    $"Дойки: {result1}-го  {e.Command.ChatMessage.Username}");
            }
            else if (command == "др")
            {
                var dr = new DateTime(now.Year+1, 10, 18);
                var subtraction = dr.Subtract(now);
                client.SendMessage(e.Command.ChatMessage.Channel, $"Мой др через: {subtraction.Days} дней");
            }
            else if (command == "бибаметр")
            {
                var result1 = rnd.Next(0, 32);
                client.SendMessage(e.Command.ChatMessage.Channel,
                    $"Результат: {result1} см  {e.Command.ChatMessage.Username}");
            }
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            client.SendMessage(e.Channel, "Привет! Бот успешно подключен к каналу.");
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}