using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CaptureUrlType
    {
        public static string UrlType()
        {
            Console.WriteLine("Please choose url type : \n" +
                "GetWellPlatform : Y | " +
                "GetWellPlatformDummy : N \n");

            string type = Console.ReadLine();

            return type;
        }
    }
}
