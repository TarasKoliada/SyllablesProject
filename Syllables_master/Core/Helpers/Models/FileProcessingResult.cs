using Sklady.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklady.Models
{
    public class FileProcessingResult : IDisposable
    {
        private Dictionary<string, int> _cvvStatistics;
        private List<AnalyzeResults> _cvvResults;
        private List<double> _candVSums;
        private List<string> _transcribedToUkrainian;
        private int _syllablesCount = 0;

        private ResultsExporter exporter;

        public FileProcessingResult(ResultsExporter exporter)
        {
            ReadableResults = new List<AnalyzeResults>();
            CvvResults = new List<AnalyzeResults>();
            this.exporter = exporter;
            Repetitions = new Dictionary<string, int>();
            Letters = new Dictionary<char, int>();
            //TranscribedToUkrainianSpellingWords = new List<string>();
        }

        public void DisposeReadableResults()
        {
            _syllablesCount = SyllablesCount;
            foreach (var res in ReadableResults)
            {
                res.Dispose();
            }

            ReadableResults.Clear();
        }
        /*public void DisposeTranscribedresults()
        {
            _transcribedToUkrainian.Clear();
            _transcribedToUkrainian = null;

        }*/
        public void DisposeCvvResults()
        {
            foreach (var res in CvvResults)
            {
                res.Dispose();
            }

            CvvResults.Clear();
        }

        public void Dispose()
        {
           /* var itemsToDispose = new List<List<AnalyzeResults>>
            {
                //ReadableResults,
                CvvResults,
                _cvvResults
            };

            foreach (var item in itemsToDispose)
            {
                foreach (var res in item)
                {
                    res.Dispose();
                }

                item.Clear();
            }*/

            ReadableResults = null;
            CvvResults = null;
            _cvvResults = null;
            _cvvStatistics.Clear();
            _candVSums.Clear();
            Repetitions.Clear();
            Letters.Clear();
            _transcribedToUkrainian.Clear();

            _cvvStatistics = null;
            _transcribedToUkrainian = null;
            _candVSums = null;
            exporter = null;
            Repetitions = null;
            Letters = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public List<AnalyzeResults> ReadableResults { get; set; }

        public List<AnalyzeResults> CvvResults
        {
            get
            {
                return _cvvResults;
            }

            set
            {
                _cvvResults = value;                
            }
        }

        public int TextLength
        {
            get
            {
                return Letters.Sum(c => c.Value);
                //return ReadableResults.Sum(r => r.Word.Length);
            }
        }

        public int SyllablesCount
        {
            get
            {
                //return ReadableResults.Sum(r => r.Syllables.Length);
                if (ReadableResults.Count != 0)
                {
                    return ReadableResults.Sum(r => r.Syllables.Length);
                }
                else return _syllablesCount;

            }
        }

        public Dictionary<string, int> Repetitions { get; set; }

        public Dictionary<char, int> Letters { get; set; }

        public Dictionary<string, int> CvvStatistics
        {
            get
            {
                if (_cvvStatistics == null)
                    _cvvStatistics = GetCvvStatistics();

                return _cvvStatistics;
            }
        }

        public string FileName { get; set; }        

        public List<double> CandVSums { get => _candVSums; set => _candVSums = value; }

        private Dictionary<string, int> GetCvvStatistics()
        {
            var res = new Dictionary<string, int>();           

            var exportedCvv = exporter.ConvertToCvv(CvvResults);

            foreach(var cvvResult in exportedCvv)
            {
                for (var i = 0; i < cvvResult.Syllables.Length; i++)
                {
                    if (IsEmptySyllable(cvvResult.Syllables[i]))
                        continue;                 
                    
                    if (!res.ContainsKey(cvvResult.Syllables[i]))
                    {
                        res.Add(cvvResult.Syllables[i], 1);
                    }
                    else
                    {
                        res[cvvResult.Syllables[i]]++;
                    }
                }
            }

            return res;
        }
        

        public List<string> TranscribedToUkrainianSpellingWords
        {
            get { return _transcribedToUkrainian; }
            set { _transcribedToUkrainian = value; }
        }


        private bool IsEmptySyllable(string syllable)
        {
            return String.IsNullOrEmpty(syllable) || String.IsNullOrWhiteSpace(syllable);
        }
    }
}
