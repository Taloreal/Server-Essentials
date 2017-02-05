using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2.Client
{
    public class SettingsFile
    {
        public string Username = "user";
        public string SelectedIP = "Default";
        public string Foreground = "";
        public string Background = "";
        public string Text = "";
        public bool Reading = false;
        public bool Dating = false;
        public bool logging = false;

        //public static SettingsFile LoadSettings(string Location)
        //{
        //
        //}

        /// <summary>
        /// Saves the selected settings.
        /// </summary>
        /// <param name="Location">The location to save the settings</param>
        /// <returns>True = worked, False = Failed</returns>
        public bool SaveSettings(string Location)
        {
            try {
                return true;
            }
            catch { 
                return false; 
            }
        }
    }
}
