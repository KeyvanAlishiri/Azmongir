using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nucs.JsonSettings;
using nucs.JsonSettings.Fluent;

namespace Azmongir
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SettingsBag settings { get; } = JsonSettings.Construct<SettingsBag>(AppSetting.fileName+ @"\config.json").EnableAutosave().LoadNow();
        public Form1()
        {
            InitializeComponent();
            AppSetting.AddOrUpdate("LogifyApi", "123456789");


            settings["logifyapi"] = "123456789";
            settings["logifyint"] = 1234;
            settings["logifybool"] = true;

            Console.WriteLine(settings["logifyapi"]);

        }
    }
}
