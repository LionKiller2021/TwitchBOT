namespace TwitchBOT
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Bot bot = new Bot();
            Console.ReadLine();
            await Task.Run(() => Thread.Sleep(Timeout.Infinite));
        }
    }
}