using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

using GW2SE.Base.PacketManagement;

namespace GW2SE.Base.NetworkManagement
{
    public delegate void NoArgEventHandler();

    public class NetworkManager
    {

        #region Variables
        IPAddress hostAddress;
        Int32 hostPort;

        TcpListener sListener;
        ClientManager sClients;
        bool sRuninng;

        static NetworkManager instance;
        #endregion

        #region Events
        public event NetworkEventHandler ClientConnected;
        public event NetworkEventHandler ClientDisconnected;
        #endregion

        #region Properties
        public bool Running { get { return sRuninng; } set { sRuninng = value; } }
        public bool Pending { get { if (Running) { return sListener.Pending(); } else { return false; } } }
        public ClientManager Clients { get { return sClients; } }
        #endregion

        public NetworkManager(String ServerAddress, Int32 ServerPort)
        {
            if (!IPAddress.TryParse(ServerAddress, out hostAddress))
                throw new ArgumentException("Cannot parse IP address, " + ServerAddress);

            if (ServerPort == 0)
            {
                hostPort = 6112;
            }
            else
            {
                hostPort = ServerPort;
            }

            NetIDManager.Instance = new NetIDManager();
            sClients = new ClientManager();

            Start();
        }

        public void Start()
        {
            sListener = new TcpListener(hostAddress, hostPort);
            sListener.Start();
            sRuninng = true;
        }

        public void Stop()
        {
            sListener.Stop();
            sListener = null;
            sRuninng = false;
        }

        public static NetworkManager Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public void ProcessActions()
        {
            if (Pending)
            {
                var client = new Client(sListener.AcceptTcpClient().Client, NetIDManager.Instance.GenerateID());
                client.Connected += OnConnection;
                client.Disconnected += OnConnectionLost;
                sClients.AddClient(client);
            }

            foreach (Client client in sClients.ToArray())
            {
                if (!client.IsConnected())
                    client.Disconnect();

                client.CheckForIncoming();
                client.ProcessPackets();
                client.ClearQueue();
            }
        }

        public void Dispose()
        {
            Clients.Dispose();
            sListener.Stop();
        }

        private void OnConnection(NetID ID)
        {
            ClientConnected(ID);
        }

        private void OnConnectionLost(NetID ID)
        {
            ClientDisconnected(ID);
        }
    }
}