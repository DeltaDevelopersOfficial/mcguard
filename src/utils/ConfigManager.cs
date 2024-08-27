using McGuard.src.structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace McGuard.src.utils
{
    internal class ConfigManager
    {
        /// <summary>
        /// List of required configuration keys
        /// </summary>
        private static List<string> requiredConfigKeys = new List<string>()
        {
            "joinmsg"
        };

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
        /// Validates a configuration
        /// It checks if all NEEDED values/keys are entered
        /// </summary>
        /// <returns>Returns TRUE if all required config it's found in config file</returns>
        public static bool ValidateConfiguration()
        {
            foreach (var keys in requiredConfigKeys)
            {
                if (!HasKey(keys))
                {
                    return false;
                }
            }

            return true;
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
                if (configContext.Trim().Length > 0 && configContext.Contains("="))
                {
                    string[] configArgs = configContext.Split('=');

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

        /// <summary>
        /// Checks the configuration if it has a specified key
        /// </summary>
        /// <param name="keyName">Needle</param>
        /// <returns>Returns TRUE if KEY exists</returns>
        public static bool HasKey(string keyName)
        {
            return configList.Where(conf => conf.ConfigKey == keyName).Select(conf => conf).Count() > 0;
        }

        /// <summary>
        /// Checks the configuration if it has a set value at key
        /// </summary>
        /// <param name="keyName">Needle</param>
        /// <returns>Returns TRUE if is any value SET at KEY</returns>
        /*
         * 
         * NOT USED NOW, IT's UNECESSARY TO COMPILE IT
         * 
         * 
        public static bool HasVal(string keyName)
        {
            return configList.Where(conf => conf.ConfigKey == keyName && conf.ConfigVal.Trim().Length > 0).Count() > 0;
        }
        */
    }
}
