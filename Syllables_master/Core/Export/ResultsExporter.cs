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
    public class ResultsExporter
    {
        private static ResultsExporter _instance;

        private CharactersTable _charsTable;
        private StatisticsTableGenerator _statisticsTableGenerator;

        public event Action<int, int> OnFileCvvItemCalculated;

        private Settings _settings;

        public ResultsExporter(Settings settings)
        {
            _settings = settings;
            _charsTable = settings.CharactersTable; 
            _statisticsTableGenerator = new StatisticsTableGenerator(settings.AbsoluteMeasures);
        }

        public StatisticsTableGenerator StatisticsTableGenerator
        {
            get
            {
                return _statisticsTableGenerator;
            }
        }
        
        public string GetFirstSyllables(List<AnalyzeResults> result)
        {          
            var sb = new StringBuilder();
            var res = result.Select(r => r).ToList();

            res = TakeOnlyFirstSyllable(result);

            for (var i = 0; i < res.Count; i++)
            {
                sb.Append(String.Join(_settings.SyllableSeparator, res[i].Syllables) + " ");
            }

            return sb.ToString();
        }

        public string GetSyllables(List<AnalyzeResults> result)
        {
            var sb = new StringBuilder();

            sb.Append(String.Join(" ", result.Select(r =>
            {
                if (r.Syllables == null || r.Word == null)
                {
                    return ""; // return an empty string or any other default value if r is null
                }
                else
                    return String.Join(_settings.SyllableSeparator, r.Syllables);
            })));

            return sb.ToString();
        }

        public (string syllables, string firstSyllable) GetSyllablesCVVUnified(List<AnalyzeResults> result)
        {
            var sbSyllables = new StringBuilder();
            var sbFirstSyllable = new StringBuilder();

            result = ConvertToCvv(result);

            for (var i = 0; i < result.Count; i++)
            {
                sbSyllables.Append(string.Join(_settings.SyllableSeparator, result[i].Syllables) + " ");
                sbFirstSyllable.Append(string.Join(_settings.SyllableSeparator, result[i].Syllables.First()) + " ");
            }

            return (sbSyllables.ToString(), sbFirstSyllable.ToString());
        }

        public string GetSyllablesFirstCVV(List<AnalyzeResults> result)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < result.Count; i++)
            {
                //sb.Append(String.Join(_settings.SyllableSeparator, [result[i].Syllables.First()]) + " ");
                sb.Append(String.Join(_settings.SyllableSeparator, result[i].Syllables.First()) + " ");
            }

            return sb.ToString();
        }

        private List<AnalyzeResults> TakeOnlyFirstSyllable(List<AnalyzeResults> anResults)
        {
            return anResults.Select(c => new AnalyzeResults()
            {
                Word = c.Word,
                //Syllables = [c.Syllables.First()]
                Syllables = new string[] { c.Syllables.First() }
            }).ToList();
        }        

        public List<AnalyzeResults> ConvertToCvvOld(List<AnalyzeResults> anResults)
        {
            var unionOpt = _charsTable.UnionOpt;

            Parallel.ForEach(anResults, resultitem =>
            {
                for (var i = 0; i < resultitem.Syllables.Length; i++)
                {
                    var modifiedSyllable = "";
                    for (var j = 0; j < resultitem.Syllables[i].Length; j++)
                    {
                        var character = resultitem.Syllables[i][j];
                        if (unionOpt.Any(c => c.CharacterValue == character))
                        {
                            modifiedSyllable += _charsTable.isConsonant(character) ? 'c' : 'v';
                        }
                    }

                    resultitem.Syllables[i] = modifiedSyllable;    
                }
            });

            return anResults;
        }

        public List<AnalyzeResults> ConvertToCvv(List<AnalyzeResults> anResults)
        {
            var unionOptSet = new HashSet<char>(_charsTable.UnionOpt.Select(c => c.CharacterValue));
            var itemsToRemove = new ConcurrentBag<AnalyzeResults>(); // Collect items to be removed

            Parallel.ForEach(anResults, resultitem =>
            {
                // Check if resultitem.Syllables is null
                if (resultitem.Syllables != null)
                {
                    for (var i = 0; i < resultitem.Syllables.Length; i++)
                    {
                        var modifiedSyllable = new StringBuilder();

                        foreach (var character in resultitem.Syllables[i])
                        {
                            if (unionOptSet.Contains(character))
                            {
                                modifiedSyllable.Append(_charsTable.isConsonant(character) ? 'c' : 'v');
                            }
                        }

                        resultitem.Syllables[i] = modifiedSyllable.ToString();
                    }
                }
                else
                {
                    itemsToRemove.Add(resultitem); // Add item to remove if resultitem.Syllables is null
                }
            });

            // Remove items marked for removal
            foreach (var itemToRemove in itemsToRemove)
            {
                anResults.Remove(itemToRemove);
            }

            return anResults;
        }

        public string GetTranscribedToUkrainianText(List<string> transcribedWords)
        {
            return string.Join(" ", transcribedWords);
        }

        public string GetStatisticsTableCsv(List<FileProcessingResult> fileProcessingResults)
        {
            return _statisticsTableGenerator.GetTableString(fileProcessingResults);           
        }       
    }
}
