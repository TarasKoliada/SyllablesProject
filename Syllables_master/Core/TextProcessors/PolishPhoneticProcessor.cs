using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sklady;
using Core;
using System.Globalization;
using Core.Helpers.Models;

namespace Sklady.TextProcessors
{
    public class PolishPhoneticProcessor : PhoneticProcessorBase
    {
        private string[] dzPrefixes = new string[] { "під", "над", "від" };

        public PolishPhoneticProcessor ( CharactersTable charactersTable, bool[] isCheckbox ) : base(charactersTable, isCheckbox )
        {
        }

        public override string Process ( string input, bool[] isCheckbox )
        {
            var res = ProcessTwoSoundingLetters(input);
            res = ProcessDoubleConsonants(res);
            res = ProcessDzDj(res);
            res = ReductionReplacements(res, isCheckbox);
            res = AsymilativeReplacements(res);

            return res;
        }

        public override string RemoveTechnicalCharacters ( string word )
        {
            return word.Replace("d", "дж")
                       .Replace("z", "дз")
                       .Replace("v", "в")
                       .Replace("w", "в")
                       .Replace("u", "в")
                       .Replace("j", "й")
                       .Replace("Y", "й");
        }

        public override string ProcessNonStableCharacters ( string word, bool isPhoneticsMode = true )
        {
            var res = base.ProcessNonStableCharacters(word);
            res = ProcessV(res);

            if (isPhoneticsMode)
                res = Regex.Replace(res, "ь", "");

            return res;
        }
        public override string TranscribeToUkrainianSpelling(string word)
        {
            return word;
        }

        private string ReductionReplacements ( string res, bool[] isCheckbox)
        {
            string optionalCharG = "ґ";
            string optionalCharL = "u";
            string optionalCharE = "ë";
            string optionalCharA = "ö";
            
            //optional
            if (CheckBoxData.PolishLetter_G_CheckboxState == true)
            {
                optionalCharG = "г";
            }

            if (CheckBoxData.PolishLetter_L_CheckboxState == true)
            {
                optionalCharL = "л";
            }

            if (CheckBoxData.PolishLetter_E_CheckboxState == true)
            {
                optionalCharE = "е";
            }

            if (CheckBoxData.PolishLetter_O_CheckboxState == true)
            {
                optionalCharA = "о";
            }

            res = Regex.Replace(res, "ia", "а");
            res = Regex.Replace(res, "ią", "ą");
            res = Regex.Replace(res, "ie", "е");
            res = Regex.Replace(res, "ię", "ę");
            res = Regex.Replace(res, "io", "о");
            res = Regex.Replace(res, "ąs", "ös");
            res = Regex.Replace(res, "ąz", "öz");
            res = Regex.Replace(res, "ąś", "öś");
            res = Regex.Replace(res, "ąź", "öź");
            res = Regex.Replace(res, "ąrz", "örz");
            res = Regex.Replace(res, "ąw", "öw");
            res = Regex.Replace(res, "ąf", "öf");
            res = Regex.Replace(res, "ąch", "öch");
            res = Regex.Replace(res, "ąg", "ög");
            res = Regex.Replace(res, "ąk", "ök");
            res = Regex.Replace(res, "ąb", "омb");
            res = Regex.Replace(res, "ąp", "омp");
            res = Regex.Replace(res, "ą", optionalCharA);
            res = Regex.Replace(res, "ąd", "онd");
            res = Regex.Replace(res, "ąt", "онt");
            res = Regex.Replace(res, "ąc", "онc");
            res = Regex.Replace(res, "ąć", "онć");
            res = Regex.Replace(res, "ąl", "ол");
            res = Regex.Replace(res, "ął", "ол");
            res = Regex.Replace(res, "ęs", "ës");
            res = Regex.Replace(res, "ęz", "ëz");
            res = Regex.Replace(res, "ęś", "ëś");
            res = Regex.Replace(res, "ęź", "ëź");
            res = Regex.Replace(res, "ęrz", "ërz");
            res = Regex.Replace(res, "ęw", "ëw");
            res = Regex.Replace(res, "ęf", "ëf");
            res = Regex.Replace(res, "ęch", "ëch");
            res = Regex.Replace(res, "ęg", "ëg");
            res = Regex.Replace(res, "ęk", "ëk");
            res = Regex.Replace(res, "ę", optionalCharE);
            res = Regex.Replace(res, "ęb", "емb");
            res = Regex.Replace(res, "ęp", "емp");
            res = Regex.Replace(res, "ęd", "енd");
            res = Regex.Replace(res, "ęt", "енt");
            res = Regex.Replace(res, "ęc", "енc");
            res = Regex.Replace(res, "ęć", "енć");
            res = Regex.Replace(res, "ęs", "ës");
            res = Regex.Replace(res, "ęz", "ë");
            res = Regex.Replace(res, "ęl", "ел");
            res = Regex.Replace(res, "ęł", "ел");
            res = Regex.Replace(res, "ch", "х");
            res = Regex.Replace(res, "cz", "ч");
            res = Regex.Replace(res, "dz|dź", "дз");
            res = Regex.Replace(res, "dż", "дж");
            res = Regex.Replace(res, "rz", "ж");
            res = Regex.Replace(res, "sz", "ш");
            res = Regex.Replace(res, "b", "б");
            res = Regex.Replace(res, "c|ć", "ц");
            res = Regex.Replace(res, "d", "д");
            res = Regex.Replace(res, "e", "е");
            res = Regex.Replace(res, "f", "ф");
            res = Regex.Replace(res, "g", optionalCharG);//додати опцію г
            res = Regex.Replace(res, "h", "х");
            res = Regex.Replace(res, "i", "і");
            res = Regex.Replace(res, "j", "й");
            res = Regex.Replace(res, "k", "к");
            res = Regex.Replace(res, "ł", "л");
            res = Regex.Replace(res, "m", "м");
            res = Regex.Replace(res, "n|ń", "н");
            res = Regex.Replace(res, "o", "о");
            res = Regex.Replace(res, "ó", "у");
            res = Regex.Replace(res, "p", "п");
            res = Regex.Replace(res, "r", "р");
            res = Regex.Replace(res, "s|ś", "с");
            res = Regex.Replace(res, "t", "т");
            res = Regex.Replace(res, "u", "у");
            res = Regex.Replace(res, "w", "w");
            res = Regex.Replace(res, "y", "и");
            res = Regex.Replace(res, "z|ź", "з");
            res = Regex.Replace(res, "ż", "ж");
            res = Regex.Replace(res, "l", optionalCharL);//додати опцію u
            res = Regex.Replace(res, "s|ś", "с");
            res = Regex.Replace(res, "s|ś", "с");
            res = Regex.Replace(res, "s|ś", "с");
            res = Regex.Replace(res, "s|ś", "с");

            return res;
        }

