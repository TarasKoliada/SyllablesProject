using Core.Models;
using Sklady.Models;
using System;
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

            foreach (var resItem in results)
            {
                var fileStatistics = GenerateStatistics(resItem);
                filesStatistics.Add(fileStatistics);

                sb.AppendLine(String.Format("{0}\t{1}", resItem.FileName, String.Join(_separator, fileStatistics)));
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
            var CandVSums = GetCVCounts(fileResult);           

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
                CVVSyllablesStatistics = CVVSyllablesStatistics.Select(r => (double)r / fileResult.SyllablesCount).ToList();
                LettersStatistics = LettersStatistics.Select(r => (double)r / fileResult.TextLength).ToList();
                RepetitionsStatistics = RepetitionsStatistics.Select(r => (double)r / fileResult.TextLength).ToList();
            }

            res.AddRange(CandVSums);
            res.AddRange(CVVSyllablesStatistics);
            res.AddRange(RepetitionsStatistics);
            res.AddRange(LettersStatistics);

            res.Insert(0, fileResult.SyllablesCount);
            res.Insert(0, fileResult.TextLength);            

            return res;
        }       

        private List<double> GetCVCounts(FileProcessingResult fileResult)
        {
            var CCount = 0.0;
            var VCount = 0.0;
            var openSyllables = 0.0;
            var closedSyllables = 0.0;            

            foreach(var item in fileResult.ReadableResults)
            {
                for (var i = 0; i < item.Syllables.Length; i++)
                {
                    var HalfCharsCount = item.Syllables[i].Count(c => c == 'Y' || c == 'u');

                    CCount += item.Syllables[i].Count(c => _charactersTable.isConsonant(c)); // as we're counting Y as 0.5V 
                    VCount += item.Syllables[i].Count(c => !_charactersTable.isConsonant(c)); // and 0.5C we have to make corresponding calculations                   

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

            if (!_useAbsoluteValues)
            {
                CCount = CCount / fileResult.TextLength;
                VCount = VCount / fileResult.TextLength;
            }

            var CtoV = CCount / VCount;

            openSyllables = openSyllables / fileResult.SyllablesCount;
            closedSyllables = closedSyllables / fileResult.SyllablesCount;
            var openToClosed = openSyllables / closedSyllables;

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
            var cvvSet = new SortedSet<string>();

            foreach (var item in results)
            {
                cvvSet.UnionWith(item.CvvStatistics.Select(c => c.Key));
            }

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
