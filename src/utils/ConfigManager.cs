using McGuard.src.structures;
using System;
using System.Collections.Generic;
using System.IO;

namespace McGuard.src.utils
{
    internal class ConfigManager
    {
        /// <summary>
        /// Configuration list
        /// </summary>
        private static List<Configuration> configList = new List<Configuration>();
        
        /// <summary>
        /// Clears the list of configuration.
        /// </summary>
        public static void ClearConfiguration()
        {
            if (configList.Count > 0)
            {
                configList.Clear();
            }
        }

        /// <summary>
        /// Loads a configuration from file to memory
        /// </summary>
        public static void LoadConfiguration(string configPath, List<string> sampleConfig = null)
        {
            if (sampleConfig != null)
            {
                if (!File.Exists(configPath))
                {
                    File.WriteAllLines(configPath, sampleConfig);
                }
            }
            else
            {
                if (!File.Exists(configPath))
                {
                    File.WriteAllLines(configPath, sampleConfig);
                }
            }

            string configContent = File.ReadAllText(configPath);

            foreach (var configContext in configContent.Split('\n'))
            {
                if (configContent.Trim().Length > 0 && configContent.Contains("="))
                {
                    string[] configArgs = configContent.Split('=');

                    string configKey = configArgs[0].Trim();
                    string configVal = configArgs[1].Trim();

                    if (configKey.Length > 0)
                    {
                        configList.Add(new Configuration
                        {
                            ConfigKey = configKey,
                            ConfigVal = configVal,
                            IsSet = configVal.Length > 0
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Returns a value from the configuration key
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <returns>Value of the config key</returns>
        public static string GetValueByKey(string key)
        {
            foreach (var configContext in configList)
            {
                if (configContext.ConfigKey == key)
                {
                    return configContext.ConfigVal;
                }
            }

            return null;
        }
    }
}
