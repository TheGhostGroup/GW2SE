using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

using GW2SE.Base.PacketManagement;

namespace GW2SE.Base.NetworkManagement
{
    public delegate void NetworkEventHandler(NetID Connection);

    public class Client
    {
        public NetID ID { get; private set; }
        public bool Removed { get; private set; }
        public Socket Socket { get; private set; }
        public PacketManager PacketManager { get; private set; }
        public NetworkMessage[] NetworkMessages { get { return messageQueue.ToArray(); } }

        public event NetworkEventHandler Connected;
        public event NetworkEventHandler Disconnected;

        private List<NetworkMessage> messageQueue;

        public Client(Socket Socket, NetID ID)
        {
            this.Socket = Socket;
            this.ID = ID;

            PacketManager = new PacketManager(this);
            messageQueue = new List<NetworkMessage>();
        }

        public bool IsConnected()
        {
            if (!Socket.Connected)
            {
                return false;
            }

            try
            {
                if (Socket.Poll(1, SelectMode.SelectRead) && Socket.Available == 0)
                {
                    Socket.Disconnect(false);
                    return false;
                }

                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public void CheckForIncoming()
        {
            List<byte> tmpData = new List<byte>();

            if (Socket.Available > 0)
            {
                byte[] buffer = new byte[Socket.Available];

                try
                {
                    Socket.Receive(buffer);
                    tmpData.AddRange(buffer);
                }
                catch (SocketException e)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error:");
                    Console.WriteLine(e.StackTrace);
                }

                messageQueue.Add(new NetworkMessage(new System.IO.MemoryStream(tmpData.ToArray()), ID));
            }
        }

        public void ClearQueue()
        {
            messageQueue.Clear();
        }

        public void ProcessPackets()
        {
            PacketManager.ProcessPackets(NetworkMessages);
        }

        public void SendPacket(IPacketOut Packet)
        {
            var message = Packet.Handle();

            if (!Packet.Initialize())
            {
                Console.WriteLine("Error in packet " + Packet.GetType().Name + " [" + message.Header + "]");
                return;
            }

            Socket.Send(message.PacketData.ToArray());
        }

        public void Disconnect()
        {
            OnDisconnect();
            NetworkManager.Instance.Clients.RemoveClient(ID);
            Removed = true;
        }

        private void OnDisconnect()
        {
            if (Disconnected != null)
            {
                Socket.Disconnect(false);
                Disconnected(ID);
            }
        }

        private void OnConnect()
        {
            if (Connected != null)
            {
                Socket.Disconnect(false);
                Connected(ID);
            }
        }
    }
}
