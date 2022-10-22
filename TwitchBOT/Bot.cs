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
            switch (e.Command.CommandText.ToLower())
            {
                case "кубик":
                    Random rnd = new Random(); // Инициализируем генератор случайных чисел
                    var result = rnd.Next(1, 7); // Получаем случайное целое число в интервале [ 1; 7 )
                    client.SendMessage(e.Command.ChatMessage.Channel, $"Результат: {result}"); // Отправляем ответ
                    break;
                case "лето":
                    var now = DateTime.Now; // Получаем текущее время
                    var endOfSummer = new DateTime(now.Year, 9, 1); // Дата окончания лета
                    var startOfSummer = new DateTime(now.Year, 6, 1); // Дата начала лета
                    if (now < endOfSummer && now > startOfSummer) // Проверяем, является ли текущая дата летом
                    {
                        var days = (endOfSummer - now).Days; // Получаем количество дней до конца лета
                        client.SendMessage(e.Command.ChatMessage.Channel, $"До конца лета дней осталось: {days}"); // Отправляем ответ
                    }
                    else
                    {
                        client.SendMessage(e.Command.ChatMessage.Channel, "Cейчас не лето :("); // Отправляем альтернативный ответ
                    }

                    break;
                case "осень":
                    var now1 = DateTime.Now; // Получаем текущее время
                    var endOfSummer1 = new DateTime(now1.Year, 12, 1); // Дата окончания лета
                    var startOfSummer1 = new DateTime(now1.Year,  9, 1); // Дата начала лета
                    if (now1 < endOfSummer1 && now1 > startOfSummer1) // Проверяем, является ли текущая дата летом
                    {
                        var days = (endOfSummer1 - now1).Days; // Получаем количество дней до конца лета
                        client.SendMessage(e.Command.ChatMessage.Channel, $"До конца осени дней осталось: {days}"); // Отправляем ответ
                    }
                    else
                    {
                        client.SendMessage(e.Command.ChatMessage.Channel, "Cейчас не осень :("); // Отправляем альтернативный ответ
                    }

                    break;
                case "пидор":

                    client.SendMessage(e.Command.ChatMessage.Channel, $"Результат: ОСУЖДАЮ БЫДЛО {e.Command.ChatMessage.Username}"); 
                    

                    break;
                case "бибаметр":
                    Random rnd1 = new Random(); // Инициализируем генератор случайных чисел
                    var result1 = rnd1.Next(1, 31 ); // Получаем случайное целое число в интервале [ 1; 7 )
            
            client.SendMessage(e.Command.ChatMessage.Channel, $"Результат: {result1} см  {e.Command.ChatMessage.Username}");
            break;
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