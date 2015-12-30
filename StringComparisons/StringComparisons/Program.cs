using System;
using System.Globalization;
using System.Threading;

namespace StringComparisons
{
    internal class Program
    {
        static void Main()
        {
            const string capital = "Alstrom";
            const string umlaut = "Alström";

            Console.WriteLine("---------------Alstrom---------------");
            Console.WriteLine("Ordinal umlaut / non-umlaut equal: " + capital.Equals(umlaut, StringComparison.Ordinal));
            Console.WriteLine("Invariant culture umlaut / non-umlaut equal: " + capital.Equals(umlaut, StringComparison.InvariantCulture));
            Console.WriteLine("Current culture umlaut / non-umlaut equal: " + capital.Equals(umlaut, StringComparison.CurrentCulture));

            const string eszett = "Straße";
            const string doubleS = "Strasse";
            Console.WriteLine("---------------Strasse---------------");
            Console.WriteLine("Ordinal eszett / double-S equal: " + eszett.Equals(doubleS, StringComparison.Ordinal));
            Console.WriteLine("Invariant culture umlaut / non-umlaut equal: " + eszett.Equals(doubleS, StringComparison.InvariantCulture));
            Console.WriteLine("Current culture umlaut / non-umlaut equal: " + eszett.Equals(doubleS, StringComparison.CurrentCulture));

            const string oe = "Alstroem";
            Console.WriteLine("---------------umlaut / oe---------------");
            Console.WriteLine("Ordinal oe / umlaut equal: " + oe.Equals(umlaut, StringComparison.Ordinal));
            Console.WriteLine("Invariant culture umlaut / non-umlaut equal: " + oe.Equals(umlaut, StringComparison.InvariantCulture));
            Console.WriteLine("Current culture umlaut / non-umlaut equal: " + oe.Equals(umlaut, StringComparison.CurrentCulture));

            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Console.WriteLine("---------------umlaut / oe after CultureInfo change---------------");
            Console.WriteLine("Ordinal oe / umlaut equal: " + oe.Equals(umlaut, StringComparison.Ordinal));
            Console.WriteLine("Invariant culture umlaut / non-umlaut equal: " + oe.Equals(umlaut, StringComparison.InvariantCulture));
            Console.WriteLine("Current culture umlaut / non-umlaut equal: " + oe.Equals(umlaut, StringComparison.CurrentCulture));

            Console.ReadLine();
        }
    }
}
