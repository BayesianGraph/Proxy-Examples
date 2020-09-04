using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace DRIECT_Proxy.Helpers
{
    public class FileReader
    {
        public static List<string> ReadFormFile(string root)
        {
            List<string> uri = new List<string>();
            

            string filename = @$"{root}\lib\Data\DIRECT.txt";

            System.IO.StreamReader file =  new System.IO.StreamReader(@filename);
            string line;



            List<string> filedata = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                uri.Add(line);             
                
            }

            return uri;

        }

      
    }
}
