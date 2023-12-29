using Core.Models;
using Sklady.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklady.Export
{
    public class StatisticsTableGenerator
    {
        private const string _separator = "\t";
        private List<string> _cvvHeaders;
        private List<string> _repetitionsHeaders;
        private List<char> _lettersHeaders;

        private StatisticsCalculator _statisticsCalculator;
        private bool _useAbsoluteValues = false;
        private CharactersTable _charactersTable;

        public StatisticsTableGenerator(bool useAbsoluteMeasures = false)
        {
            _useAbsoluteValues = useAbsoluteMeasures;
            _charactersTable = new CharactersTable(Table.Table1);
        }

        public string GetTableString(List<FileProcessingResult> results)
        {
            var sb = new StringBuilder();   
            ProcessCVVHeaders(results);
            ProcessRepetitionsHeaders(results);
            ProcessLettersHeaders(results);

            var headerItems = GenerateTableHeader();
            sb.AppendLine(String.Join(_separator, headerItems));

            var filesStatistics = new List<List<double>>();

            var allFileStatistics = new List<double>[results.Count];
            Parallel.For(0, results.Count, i =>
            {
                var fileStatistics = GenerateStatistics(results[i]);
                filesStatistics.Add(fileStatistics);
                allFileStatistics[i] = fileStatistics;
            });

            for (var i = 0; i < results.Count; i++)
            {
                sb.AppendLine(String.Format("{0}\t{1}", results[i].FileName, String.Join(_separator, allFileStatistics[i])));
            }

            var groupedStatistics = GroupByMeasure(filesStatistics);
            _statisticsCalculator = new StatisticsCalculator(groupedStatistics[0]); // at 0 position we have list of file lengths

            var weightedAvg = groupedStatistics.Select(c => String.Format("{0}", _statisticsCalculator.GetAvarage(c)));
            sb.AppendLine(String.Format("{0}\t{1}", "Average", String.Join(_separator, weightedAvg)));

            var avg = groupedStatistics.Select(c => String.Format("{0}", _statisticsCalculator.GetWeightedAvarage(c)));
            sb.AppendLine(String.Format("{0}\t{1}", "Weighted Average", String.Join(_separator, avg)));

            var weightedDelta = groupedStatistics.Select(c => String.Format("{0}", _statisticsCalculator.GetWeightedDelta(c)));
            sb.AppendLine(String.Format("{0}\t{1}", "Avg Square Weighted Delta", String.Join(_separator, weightedDelta)));

            var delta = groupedStatistics.Select(c => String.Format("{0}", _statisticsCalculator.GetDelta(c)));
            sb.AppendLine(String.Format("{0}\t{1}", "Avg Square Delta", String.Join(_separator, delta)));

            return sb.ToString();
        }       

        private List<List<double>> GroupByMeasure(List<List<double>> fileStatistics)
        {
            var count = fileStatistics.First().Count; // all list should have the same count
            var res = new List<List<double>>();

            for (var i = 0; i < count; i++)
            {
                res.Add(new List<double>());
            }

            for (var i = 0; i < count; i++)
            {                
                for (var j = 0; j < fileStatistics.Count; j++)
                {
                    res[i].Add(fileStatistics[j][i]);
                }
            }

            return res;
        }

        private List<double> GenerateStatistics(FileProcessingResult fileResult)
        {
            var res = new List<double>();

            var CVVSyllablesStatistics = new List<double>();
            var RepetitionsStatistics = new List<double>();
            var LettersStatistics = new List<double>();
            var CandVSums = fileResult.CandVSums;       

            foreach (var header in _cvvHeaders)
            {
                if (fileResult.CvvStatistics.ContainsKey(header))
                {
                    CVVSyllablesStatistics.Add(fileResult.CvvStatistics[header]);
                }
                else
                {
                    CVVSyllablesStatistics.Add(0);
                }
            }

            foreach (var header in _repetitionsHeaders)
            {
                if (fileResult.Repetitions.ContainsKey(header))
                {
                    RepetitionsStatistics.Add(fileResult.Repetitions[header]);
                }
                else
                {
                    RepetitionsStatistics.Add(0);
                }
            }

            foreach (var header in _lettersHeaders)
            {
                if (fileResult.Letters.ContainsKey(header))
                {
                    LettersStatistics.Add(fileResult.Letters[header]);
                }
                else
                {
                    LettersStatistics.Add(0);
                }
            }

            if (!_useAbsoluteValues)
            {
                Parallel.Invoke(
                    () => CVVSyllablesStatistics = CVVSyllablesStatistics.Select(r => (double)r / fileResult.SyllablesCount).ToList(),
                    () => LettersStatistics = LettersStatistics.Select(r => (double)r / fileResult.TextLength).ToList(),
                    () => RepetitionsStatistics = RepetitionsStatistics.Select(r => (double)r / fileResult.TextLength).ToList()
                );
            }

            res.AddRange(CandVSums);
            res.AddRange(CVVSyllablesStatistics);
            res.AddRange(RepetitionsStatistics);
            res.AddRange(LettersStatistics);

            res.Insert(0, fileResult.SyllablesCount);
            res.Insert(0, fileResult.TextLength);            

            return res;
        }         

        public List<double> GetCVCountsParallel(FileProcessingResult fileResult)
        {
            var CCount = 0.0;
            var VCount = 0.0;
            var openSyllables = 0.0;
            var closedSyllables = 0.0;

            // remove all syllables null
            fileResult.ReadableResults.RemoveAll(c => c.Syllables == null);

            // Using Parallel.ForEach for parallel processing
            Parallel.ForEach(fileResult.ReadableResults, item =>
            {
                try
                {
                    for (var i = 0; i < item.Syllables.Length; i++)
                    {
                        var HalfCharsCount = item.Syllables[i].Count(c => c == 'Y' || c == 'u');

                        CCount += item.Syllables[i].Count(c => _charactersTable.isConsonant(c));
                        VCount += item.Syllables[i].Count(c => !_charactersTable.isConsonant(c));

                        if (_charactersTable.isConsonant(item.Syllables[i].Last()))
                        {
                            closedSyllables++;
                        }
                        else
                        {
                            openSyllables++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            if (!_useAbsoluteValues)
            {
                CCount /= fileResult.TextLength;
                VCount /= fileResult.TextLength;
            }

            var CtoV = CCount / VCount;

            openSyllables /= fileResult.SyllablesCount;
            closedSyllables /= fileResult.SyllablesCount;
            var openToClosed = openSyllables / closedSyllables;

            //return [CCount, VCount, CtoV, openSyllables, closedSyllables, openToClosed];
            return new List<double>() { CCount, VCount, CtoV, openSyllables, closedSyllables, openToClosed };
        }

        private List<string> GenerateTableHeader()
        {
            var res = new List<string>();
            res.AddRange(new string[] { "Text", "Length", "SyllablesCount", "Total C", "Total V", "C/V", "Opened", "Closed", "Opened/Closed" });
            res.AddRange(_cvvHeaders);
            res.AddRange(_repetitionsHeaders);
            res.AddRange(_lettersHeaders.Select(c => c.ToString()));

            return res;
        }

        private void ProcessCVVHeaders(List<FileProcessingResult> results)
        {
            var cvvSet = new ConcurrentBag<string>();

            Parallel.ForEach(results, item =>
            {
                var cvvs = item.CvvStatistics.Select(c => c.Key);
        
                lock (cvvSet)
                {
                    foreach (var cvv in cvvs)
                    {
                        cvvSet.Add(cvv);
                    }
                }
            });

            _cvvHeaders = cvvSet.ToList();
        }

        private void ProcessRepetitionsHeaders(List<FileProcessingResult> results)
        {
            var repetitions = new SortedSet<string>();

            foreach (var item in results)
            {
                repetitions.UnionWith(item.Repetitions.Select(c => c.Key));
            }

            _repetitionsHeaders = repetitions.ToList();
        }

        private void ProcessLettersHeaders(List<FileProcessingResult> results)
        {
            var letters = new SortedSet<char>();

            foreach (var item in results)
            {
                letters.UnionWith(item.Letters.Select(c => c.Key));
            }

            _lettersHeaders = letters.ToList();
        }
    }
}
