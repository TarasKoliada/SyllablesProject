using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sklady.TextProcessors
{
    public class EnglishPhoneticProcessor : PhoneticProcessorBase
    {
        private string[] dzPrefixes = new string[] { "під", "над", "від" };

        public EnglishPhoneticProcessor ( CharactersTable charactersTable, bool[] isCheckbox )
            : base(charactersTable, isCheckbox)
        {
        }

        public override string Process ( string input, bool[] isCheckbox )
        {
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

        private string ReductionReplacements(string res)
        {
            res = Regex.Replace(res, "cionals", "cinals");
            res = Regex.Replace(res, "sional", "sonal");
            res = Regex.Replace(res, "tional", "tonal");
            res = Regex.Replace(res, "cions", "cons");
            res = Regex.Replace(res, "sions", "sons");
            res = Regex.Replace(res, "tions", "tons");
            res = Regex.Replace(res, "were", "wer");
            res = Regex.Replace(res, "cion", "con");
            res = Regex.Replace(res, "sion", "son");
            res = Regex.Replace(res, "ore", "or");
            res = Regex.Replace(res, "ous", "os");
            res = Regex.Replace(res, "ae", "a");
            res = Regex.Replace(res, "ai", "a");
            res = Regex.Replace(res, "au", "u");

            return res;
        }

        private string AsymilativeReplacements(string res)
        {
            res = Regex.Replace(res, "(e)(a)", "e");
            res = Regex.Replace(res, "(e)(e)", "e");
            res = Regex.Replace(res, "^(a)(r)(e)^", "ar");
            res = Regex.Replace(res, "(e)(o)", "o");
            res = Regex.Replace(res, "(i)(e)", "i");
            res = Regex.Replace(res, "(u)(a)", "u");
            res = Regex.Replace(res, "(u)(e)", "e");
            res = Regex.Replace(res, "(u)(i)", "i");
            res = Regex.Replace(res, "(e)(d)^", "d");
            res = Regex.Replace(res, "(y)^", "i");
            res = Regex.Replace(res, "(e)^", "y");

            return res;
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
