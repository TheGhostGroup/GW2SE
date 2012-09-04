using System;
using System.Net;
using System.Reflection;

using GW2SE.Base;
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

            Logger.SetTitle("Guild Wars 2 Login Server [Test]");
            Console.WindowWidth += 28;

            Logger.WriteLineColored(" ----------------------------------------", ConsoleColor.Green);
            Logger.WriteLineColored("| Guild Wars 2 Emu Project - By iPHAnTom |", ConsoleColor.Green);
            Logger.WriteLineColored(" ----------------------------------------", ConsoleColor.Green);

            Logger.WriteLine();
            Logger.WriteColored("IP address: ", ConsoleColor.Gray);
            Logger.WriteLineColored(ServerAddress, ConsoleColor.Red);

            Logger.WriteLine();
            Logger.WriteLine();
            Logger.WriteColored("Creating network manager and starting server... ", ConsoleColor.Blue);
            NetworkManager.Instance = new NetworkManager(ServerAddress, 6112);
            NetworkManager.Instance.ClientDisconnected += ConnectionLost;

            Logger.WriteLineColored("[done]", ConsoleColor.Yellow);

            PacketRegistrator.Instance.RegisterPackets(Assembly.GetExecutingAssembly().GetTypes());

            Console.ForegroundColor = ConsoleColor.Cyan;

            while (NetworkManager.Instance.Running)
            {
                NetworkManager.Instance.ProcessActions();
            }
        }

        private static void ConnectionLost(NetID ID)
        {
            Console.WriteLine("Client disconnected [" + ID.Value.ToString() + "]");
        }
    }
}
