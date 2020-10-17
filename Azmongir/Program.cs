using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using System.Threading;
using DevExpress.LookAndFeel;
using nucs.JsonSettings;
using nucs.JsonSettings.Fluent;
using DevExpress.XtraEditors;


namespace Azmongir
{
    static class Program
    {
       




        static SettingsBag Settings { get; } = JsonSettings.Construct<SettingsBag>(AppSetting.fileName + @"\config.json").EnableAutosave().LoadNow();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool instance = false;
            Mutex mutex = new Mutex(true, Application.ProductName, out instance);

            if(instance)
            {

                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fa");
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                BonusSkins.Register();
                SkinManager.EnableFormSkins();
                if (!System.IO.Directory.Exists(AppSetting.fileName))
                    System.IO.Directory.CreateDirectory(AppSetting.fileName);

               
                

                try
                {
                    UserLookAndFeel.Default.SetSkinStyle(Settings[AppSetting.SkinName].ToString() ?? "The Bezier");
                }
                catch (Exception)
                {


                }



                
                Application.Run(new Form1());
                mutex.ReleaseMutex();
            }
            else
            {
                XtraMessageBox.Show("برنامه در حال اجرا می باشد");
            }

        }
    }
}
