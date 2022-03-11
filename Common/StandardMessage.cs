using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class StandardMessage
    {
        public static void StartMessage()
        {
            Console.WriteLine("Login Process .... \n");
        }

        public static void SuccessLoginMessage(string token)
        {
            Console.WriteLine($"Success Login ! Token : {token} \n");
        }

        public static void ErrorLoginMessage(string message)
        {
            Console.WriteLine($"Error call login api : {message} \n");
        }

        public static void FetchMessage()
        {
            Console.WriteLine("Fetch data from api ... \n");
        }

        public static void FetchSuccessMessage()
        {
            Console.WriteLine("Succeed fetch data from api ! \n");
        }

        public static void FetchErrorMessage(string message)
        {
            Console.WriteLine($"Failed fetch data from api : {message} \n");
        }

        public static void DataExistMessage()
        {
            Console.WriteLine("Table not empty, so just update the data \n");
        }

        public static void DataNotExistMessage()
        {
            Console.WriteLine("Table empty, so need add data \n");
        }

        public static void RowAffectedMessage(int p, int w, string exeType)
        {
            Console.WriteLine($"Success {exeType} data to both table : \n" +
                $"Platform table row affected : {p} \n" +
                $"Well table row affected : {w} \n");
        }

        public static void InsertErrorMessage(string message)
        {
            Console.WriteLine($"Failed insert data to table : {message} \n");
        }

        public static void ListErrorMessage()
        {
            Console.WriteLine("List empty");
        }
    }
}
