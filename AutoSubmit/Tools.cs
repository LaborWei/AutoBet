using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoSubmit
{
    public class Tools
    {
        public static string fileName = System.IO.Path.GetFileName(Application.ExecutablePath);
        public static bool addSetting(string key, string value)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(fileName);
            config.AppSettings.Settings.Add(key, value);
            config.Save();
            return true;
        }
        public static string getSetting(string key)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(fileName);
            string value = config.AppSettings.Settings[key].Value;
            return value;
        }
        public static bool updateSeeting(string key, string newValue)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(fileName);
            string value = config.AppSettings.Settings[key].Value = newValue;
            config.Save();
            return true;
        }
    }
}