        private string AsymilativeReplacements ( string res )
        {
            res = Regex.Replace(res, "(с)(ш)", "$2");
            res = Regex.Replace(res, "(з)(ж)", "$2");
            res = Regex.Replace(res, "^(з)(ш)", "$2");
            res = Regex.Replace(res, "(ш)(с)", "$2");
            res = Regex.Replace(res, "(ч)(ц)", "$2");
            res = Regex.Replace(res, "(т)(с)", "ц");
            res = Regex.Replace(res, "(т)(ц)", "$2");
            res = Regex.Replace(res, "(т)(ч)", "$2");

            return res;
        }

        private string ProcessDoubleConsonants ( string input )
        {
            input = Regex.Replace(input, @"([а-яА-Я])\1+", "$1");

            return input;
        }

        private string ProcessTwoSoundingLetters ( string input )
        {
            input = ReplacePhoneticCharacter('ю', "йу", input);
            input = ReplacePhoneticCharacter('я', "йа", input);
            input = ReplacePhoneticCharacter('є', "йе", input);
            input = Regex.Replace(input, "ї", "і");
            input = Regex.Replace(input, "щ", "шч");

            return input;
        }

        private string ProcessDzDj ( string word )
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

        public string ProcessV ( string word )
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

        private bool IsFirstOrHasVowelBefore ( string word, int indexOfV )
        {
            return indexOfV == 0 || !CharactersTable.isConsonant(word[indexOfV - 1]);
        }

        private bool IsLastOrHasConsonantAfter ( string word, int indexOfV )
        {
            return indexOfV == word.Length - 1 || CharactersTable.isConsonant(word[indexOfV + 1]);
        }

        private bool HasConsonantBeforeAndVowelAfter ( string word, int indexOfV )
        {
            return (indexOfV != 0 && CharactersTable.isConsonant(word[indexOfV - 1]))
                   && (indexOfV != word.Length - 1 && !CharactersTable.isConsonant(word[indexOfV + 1]));
        }

        private bool HasPredefinedPreffix ( string word, int indexOfSound )
        {
            if (indexOfSound > 1 && this.dzPrefixes.Any(p => p == word.Substring(indexOfSound - 2, p.Length)))
                return true;

            return false;
        }
    }
}
