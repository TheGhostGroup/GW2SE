using System;
using System.Collections.Generic;

namespace GW2SE.Base.NetworkManagement
{
    public class NetIDManager
    {
        List<NetID> idList;
        static NetIDManager instance;

        public NetIDManager()
        {
            idList = new List<NetID>();
        }

        public static NetIDManager Instance
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

        public NetID GenerateID()
        {
            if (idList.Count < 10000 && idList.Count >= 0)
            {
                NetID tmp = new NetID(idList.Count);
                idList.Add(tmp);
                return tmp;
            }

            else
            {
                return new NetID(-1);
            }
        }

        public bool RemoveID(NetID ID)
        {
            return idList.Remove(ID);
        }

        public NetID[] List { get { return idList.ToArray(); } }
    }
}
