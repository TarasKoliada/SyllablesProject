using Core.Helpers;
using Sklady.Export;
using Sklady.Models;
using Sklady.TextProcessors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sklady
{
    public class TextAnalyzer
    {
        private Stopwatch _stopWatch = new Stopwatch();
        public long ElapsedToCountWords { get { return _stopWatch.ElapsedMilliseconds; } }
        private string _text;
        private string[] _words;
        private WordAnalyzer _wordAnalyzer;
        private CharactersTable table;
        private PhoneticProcessorBase _phoneticProcessor;// = new PhoneticProcessor();        
        Settings settings;
        ResultsExporter exporter;

        public string FileName { get; private set; }
        public int TextLength { get; private set; }

        public event Action<int, int, string> OnWordAnalyzed;
        public event Action<Exception, string, string> OnErrorOccured;

        public TextAnalyzer(string text, string fileName, Settings settings, ResultsExporter exporter, bool[] isCheckBox)
        {
            this.table = settings.CharactersTable;
            this.settings = settings;
            this.exporter = exporter;

            switch (settings.Language)
            {
                case Languages.Ukraine:
                    _phoneticProcessor = new UkrainePhoneticProcessor(table, isCheckBox);
                    break;
                case Languages.Russian:
                    _phoneticProcessor = new RussianPhoneticProcessor(table, isCheckBox);
                    break;
                case Languages.Ancient:
                    _phoneticProcessor = new AncientPhoneticProcessor(table, isCheckBox);
                    break;
                case Languages.English:
                    _phoneticProcessor = new EnglishPhoneticProcessor(table, isCheckBox);
                    break;
                case Languages.Polish:
                    _phoneticProcessor = new PolishPhoneticProcessor(table, isCheckBox);
                    break;
                case Languages.Bulgarian:
                    _phoneticProcessor = new BulgraianProneticProcessor(table, isCheckBox);
                    break;
            }
            
            FileName = fileName;
            _wordAnalyzer = new WordAnalyzer(table, settings);            
            PrepareText(text);
        }

        private void PrepareText(string inputText)
        {            
            var sb = new StringBuilder(inputText.ToLower());

            for (var i = 0; i < settings.CharactersToRemove.Count; i++) // Remove all unnecesarry characters
            {
                sb.Replace(settings.CharactersToRemove[i], "");
            }

            _text = sb.ToString();
            _text = Regex.Replace(_text, "([0-9][а-яА-Я])", "");//Remove chapter number (for vk)
            _text = Regex.Replace(_text, "([0-9][a-zA-Z])", "");
            _text = Regex.Replace(_text, "([0-9][a-żA-Ż])", ""); //polish
           // _text = Regex.Replace(_text, RegexHelper.RemoveAllLatinExcept(settings.CharsToSkip), "");
            _text = Regex.Replace(_text, @"/\t|\n|\r", " "); // remove new line, tabulation and other literals

            _text = Regex.Replace(_text, @"(\- )", ""); // Handle word hyphenations            
            _text = Regex.Replace(_text, @"и́| ̀и|ù|ѝ̀̀| ̀̀и|ѝ|́и", "й");

            _words = _text.Split(new[] { " ", " " }, StringSplitOptions.RemoveEmptyEntries).ToArray(); // Split text by words
        }

        public FileProcessingResult GetResults(bool[] isCheckbox)
        {
            var result = new FileProcessingResult(exporter);   
            for (var i = 0; i < _words.Length; i++)
            {
                try
                {
                    UpdateRepetitions(result.Repetitions, _words[i]);                    

                    if (settings.PhoneticsMode)
                        _words[i] = _phoneticProcessor.Process(_words[i], isCheckbox); // In case of phonetics mode make corresponding replacements

                    _words[i] = _phoneticProcessor.ProcessNonStableCharacters(_words[i], settings.PhoneticsMode); // Replace some chars according to their power

                    UpdateLetters(result.Letters, _words[i]);

                    var syllables = _wordAnalyzer.GetSyllables(_words[i]).ToArray();

                    if (settings.PhoneticsMode)                    
                        syllables = RemoveApos(syllables); 
                    

                    result.CvvResults.Add(new AnalyzeResults()
                    {
                        Word = _words[i],
                        Syllables = RemoveApos(syllables)
                    });

                    result.ReadableResults.Add(new AnalyzeResults()
                    {
                        Word = _words[i],
                        Syllables = settings.PhoneticsMode ? syllables : UnprocessPhonetics(syllables)
                    });

                    OnWordAnalyzed?.Invoke(i + 1, _words.Length, FileName);
                }
                catch (Exception ex)
                {
                   OnErrorOccured?.Invoke(ex, _words[i], FileName);
                } 
            }

            result.FileName = this.FileName;

            return result;
        }

        private void UpdateLetters(Dictionary<char, int> letters, string word)
        {
            _stopWatch.Start();           
            for (var i = 0; i < word.Length; i++)
            {                
                if (word[i] == '\'' || word[i] == '-' || word[i] == '\n' || word[i] == '\r' || word[i] == '\t')
                {
                    continue;
                }

                var key = GetKeyForLetter(word[i]);

                if (letters.ContainsKey(key))
                {
                    letters[key] += 1;
                }
                else
                {
                    letters[key] = 1;
                }
            }
            _stopWatch.Stop();
        }

        private char GetKeyForLetter(char letter)
        {
            var predefinedPairs = new Dictionary<char, char>();
            predefinedPairs.Add('я', 'а');
            predefinedPairs.Add('є', 'е');
            predefinedPairs.Add('ю', 'у');
            predefinedPairs.Add('ї', 'і');
            predefinedPairs.Add('ё', 'о');

            if (settings.Language == Languages.Russian)
                predefinedPairs.Add('е', 'э');

            return predefinedPairs.ContainsKey(letter) ? predefinedPairs[letter] : letter;
        }

        private void UpdateRepetitions(Dictionary<string, int> repetitions, string word)
        {
            var match = Regex.Match(word, @"([а-яА-Я])\1+");

            if (match.Success)
            {
                if (!repetitions.ContainsKey(match.Value))
                {
                    repetitions.Add(match.Value, 1);
                }
                else
                {
                    repetitions[match.Value]++;
                }
            }                     
        }

        private string[] UnprocessPhonetics(string[] syllabeles)
        {
            for (var i = 0; i < syllabeles.Length; i++)
            {
                syllabeles[i] = _phoneticProcessor.RemoveTechnicalCharacters(syllabeles[i]);
            }

            return syllabeles;
        }

        private string[] RemoveApos(string[] input)
        {
            var result = new string[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                result[i] = input[i].Replace("'", "");
            }

            return result;
        }
    }
}
