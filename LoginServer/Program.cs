using System;
using System.Net;
using System.Reflection;

using GW2SE.Base;
using GW2SE.PluginSystem;
using GW2SE.Base.NetworkManagement;
using GW2SE.Base.PacketManagement;

namespace GW2SE.LoginServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string ServerAddress = IPAddress.Loopback.ToString();

            if (args.Length == 2)
            {
                if (args[0] == "address")
                {
                    IPAddress serverIP;
                    if (IPAddress.TryParse(args[1], out serverIP))
                    {
                        ServerAddress = serverIP.ToString();
                    }
                }
            }

            Console.CancelKeyPress += CtrlC;
            Console.Title = "Guild Wars 2 Login Server [Test]";
            Console.WindowWidth += 28;
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(@"   ____________________________");
            Console.WriteLine(@"  /                            \");
            Console.WriteLine(@" <        --[ GW2Emu ]--        >");
            Console.WriteLine(@"  \____________________________/");
            Console.WriteLine();
            Console.Write(" => IP address: ");
            Console.WriteLine(ServerAddress);

            Console.WriteLine();
            Console.Write(" => Creating network manager and starting server... ");
            NetworkManager.Instance = new NetworkManager(ServerAddress, 6112);
            NetworkManager.Instance.ClientDisconnected += ConnectionLost;
            Console.WriteLine("[done]");

            Console.Write(" => Registering packets... ");
            PacketRegistrator.Instance.RegisterPackets(Assembly.GetExecutingAssembly().GetTypes());
            Console.WriteLine("[done]");

            Console.Write(" => Loading plugins... ");
            try
            {
                PluginManager.Instance = new PluginManager(PluginManager.LoadPlugins("Plugin"));
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                Console.Write("Press any key to exit... ");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("[done]");

            PluginManager.Instance.HandleOnEnabled();

            while (NetworkManager.Instance.Running)
            {
                NetworkManager.Instance.ProcessActions();
            }
        }

        private static void CtrlC(object sender, ConsoleCancelEventArgs e)
        {
            PluginManager.Instance.HandleOnDisabled();
        }

        private static void ConnectionLost(NetID ID)
        {
            Console.WriteLine("Client disconnected [" + ID.Value.ToString() + "]");
        }
    }
}
