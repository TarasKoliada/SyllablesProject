using Sklady.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklady.Models
{
    public class FileProcessingResult
    {
        private Dictionary<string, int> _cvvStatistics;
        private List<AnalyzeResults> _cvvResults;

        private ResultsExporter exporter;

        public FileProcessingResult(ResultsExporter exporter)
        {
            ReadableResults = new List<AnalyzeResults>();
            CvvResults = new List<AnalyzeResults>();
            this.exporter = exporter;
            this.Repetitions = new Dictionary<string, int>();
            this.Letters = new Dictionary<char, int>();
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
                return ReadableResults.Sum(r => r.Syllables.Length);
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

        private Dictionary<string, int> GetCvvStatistics()
        {
            var res = new Dictionary<string, int>();           

            var exportedCvv = exporter.ConvertToCvv(this.CvvResults.Select(c => c).ToList());

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

        private bool IsEmptySyllable(string syllable)
        {
            return String.IsNullOrEmpty(syllable) || String.IsNullOrWhiteSpace(syllable);
        }
    }
}
