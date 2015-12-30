using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ListParser
{
    class Program
    {
        static void Main()
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\all-unicode.dic"));
            var unsorted = File.ReadAllLines(path).ToList();
            var set = new HashSet<string>(unsorted);
            var sorted = set.ToList();
            sorted.Sort();

            for (var i = 0; i < unsorted.Count; i++)
            {
                if (!unsorted[i].Equals(sorted[i], StringComparison.Ordinal))
                {
                    Console.WriteLine(i + " sorted: " + sorted[i] + " unsorted: " + unsorted[i]);
                }
            }

            Console.WriteLine(set.Count);
            Console.ReadLine();
        }
    }
}
