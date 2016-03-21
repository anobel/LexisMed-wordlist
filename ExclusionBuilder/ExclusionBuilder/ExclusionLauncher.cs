using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ExclusionBuilder
{
    class ExclusionLauncher
    {
        static void Main(string[] args)
        {
            if (!args.Any() || string.IsNullOrWhiteSpace(args[0]))
            {
                throw new ArgumentException("Enter a directory path for the .txt files that will be excluded");
            }

            var aggregator = new ExclusionAggregator(@"M:\dev\LexisMed\ExclusionBuilder");
            var stopwatch = Stopwatch.StartNew();
            var exclusions = aggregator.GetExcludedWords();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds:N0}ms elapsed");

            var filename = Path.Combine(args[0], "aggregate-exclusions.txt");
            File.WriteAllLines(filename, exclusions);
        }
    }
}
