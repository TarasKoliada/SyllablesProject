using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sklady.TextProcessors
{
    public class EnglishPhoneticProcessor : PhoneticProcessorBase
    {
        private string[] dzPrefixes = new string[] { "під", "над", "від" };

        // Patterns for ReductionReplacements
        private readonly Dictionary<string, string> ReductionReplacementsPatterns = new Dictionary<string, string>
        {
            {"cionals", "cinals"},
            {"sional", "sonal"},
            {"tional", "tonal"},
            {"cions", "cons"},
            {"sions", "sons"},
            {"tions", "tons"},
            {"were", "wer"},
            {"cion", "con"},
            {"sion", "son"},
            {"ore", "or"},
            {"ous", "os"},
            {"ae", "a"},
            {"ai", "a"},
            {"au", "u"}
        };

        // Patterns for AsymilativeReplacements
        private Dictionary<string, string> AsymilativeReplacementsPatterns = new Dictionary<string, string>
        {
            {"ea", "e"},
            {"ee", "e"},
            {"^are^", "ar"},
            {"eo", "o"},
            {"ua", "u"},
            {"ue", "e"},
            {"ui", "i"},
            {"ie", "i"},
            {"ed^", "d"},
            {"y^", "i"},
            {"e^", "y"}
        };


        public EnglishPhoneticProcessor ( CharactersTable charactersTable, bool[] isCheckbox )
            : base(charactersTable, isCheckbox)
        {
        }
        
        public override string Process ( string input, bool[] isCheckbox )
        {

            if (!MatchesWithEnglishAlphabet(input))
                throw new FormatException();

            var res = ProcessTwoSoundingLetters(input);
            res = ProcessDoubleConsonants(res);
            res = ReductionReplacements(res);
            res = AsymilativeReplacements(res);

            return res;
        }

        public override string RemoveTechnicalCharacters(string word)
        {
            return word.Replace("d", "дж")
                       .Replace("z", "дз")
                       .Replace("v", "в")
                       .Replace("w", "в")
                       .Replace("u", "в")
                       .Replace("j", "й")
                       .Replace("Y", "й");
        }

        public override string ProcessNonStableCharacters(string word, bool isPhoneticsMode = true)
        {
            var res = base.ProcessNonStableCharacters(word);
            res = ProcessV(res);

            if (isPhoneticsMode)
                res = Regex.Replace(res, "ь", "");

            return res;
        }

        public bool MatchesWithEnglishAlphabet(string word)
        {
            Regex regex = new("^[A-Za-z]+$");

            return regex.IsMatch(word);
        }

        private string ReductionReplacements(string res)
        {
            var stringBuilder = new StringBuilder(res);

            foreach (var pattern in ReductionReplacementsPatterns)
            {
                stringBuilder.Replace(pattern.Key, pattern.Value);
            }

            return stringBuilder.ToString();
        }

        private string AsymilativeReplacements(string res)
        {
            var stringBuilder = new StringBuilder(res);

            foreach (var pattern in AsymilativeReplacementsPatterns)
            {
                stringBuilder.Replace(pattern.Key, pattern.Value);
            }

            return stringBuilder.ToString();
        }

        private string ProcessDoubleConsonants(string input)
        {
            input = Regex.Replace(input, @"([a-zA-Z])\1+", "$1");

            return input;
        }

        private string ProcessTwoSoundingLetters(string input)
        {
            input = ReplacePhoneticCharacter('ю', "jу", input);
            return input;
        }

        private string ProcessDzDj(string word)
        {
            word = word.Replace("дж", "d");

            var indexOfDz = word.IndexOf("дз");

            while (indexOfDz != -1)
            {
                if (HasPredefinedPreffix(word, indexOfDz))
                {
                    indexOfDz = word.IndexOf("дз", indexOfDz + 1);
                }
                else
                {
                    word = word.Remove(indexOfDz, 2).Insert(indexOfDz, "z");
                    indexOfDz = word.IndexOf("дз", indexOfDz + 1);
                }
            }
            return word;
        }

        public string ProcessV(string word)
        {
            var indexOfV = word.IndexOf('в');

            while (indexOfV != -1)
            {
                if (IsFirstOrHasVowelBefore(word, indexOfV) && IsLastOrHasConsonantAfter(word, indexOfV))
                {
                    word = word.Remove(indexOfV, 1).Insert(indexOfV, "u");
                }
                else if (HasConsonantBeforeAndVowelAfter(word, indexOfV))
                {
                    word = word.Remove(indexOfV, 1).Insert(indexOfV, "w");
                }
                else
                {
                    word = word.Remove(indexOfV, 1).Insert(indexOfV, "v");
                }

                indexOfV = word.IndexOf('в', indexOfV + 1);
            }

            return word;
        }

        private bool IsFirstOrHasVowelBefore(string word, int indexOfV)
        {
            return indexOfV == 0 || !CharactersTable.isConsonant(word[indexOfV - 1]);
        }

        private bool IsLastOrHasConsonantAfter(string word, int indexOfV)
        {
            return indexOfV == word.Length - 1 || CharactersTable.isConsonant(word[indexOfV + 1]);
        }

        private bool HasConsonantBeforeAndVowelAfter(string word, int indexOfV)
        {
            return (indexOfV != 0 && CharactersTable.isConsonant(word[indexOfV - 1]))
                   && (indexOfV != word.Length - 1 && !CharactersTable.isConsonant(word[indexOfV + 1]));
        }

        private bool HasPredefinedPreffix(string word, int indexOfSound)
        {
            if (indexOfSound > 1 && this.dzPrefixes.Any(p => p == word.Substring(indexOfSound - 2, p.Length)))
                return true;

            return false;
        }
    }
}
