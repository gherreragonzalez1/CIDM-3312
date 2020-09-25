using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LINQDataSources;

namespace client
{
    class Program
    {
        //Query 1: Create and show a LINQ Query that lists all Sepal Widths that are above the average Sepal Width.
        static string QueryOne(IEnumerable<IrisRecord> records) {
            
            string answer = "";

            if(records != null) {

                var sepalWidthAverage = records.Average(record => record.SepalWidth);
                var firstQuery =
                    from record in records
                    where record.SepalWidth > sepalWidthAverage
                    orderby record.SepalWidth
                    select record;

                answer = $"Sepal Width average is {sepalWidthAverage}";
                PrintRecords($"Records that are above the Sepal Width average {sepalWidthAverage}:", firstQuery);
            }

            return answer;

        }

        //Query 2: Create and show a LINQ Query that lists all Sepal Lengths that are below the average Sepal Length
        static string QueryTwo(IEnumerable<IrisRecord> records) {
            
            string answer = "";

            if(records != null) {

                var sepalLengthAverage = records.Average(record => record.SepalLength);
                var secondQuery =
                    from record in records
                    where record.SepalLength > sepalLengthAverage
                    orderby record.SepalLength
                    select record;

                answer = $"Sepal Length average is {sepalLengthAverage}";
                PrintRecords($"Sepal Length records above the average {sepalLengthAverage}:", secondQuery);
            }

            return answer;
        }

        //Query 3: Create and show a LINQ Query that indicates which class of iris has the lowest average Petal Width
        static string QueryThree(IEnumerable<IrisRecord> records) {

            string answer = "";

            if(records != null) {

                //Iris-setosa
                var setosaAvg = (Name: "Iris-setosa", Avg: 0d);
                setosaAvg.Avg = records.Where(r => r.IrisClassificationName == setosaAvg.Name)
                                       .Average<IrisRecord>(r => r.PetalWidth);

                //Iris-versicolor
                var versicolorAvg = (Name: "Iris-versicolor", Avg: 0d);
                versicolorAvg.Avg = records.Where(r => r.IrisClassificationName == versicolorAvg.Name)
                                           .Average(r => r.PetalWidth);

                //Iris-virginica
                var virginicaAvg = (Name: "Iris-virginica", Avg: 0d);
                virginicaAvg.Avg = records.Where(r => r.IrisClassificationName == virginicaAvg.Name)
                                          .Average(r => r.PetalWidth);

                List<(string, double)> avgs = new List<(string, double)>() {
                    setosaAvg,
                    versicolorAvg,
                    virginicaAvg
                };

                // foreach (var avg in avgs) {
                //      Console.WriteLine($"{avg}");
                // }

                var classWithMinAvg = avgs.Min();
                var petalWidthMinAvg = avgs.Min(a => a.Item2);

                answer = $"Lowest avg of Petal Width: {petalWidthMinAvg} \nClass with the lowest avg of Petal Width: {classWithMinAvg}";

            }

            return answer;

        }

        //Query 4: Create and show a LINQ Query that indicates which class of iris has the highest average Petal Length

        static string QueryFour(IEnumerable<IrisRecord> records) {

            string answer = "";

            if(records != null) {

                //Iris-setosa
                var setosaAvg = (Name: "Iris-setosa", Avg: 0d);
                setosaAvg.Avg = records.Where(r => r.IrisClassificationName == setosaAvg.Name)
                                       .Average(r => r.PetalLength);

                //Iris-versicolor
                var versicolorAvg = (Name: "Iris-versicolor", Avg: 0d);
                versicolorAvg.Avg = records.Where(r => r.IrisClassificationName == versicolorAvg.Name)
                                           .Average(r => r.PetalLength);

                //Iris-virginica
                var virginicaAvg = (Name: "Iris-virginica", Avg: 0d);
                virginicaAvg.Avg = records.Where(r => r.IrisClassificationName == virginicaAvg.Name)
                                          .Average(r => r.PetalLength);

                List<(string, double)> avgs = new List<(string, double)>() {
                    setosaAvg,
                    versicolorAvg,
                    virginicaAvg
                };

                // foreach (var avg in avgs) {
                //      Console.WriteLine($"{avg}");
                // }

                var classWithMaxAvg = avgs.Max();
                var petalLengthMaxAvg = avgs.Max(a => a.Item2);

                answer = $"Highest avg of Petal Length: {petalLengthMaxAvg} \nClass of iris with the highest avg Petal Length: {classWithMaxAvg}";
            }
            
            return answer;
        }

