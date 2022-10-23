using System.Threading.Channels;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace TwitchBOT
{
    public class Bot
    {
        public TwitchClient client = new TwitchClient();
        ConnectionCredentials credentials = new ConnectionCredentials("botyarastreamer", "oauth:83aoeeykfnhvq0o1px7ay8a482kupi");
        private Random rnd = new Random();
        string[] badWords = new string[] { "пидор","негр","нига","пидарас","пидарасина","пидарила","негритоска","п.и.д.а.р","педик","пидр","нигер","пидар","пидараска","пидрила" };
        private ulong sendedMessagesCount = 0;

        
        public Bot()
        {
            client.Initialize(credentials, "Lion_Killer123");

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;
            client.OnMessageReceived += Client_OnMessageReceived;
            // client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnDisconnected += Client_OnDisconnected;
            client.OnHostingStarted += Client_OnHostingStarted;
            client.Connect();
        }

        // private void Client_OnNewSubscriber(object sender, OnMessageReceivedArgs e)
        // {
        //     
        // }
        private void Client_OnHostingStarted(object? sender, OnHostingStartedArgs e)
        {
            client.Reconnect();
        }

        private void Client_OnDisconnected(object? sender, OnDisconnectedEventArgs e)
        {
            client.Reconnect();
        }
        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            
            if (e.ChatMessage.Message.ToLower().Contains("привет"))
            {
                SendMessage(e.ChatMessage,"Добро пожаловать, не забудь подписатся");
            }
            if (!e.ChatMessage.IsBroadcaster) // && !e.ChatMessage.IsModerator
            {
                foreach (var badWord in badWords)
                {
                    if (e.ChatMessage.Message.ToLower().Contains(badWord.ToLower()))
                    {
                        if (e.ChatMessage.IsSubscriber)
                        {
                            client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromSeconds(30),
                                "Осуждаю не одабряю Оформи лучше подписку ещё на месяц 🏳‍🌈 !!!");
                        }
                        else
                        {
                            client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromSeconds(30),
                                "Осуждаю не одабряю быдло 🏳‍🌈 !!!");
                        }
                    }
                }
            }

            if (sendedMessagesCount >= 50)
            {
                SendMessage(e.ChatMessage, "/announce 🎆НЕЗАБУДЬ Зафоловится,а и по возможности подписатся🎇");
                sendedMessagesCount = 0;
            }
            sendedMessagesCount++;
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            var command = e.Command.CommandText.ToLower();
            var now = DateTime.Now;

            if (command == "размергруди")
            {
                var result1 = rnd.Next(0, 6);
                SendMessage(e.Command.ChatMessage,
                    $"Дойки: {result1}-го  {e.Command.ChatMessage.Username}");
            }
            else if (command == "др")
            {
                var dr = new DateTime(now.Year+1, 10, 18);
                var subtraction = dr.Subtract(now);
                SendMessage(e.Command.ChatMessage, $"Мой др через: {subtraction.Days} дней");
            }
            else if (command == "бибаметр")
            {
                var result1 = rnd.Next(0, 32);
                SendMessage(e.Command.ChatMessage,
                    $"Результат: {result1} см  {e.Command.ChatMessage.Username}");
            }
            else if (command == "help")
            {
                SendMessage(e.Command.ChatMessage, 
                "!бибаметр" +
                "\n !размергруди" +
                "\n !др");
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

        private void SendMessage(ChatMessage chatMessage, string message)
        {
            client.SendMessage(chatMessage.Channel, message);
        } 
    }
}