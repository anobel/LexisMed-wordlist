using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExclusionBuilder
{
    public class ExclusionAggregator
    {
        private readonly string _directoryPath;
        public ExclusionAggregator(string directoryPath)
        {
            _directoryPath = Path.Combine(Environment.ExpandEnvironmentVariables(directoryPath));
        }

        private readonly string[] _tokens = {Environment.NewLine, " ", ",", ";", ".", "?", "!", "/", "-", "("};

        public HashSet<string> GetExcludedWords()
        {
            var relevantFiles =
                Directory.EnumerateFiles(_directoryPath, "*.txt", SearchOption.TopDirectoryOnly)
                .Where(d => Path.GetFileName(d) != "aggregate-exclusions.txt");

            var words = relevantFiles
                .AsParallel()
                .Select(f => File.ReadAllText(f))
                .Select(text => text.Replace(")", string.Empty))    //Allows (O')Byrne notation to become O'Byrne when coupled with token symbols
                .SelectMany(text => text.Split(_tokens, StringSplitOptions.RemoveEmptyEntries))
                .Where(word => !string.IsNullOrWhiteSpace(word))
                .Select(word => word.Trim());

            return new HashSet<string>(words, StringComparer.OrdinalIgnoreCase);
        }
    }
}