        // Query 5: Create and show a LINQ Query that indicates the widest Sepal Width for each class of iris
        static string QueryFive(IEnumerable<IrisRecord> records) {

            string answer = "";

            if(records != null) {

                //Iris-setosa
                var setosaWidth = (Name: "Iris-setosa", Width: 0d);
                setosaWidth.Width = records.Where(r => r.IrisClassificationName == setosaWidth.Name)
                                           .Max(r => r.SepalWidth);

                //Iris-versicolor
                var versicolorWidth = (Name: "Iris-versicolor", Width: 0d);
                versicolorWidth.Width = records.Where(r => r.IrisClassificationName == versicolorWidth.Name)
                                               .Max(r => r.SepalWidth);

                //Iris-virginica
                var virginicaWidth = (Name: "Iris-virginica", Width: 0d);
                virginicaWidth.Width = records.Where(r => r.IrisClassificationName == virginicaWidth.Name)
                                              .Max(r => r.SepalWidth);

                List<(string, double)> widths = new List<(string, double)>() {
                    setosaWidth,
                    versicolorWidth,
                    virginicaWidth
                };

                foreach (var width in widths) {
                     Console.WriteLine($"{width}");
                }

            }

            return answer;
        }

        // Query 6: Create and show a LINQ Query that indicates the shortest Sepal Width for each class of iris
        static string QuerySix(IEnumerable<IrisRecord> records) {

            string answer = "";

            if(records != null) {

                //Iris-setosa
                var setosaWidth = (Name: "Iris-setosa", Width: 0d);
                setosaWidth.Width = records.Where(r => r.IrisClassificationName == setosaWidth.Name)
                                           .Min(r => r.SepalWidth);

                //Iris-versicolor
                var versicolorWidth = (Name: "Iris-versicolor", Width: 0d);
                versicolorWidth.Width = records.Where(r => r.IrisClassificationName == versicolorWidth.Name)
                                               .Min(r => r.SepalWidth);

                //Iris-virginica
                var virginicaWidth = (Name: "Iris-virginica", Width: 0d);
                virginicaWidth.Width = records.Where(r => r.IrisClassificationName == virginicaWidth.Name)
                                              .Min(r => r.SepalWidth);

                List<(string, double)> widths = new List<(string, double)>() {
                    setosaWidth,
                    versicolorWidth,
                    virginicaWidth
                };

                foreach (var width in widths) {
                     Console.WriteLine($"{width}");
                }

            }

            return answer;
        }
        
        //Query 7: Create and show a LINQ Query that indicates the ranks the top 5 widest Petal Widths
        static string QuerySeven(IEnumerable<IrisRecord> records) {

            string answer = "";

            if(records != null)  {

            var widestPetalWidth = records.Max(record => record.PetalWidth);
            var seventhQuery =
                (from record in records
                where record.PetalWidth <= widestPetalWidth
                orderby record.PetalWidth descending
                select record).Take(5);

            PrintRecords($"Widest Petal Width: {widestPetalWidth}", seventhQuery);
            }

            return answer;

        }

        //Query 8: Create and show a LINQ Query that indicates the ranks the 5 shortest Petal Lengths
        static string QueryEight(IEnumerable<IrisRecord> records) {

            string answer = "";

            if(records != null)  {

            var shortestPetalLength = records.Min(record => record.PetalLength);
            var eighthQuery =
                (from record in records
                where record.PetalLength >= shortestPetalLength
                orderby record.PetalLength ascending
                select record).Take(5);

            PrintRecords($"Shortest Petal Length: {shortestPetalLength}", eighthQuery);

            }

            return answer;
        }


        //Query 9: Create and show a LINQ Query that indicates the median Sepal Width for each class of iris
        static string QueryNine(IEnumerable<IrisRecord> records) {

            string answer = "";

            if(records != null)  {



            }

            return answer;

        }

        static void Main(string[] args)
        {

            IEnumerable<IrisRecord> records = LoadIrisData();

            // Console.WriteLine(QueryOne(records));

            // Console.WriteLine(QueryTwo(records));

            // Console.WriteLine(QueryThree(records));

            // Console.WriteLine(QueryFour(records));

            // Console.WriteLine(QueryFive(records));

            // Console.WriteLine(QuerySix(records));

            // Console.WriteLine(QuerySeven(records));

            // Console.WriteLine(QueryEight(records));

            // Console.WriteLine(QueryNine(records));

            // Console.WriteLine(QueryTen(records));

            //Query 9: Create and show a LINQ Query that indicates the median Sepal Width for each class of iris
            // var ninthQuery = 
            //     (from record in records
            //     where record.SepalWidth > 0
            //     orderby record.SepalWidth
            //     select record).GroupBy(record => record.IrisClassificationName).Select(record => record.First());
                
            // PrintRecords($"Medians for each iris class:", ninthQuery);
                

            //Query 10: Create and show a LINQ Query that indicates the mode Petal Length for each class of iris
            // var tenthQuery = 
            //     (from record in records
            //     where record.PetalLength > 0
            //     orderby record.PetalLength descending
            //     select record).GroupBy(record => record.IrisClassificationName).Select(record => record.First());
            //     var mode = tenthQuery.GroupBy(v => v)
            //                          .OrderByDescending(g => g.Count())
            //                          .First()
            //                          .Select(record => record.PetalLength)
            //                          .First();
                
            // PrintRecords($"Mode for each iris class: {mode}", tenthQuery);
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