namespace ChatClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000); // задержка 1 секунда между запусками
            Client client = new Client();
            client.Start();
        }
    }
}
