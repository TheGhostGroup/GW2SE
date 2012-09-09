using System;

namespace GW2SE.PluginSystem
{
    public delegate void PluginEventHandler(object Sender);

    public class Events
    {
        #region Instances
        static  LoginServerEvents loginInstance = new LoginServerEvents();

        public static LoginServerEvents LoginServer
        {
            get
            {
                return loginInstance;
            }
        }

        static GameServerEvents gameInstance = new GameServerEvents();

        public static GameServerEvents GameServer
        {
            get
            {
                return gameInstance;
            }
        }
        #endregion

        #region LoginServerEvents
        public class LoginServerEvents
        {
            internal LoginServerEvents() { }

            #region Events
            public event PluginEventHandler ClientVersionReceived;
            public event PluginEventHandler ClientSeedReceived;
            #endregion

            #region Event triggers
            public void OnClientVersionReceived(object Sender) { if (ClientVersionReceived != null) { ClientVersionReceived(Sender); } }
            public void OnClientSeedReceived(object Sender) { if (ClientSeedReceived != null) { ClientSeedReceived(Sender); } }
            #endregion
        }
        #endregion

        #region GameServerEvents
        public class GameServerEvents
        {
            internal GameServerEvents() { }

            #region Events

            #endregion

            #region Event triggers

            #endregion
        }
        #endregion
    }
}