using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using nucs.JsonSettings;
using nucs.JsonSettings.Fluent;

namespace Azmongir
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
       public SettingsBag settings { get; } = JsonSettings.Construct<SettingsBag>(AppSetting.fileName+ @"\config.json").EnableAutosave().LoadNow();
        public Form1()
        {
            InitializeComponent();
            AppSetting.AddOrUpdate("LogifyApi", "123456789");


            settings["logifyapi"] = "123456789";
            settings["logifyint"] = 1234;
            settings["logifybool"] = true;

            Console.WriteLine(settings["logifyapi"]);

            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(skinRibbonGalleryBarItem1, true, true);
            skinRibbonGalleryBarItem1.GalleryItemClick += new GalleryItemClickEventHandler(skinRibbonGalleryBarItem1_GalleryItemClick);
            if(settings[AppSetting.SkinName] != "0")
            {
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(settings[AppSetting.SkinName].ToString());
            }
        }

        private void skinRibbonGalleryBarItem1_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            settings[AppSetting.SkinName] = DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName;
        }
    }
}
