using System;
using System.Linq;

namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };
            // You can convert string to double by
            // double.Parse(str)

            int stdCount = data.GetLength(0) - 1;
            // ---------- TODO ----------
            double mathSum = 0;
            double mathAver = 0;

            double scienceSum = 0;
            double scienceAver = 0;

            double englishSum = 0;
            double englishAver = 0;

            for (int i = 1; i <= stdCount; i++)
            {
                mathSum += double.Parse(data[i, 2]);
                scienceSum += double.Parse(data[i, 3]);
                englishSum += double.Parse(data[i, 4]);
            }

            mathAver = mathSum / stdCount;
            scienceAver = scienceSum / stdCount;
            englishAver = englishSum / stdCount;

            Console.WriteLine("Average  Scores:");
            Console.WriteLine($"Math: {mathAver:N2} \nScience: {scienceAver:N2} \nEnglish: {englishAver:N2}\n");
            Console.WriteLine("Max and min Scores:");

            //math
            double min = double.Parse(data[1, 2]);
            double max = double.Parse(data[1, 2]);
            for (int i = 2; i <= stdCount; i++)
            {
                min = (min > double.Parse(data[i, 2])) ? double.Parse(data[i, 2]) : min;
                max = (max < double.Parse(data[i, 2])) ? double.Parse(data[i, 2]) : max;
            }
            Console.WriteLine($"Math: ({max},{min})");

            //Science
            min = double.Parse(data[1, 3]);
            max = double.Parse(data[1, 3]);
            for (int i = 2; i <= stdCount; i++)
            {
                min = (min > double.Parse(data[i, 3])) ? double.Parse(data[i, 3]) : min;
                max = (max < double.Parse(data[i, 3])) ? double.Parse(data[i, 3]) : max;
            }
            Console.WriteLine($"Science: ({max},{min})");

            //English
            min = double.Parse(data[1, 4]);
            max = double.Parse(data[1, 4]);
            for (int i = 2; i <= stdCount; i++)
            {
                min = (min > double.Parse(data[i, 4])) ? double.Parse(data[i, 4]) : min;
                max = (max < double.Parse(data[i, 4])) ? double.Parse(data[i, 4]) : max;
            }
            Console.WriteLine($"English: ({max},{min})\n");

            double AliceScore = 0;
            double BobScore = 0;
            double CharlieScore = 0;
            double DavidScore = 0;
            double EveScore = 0;

            for (int i = 2; i < stdCount; i++)
            {
                AliceScore += double.Parse(data[1, i]);
                BobScore += double.Parse(data[2, i]);
                CharlieScore += double.Parse(data[3, i]);
                DavidScore += double.Parse(data[4, i]);
                EveScore += double.Parse(data[5, i]);
            }
            double[] allScores = { AliceScore, BobScore, CharlieScore, DavidScore, EveScore };

            double[] stdRanking = new double[stdCount];

            for (int i = 0; i < stdCount; i++)
            {
                stdRanking[i] = 1;
                for (int j = 0; j < stdCount; j++)
                {
                    if (allScores[i] < allScores[j])
                    {
                        stdRanking[i]++;
                    }
                }
            }
            Console.WriteLine("Students rank by total scores:");
            for (int i = 0; i < stdCount; i++)
            {
                if (stdRanking[i] == 1)
                {
                    Console.WriteLine($"{data[i + 1, 1]}: {stdRanking[i]}st");
                }
                else if (stdRanking[i] == 2)
                {
                    Console.WriteLine($"{data[i + 1, 1]}: {stdRanking[i]}nd");
                }
                else if (stdRanking[i] == 3)
                {
                    Console.WriteLine($"{data[i + 1, 1]}: {stdRanking[i]}rd");
                }
                else
                {
                    Console.WriteLine($"{data[i + 1, 1]}: {stdRanking[i]}th");
                }

            }
            // --------------------
        }
    }
}

/* example output

Average Scores: 
Math: 84.40
Science: 86.80
English: 86.20

Max and min Scores: 
Math: (94, 72)
Science: (95, 76)
English: (92, 78)

Students rank by total scores:
Alice: 4th
Bob: 1st
Charlie: 5th
David: 2nd
Eve: 3rd

*/
