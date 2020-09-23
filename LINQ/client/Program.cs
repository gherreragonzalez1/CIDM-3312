using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using LINQDataSources;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {

            IEnumerable<IrisRecord> records = LoadIrisData();
            IEnumerable<IrisRecord> filtered;

            if(records != null)
            {
                float boundary = 4.0f;
                var otherfilter = 
                    from record in records
                    where record.PetalLength < boundary
                    select record;
                PrintRecords($"Petal Length at or below {boundary}", otherfilter);  

                var sepalWidthAverage = records.Average(record => record.SepalWidth);

            }
        }

        static void PrintRecords(string message, IEnumerable<Object> records)
        {
            // simplest query shows all records
            Console.WriteLine(message);
            foreach(Object record in records)
            {
                Console.WriteLine(record);
            }
        }

        static IEnumerable<IrisRecord> LoadIrisData()
        {
            // this is somewhat "brittle" code as it only works when the project is
            // run within the client folder.
            Console.WriteLine($@"{Directory.GetCurrentDirectory()}\data\iris.data");
            FileInfo file = new FileInfo($@"{Directory.GetCurrentDirectory()}\data\iris.data");
            Console.WriteLine(file.FullName);
            
            IEnumerable<IrisRecord> records = null;

            try
            {
                records = IrisDataSourceHelper.GetIrisRecordsFromFlatFile(file.FullName);
            }catch (Exception exp)
            {
                Console.Error.WriteLine(exp.StackTrace);
            }
            return records;
        }
    }
}