using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace RecruitsLauncherManifestGenerator
{
    class ManifestGenerator
    {
        static void Main(string[] args)
        {
            String dir = @"C:\Program Files (x86)\Commotion Games\Recruits";

            /** Generates Manifest */
            String[] filePaths = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
            List<String> realPaths = new List<string>();
            foreach (String str in filePaths)
            {
                realPaths.Add(str.Replace(dir, @"http://recruitsgame.com/downloads/game/recruits").Replace("\\", "/"));
            }

            

            realPaths = FormatFilePaths(realPaths);

            for (int i = 0; i < realPaths.Count; i++ )
            {
                realPaths[i] = realPaths[i].Replace(@"http://recruitsgame.com/downloads/game/recruits", "").Replace("\\", "/");
            }
            realPaths.Insert(0, "http://recruitsgame.com/downloads/game/recruits");
            System.IO.File.WriteAllLines(@"D:\\Documents\\test.txt", realPaths.ToArray());
        }

        private static List<String> FormatFilePaths(List<String> filePaths)
        {
            List<String> files = new List<String>();

            for (Int32 fileNr = 0; fileNr < filePaths.Count; fileNr++)
            {
                Console.Clear();
                Console.WriteLine((int)((double)fileNr / filePaths.Count * 100) + "% Calculated");
                try
                {
                    HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(filePaths[fileNr]);
                    HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();

                    files.Add(filePaths[fileNr] + ";" + webResp.ContentLength);

                    webResp.Close();
                }
                catch (Exception) { }
            }

            return files;
        }
    }
}
        