using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace UnpackAssembly
{
    class Main
    {
        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly assembly;
            string[] fields = args.Name.Split(new char[] { ',' });
            string name = fields[0];
            string culture = fields[2];
            if ((!name.EndsWith(".resources") ? true : culture.EndsWith("neutral")))
            {
                assembly = EmbeddedAssembly.Get(args.Name);
            }
            else
            {
                assembly = null;
            }
            return assembly;
        }


        public static void Unpack()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Main.OnResolveAssembly);
            string[] manifestResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            for (int i = 0; i < (int)manifestResourceNames.Length; i++)
            {
                string str = manifestResourceNames[i];
                if ((str.ToLower().EndsWith(".resources") ? false : str.ToLower().EndsWith(".dll")))
                {
                    EmbeddedAssembly.Load(str, str);
                }
            }
        }
    }
}
