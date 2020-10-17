using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Azmongir
{
    public class AppSetting
    {
        public static string fileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + Assembly.GetExecutingAssembly().GetName().Name;

        #region
        public static string SkinName = "Skin";

        #endregion
        public static string ReadSettings(string key)
        {
            string result =string.Empty;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? "0";
            }
            catch (ConfigurationErrorsException)
            {

                result = "Error Setting App Setting";
            }
            return result;
        }


        public static void AddOrUpdate(string key,string value)
        {
            try
            {
                var ConfigFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = ConfigFile.AppSettings.Settings;
                if (settings[key] == null)
                    settings.Add(key, value);
                else
                    settings[key].Value = value;

                ConfigFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(ConfigFile.AppSettings.SectionInformation.Name);

            }
            catch (Exception)
            {

                
            }
        }
    }
}
