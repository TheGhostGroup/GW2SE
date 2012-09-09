using System;

namespace GW2SE.PluginSystem
{
    public interface IPlugin
    {
        void OnEnable();
        void OnDisable();
    }
}