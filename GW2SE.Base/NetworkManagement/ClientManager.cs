using System;
using System.Collections.Generic;

namespace GW2SE.Base.NetworkManagement
{
    public class ClientManager
    {
        System.Collections.Generic.
        Dictionary<NetID, Client> clientList;

        public ClientManager()
        {
            clientList = new Dictionary<NetID, Client>();
        }

        public bool TryGetClient(NetID ID, out Client GW2Client)
        {
            Client client;
            bool result = clientList.TryGetValue(ID, out client);
            GW2Client = client;
            return result;
        }

        public void AddClient(Client GW2Client)
        {
            if (!clientList.ContainsValue(GW2Client) && !clientList.ContainsKey(GW2Client.ID))
            {
                clientList.Add(GW2Client.ID, GW2Client);
            }
        }

        public void RemoveClient(NetID ID)
        {
            if (clientList.ContainsKey(ID))
            {
                clientList.Remove(ID);
            }
        }

        public Client[] ToArray()
        {
            List<Client> array = new List<Client>();

            foreach (KeyValuePair<NetID, Client> client in clientList)
            {
                array.Add(client.Value);
            }

            return array.ToArray();
        }
    }
}
