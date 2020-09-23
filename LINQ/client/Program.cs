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
                //print all
                // PrintRecords("unfiltered", records);

                // float boundary = 4.0f;
                // var otherfilter = 
                //     from record in records
                //     where record.PetalLength < boundary
                //     select record;
                // PrintRecords($"Petal Length at or below {boundary}", otherfilter);  

                //Query 1: Create and show a LINQ Query that lists all Sepal Widths that are above the average Sepal Width.
                // var sepalWidthAverage = records.Average(record => record.SepalWidth);
                // var firstQuery =
                //     from record in records
                //     where record.SepalWidth > sepalWidthAverage
                //     orderby record.SepalWidth
                //     select record;
                // PrintRecords($"Sepal Width above the average {sepalWidthAverage}", firstQuery);

                //Query 2: Create and show a LINQ Query that lists all Sepal Lengths that are below the average Sepal Length
                // var sepalLengthAverage = records.Average(record => record.SepalLength);
                // var secondQuery =
                //     from record in records
                //     where record.SepalLength > sepalLengthAverage
                //     orderby record.SepalLength
                //     select record;
                // PrintRecords($"Sepal Length above the average {sepalLengthAverage}", secondQuery);

                //Query 3: Create and show a LINQ Query that indicates which class of iris has the lowest average Petal Width
                // var petalWidthAverage = records.Average(record => record.PetalWidth);
                // var petalWidthLowest = records.Min(record => record.PetalWidth);
                // var thirdQuery =
                //     from record in records
                //     where record.PetalWidth < petalWidthAverage && record.PetalWidth == petalWidthLowest
                //     orderby record.IrisClassificationName
                //     select record;
                // PrintRecords($"Lowest average Petal Width {petalWidthLowest}", thirdQuery);

                //Query 4: Create and show a LINQ Query that indicates which class of iris has the highest average Petal Length
                //    var petalLengthAverage = records.Average(record => record.PetalLength);
                //    var fourthQuery =
                //         (from record in records
                //         where record.PetalLength > petalLengthAverage
                //         orderby record.PetalLength descending
                //         select record).Take(1);
                //     PrintRecords($"Average Petal Length: {petalLengthAverage}", fourthQuery);
                //     var fourthQuery = records.Where(record => record.PetalLength > petalLengthAverage).OrderByDescending(record => record.PetalLength).Take(1);
                //     PrintRecords($"Average Petal Length: {petalLengthAverage}", fourthQuery);

                //Query 5: Create and show a LINQ Query that indicates the widest Sepal Width for each class of iris
                    // var widestSepalWidth = records.Max(record => record.SepalWidth);
                    // var fifthQuery = records.GroupBy(record => record.IrisClassificationName)
                    //                         .SelectMany(y=>y.Where(z=>z.SepalWidth == y.Max(i=>i.SepalWidth)));
                    // PrintRecords($"Widest Sepal Width: {widestSepalWidth}", fifthQuery);

                //Query 6: Create and show a LINQ Query that indiciates the shortest Sepal Width for each class of iris
                    var shortestSepalWidth = records.Min(record => record.SepalWidth);
                    var sixthQuery = records.GroupBy(record => record.IrisClassificationName)
                                            .SelectMany(y=>y.Where(z=>z.SepalWidth == y.Min(i=>i.SepalWidth)));
                    PrintRecords($"Shortest Sepal Width: {shortestSepalWidth}", sixthQuery);

                //Query 7: Create and show a LINQ Query that indicates the ranks the top 5 widest Petal Widths
                    // var widestPetalWidth = records.Max(record => record.PetalWidth);
                    // var seventhQuery =
                    //     (from record in records
                    //     where record.PetalWidth <= widestPetalWidth
                    //     orderby record.PetalWidth descending
                    //     select record).Take(5);
                    // PrintRecords($"Widest Petal Width: {widestPetalWidth}", seventhQuery);

                //Query 8: Create and show a LINQ Query that indicates the ranks the botton 5 shortest Petal Lengths
                    // var shortestPetalLength = records.Min(record => record.PetalLength);
                    // var eighthQuery =
                    //     (from record in records
                    //     where record.PetalLength >= shortestPetalLength
                    //     orderby record.PetalLength ascending
                    //     select record).Take(5);
                    // PrintRecords($"Shortest Petal Length: {shortestPetalLength}", eighthQuery);

                //Query 9: Create and show a LINQ Query that indicates the median Sepal Width for each class of iris
                    // var ninthQuery =
                    //     from record in records
                    //     orderby record
                    //     select record;

                    
                //Query 10: Create and show a LINQ Query that indicates the mode Petal Length for each class of iris

            } 
        }

        static void PrintRecords(string message, IEnumerable<IrisRecord> records)
        {
            // simplest query shows all records
            Console.WriteLine(message);
            foreach(IrisRecord record in records)
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