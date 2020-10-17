using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

public static class EmbeddedAssembly
{
    private static Dictionary<string, Assembly> _dic;

    public static Assembly Get(string assemblyFullName)
    {
        Assembly assembly;
        Assembly item;
        if ((EmbeddedAssembly._dic == null ? false : EmbeddedAssembly._dic.Count != 0))
        {
            string name = (new AssemblyName(assemblyFullName)).Name;
            if (EmbeddedAssembly._dic.ContainsKey(name))
            {
                item = EmbeddedAssembly._dic[name];
            }
            else
            {
                item = null;
            }
            assembly = item;
        }
        else
        {
            assembly = null;
        }
        return assembly;
    }

    public static void Load(string embeddedResource, string fileName)
    {
        byte[] numArray;
        Assembly assembly;
        bool flag;
        string str;
        if (EmbeddedAssembly._dic == null)
        {
            EmbeddedAssembly._dic = new Dictionary<string, Assembly>();
        }
        using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedResource))
        {
            if (manifestResourceStream != null)
            {
                numArray = new byte[(int)manifestResourceStream.Length];
                manifestResourceStream.Read(numArray, 0, (int)manifestResourceStream.Length);
                try
                {
                    assembly = Assembly.Load(numArray);
                    EmbeddedAssembly._dic.Add(assembly.GetName().Name, assembly);
                    return;
                }
                catch
                {
                }
            }
            else
            {
                return;
            }
        }
        using (SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider())
        {
            string str1 = BitConverter.ToString(sHA1CryptoServiceProvider.ComputeHash(numArray)).Replace("-", string.Empty);
            str = string.Concat(Path.GetTempPath(), fileName);
            if (!File.Exists(str))
            {
                flag = false;
            }
            else
            {
                byte[] numArray1 = File.ReadAllBytes(str);
                string str2 = BitConverter.ToString(sHA1CryptoServiceProvider.ComputeHash(numArray1)).Replace("-", string.Empty);
                flag = str1 == str2;
            }
        }
        if (!flag)
        {
            File.WriteAllBytes(str, numArray);
        }
        assembly = Assembly.LoadFile(str);
        EmbeddedAssembly._dic.Add(assembly.GetName().Name, assembly);
    }
}