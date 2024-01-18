using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sklady.TextProcessors
{
    public class UkrainePhoneticProcessor : PhoneticProcessorBase
    {
        private string[] dzPrefixes = new string[] { "під", "над", "від" };

        private readonly Dictionary<string, string> ReductionReplacementsPatterns = new Dictionary<string, string>
        {
            { "нтськ", "нск" },
            { "стськ", "ск" },
            { "нтст", "нст" },
            { "стц", "сц" },
            { "стч", "шч" },
            { "стд", "зд" },
            { "стс", "с" },
            { "стн", "сн" },
            { "нтс", "нс" },
            { "тст", "ц" },
            { "тьс", "ц" }
        };

        private readonly Dictionary<string, string> AsymilativeReplacementsPatterns = new Dictionary<string, string>
        {
            { "(с)(ш)", "$2" },
            { "(з)(ж)", "$2" },
            { "^(з)(ш)", "$2" },
            { "(ш)(с)", "$2" },
            { "(ч)(ц)", "$2" },
            { "(т)(с)", "ц" },
            { "(т)(ц)", "$2" },
            { "(т)(ч)", "$2" }
        };

        public UkrainePhoneticProcessor ( CharactersTable charactersTable, bool[] isCheckbox )
            : base(charactersTable, isCheckbox)
        {
        }

        public override string Process ( string input, bool[] isCheckbox )
        {
            //Exclude russian letters  also
           // if (!ContainsOnlyUkrainianLetters(input))
             //   throw new FormatException();

            var res = ProcessTwoSoundingLetters(input);
            res = ProcessDoubleConsonants(res);
            res = ProcessDzDj(res);
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
                       .Replace("Y", "й")
                       .Replace("j", "я")
                       .Replace("-", "");
        }

        public override string ProcessNonStableCharacters(string word, bool isPhoneticsMode = true)
        {
            var res = base.ProcessNonStableCharacters(word);
            res = ProcessV(res);

            if (isPhoneticsMode)
                res = Regex.Replace(res, "ь", "");

            return res;
        }
        /*public override string TranscribeToUkrainianSpelling(string word, Languages language)
        {
            return word;
        }*/

        private bool ContainsOnlyUkrainianLetters(string word)
        {
            return Regex.IsMatch(word, @"^[а-яґєіїй']+$");
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
            input = Regex.Replace(input, @"([а-яА-Я])\1+", "$1");

            return input;
        }

        private string ProcessTwoSoundingLetters(string input)
        {
            input = ReplacePhoneticCharacter('ю', "jу", input);
            input = ReplacePhoneticCharacter('я', "jа", input);
            input = ReplacePhoneticCharacter('є', "jе", input);            
            input = Regex.Replace(input, "ї", "і");
            input = Regex.Replace(input, "щ", "шч");
            input = Regex.Replace(input, "ю", "jy");
            input = Regex.Replace(input, "я", "ja");

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
