using System;
using System.IO;
using System.Reflection;

namespace GW2SE.PluginSystem
{
    public class PluginManager
    {

        #region Instance
        static PluginManager instance;

        public static PluginManager Instance
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
        #endregion

        #region LoadPlugins
        public static IPlugin[] LoadPlugins(string PluginFolder)
        {
            IPlugin[] pluginTmp;

            try
            {
                var pluginFolder = Directory.GetCurrentDirectory() + "\\" + PluginFolder;

                if (!Directory.Exists(pluginFolder))
                    Directory.CreateDirectory(pluginFolder);

                var pluginFiles = Directory.GetFiles(pluginFolder, "*.DLL");

                pluginTmp = new IPlugin[pluginFiles.Length];

                for (int i = 0; i < pluginFiles.Length; i++)
                {

                    var asm = Assembly.LoadFile(pluginFiles[i]);

                    foreach (Type type in asm.GetTypes())
                    {
                        if (type.Name == "PluginMain" && typeof(IPlugin).IsAssignableFrom(type))
                        {
                            pluginTmp[i] = (IPlugin)Activator.CreateInstance(type);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return pluginTmp;
        }
        #endregion

        IPlugin[] plugins;

        public PluginManager(IPlugin[] Plugins)
        {
            plugins = Plugins;
        }

        public void HandleOnEnabled()
        {
            foreach (IPlugin plugin in plugins)
            {
                plugin.OnEnable();
            }
        }

        public void HandleOnDisabled()
        {
            foreach (IPlugin plugin in plugins)
            {
                plugin.OnDisable();
            }
        }
    }
}
