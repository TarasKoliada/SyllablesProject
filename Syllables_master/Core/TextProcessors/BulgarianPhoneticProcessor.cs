using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sklady.TextProcessors
{
    public class BulgraianProneticProcessor : PhoneticProcessorBase
    {
        private readonly char[] specialChars;
        private readonly char[] ringingSounds;
        private Dictionary<char, Func<string, int, string>> characterMap;
        public BulgraianProneticProcessor(CharactersTable charactersTable, bool[] isCheckbox)
            : base(charactersTable, isCheckbox)
        {   
            specialChars = new char[] { 'п', 'ф', 'т', 'с', 'ц', 'ш', 'ч', 'к' };
            ringingSounds = new char[] { 'б', 'в', 'г', 'ґ', 'д', 'ж', 'з'}; //дзвінкі звуки
        }


        
        public override string Process(string input, bool[] isCheckbox)
        {
            input = input.ToLower();
            var res = new StringBuilder(input.Length);
            GenerateCharacterMap(res);


            foreach (char c in input)
            {
                if (characterMap.TryGetValue(c, out var processChar))
                {
                    var processedChar = processChar(input, res.Length);
                    if (processedChar != "\0")
                        res.Append(processedChar);
                }
                else res.Append(c);
            }
            var result = ProcessTwoSoundingLetters(res.ToString());

            return result;
        }

        private void GenerateCharacterMap(StringBuilder res)
        {
            characterMap = new Dictionary<char, Func<string, int, string>>()
            {
                {'б', ProcessCharacterB},
                {'в', ProcessCharacterV},
                {'г', ProcessCharacterG},
                {'д', ProcessCharacterD},
                {'ж', ProcessCharacterZh},
                {'з', ProcessCharacterZ},
                {'щ', ProcessCharacterShch},
                {'ь', (str, index) => "\0"},
            };
        }

        //This method processing single ukrainian sound 'В'
        public string ProcessSingleV(string char_F, char nextWordFirstChar)
        {
            return ringingSounds.Contains(nextWordFirstChar) ? "в" : char_F; 
        }

        private bool IsBeforeSpecialCharacter(string input, int index) => specialChars.Contains(input[index + 1]);

        private bool IsLastLetterOfWord(string input, int index) => index + 1 == input.Length || input[index + 1] == ' ';

        private bool HasOtherSymbolsInString(int stringLength) => stringLength > 1;

        public override string RemoveTechnicalCharacters(string word) => word;


        //remove all characters that do not belong to the Bulgarian alphabet
        public override string ProcessNonStableCharacters(string word, bool isPhoneticsMode = true)
        {    
            /*string bulgarianAlphabetPattern = @"[А-Яа-яЁёҐґJjІіVv]";
            var builder = new StringBuilder();
            foreach (char c in word.Where(c => Regex.IsMatch(c.ToString(), bulgarianAlphabetPattern)))
                builder.Append(c);*/

            word = base.ProcessJ(word);

            return word;
        }

        private string ProcessCharacterB(string input, int index)
            => IsLastLetterOfWord(input, index) || IsBeforeSpecialCharacter(input, index) ? "п" : "б";

        private string ProcessCharacterV(string input, int index)
            => IsLastLetterOfWord(input, index) || IsBeforeSpecialCharacter(input, index) && HasOtherSymbolsInString(input.Length) ? "ф" : "v";

        private string ProcessCharacterG(string input, int index)
            => IsLastLetterOfWord(input, index) || IsBeforeSpecialCharacter(input, index) ? "к" : "ґ";

        private string ProcessCharacterD(string input, int index)
            => IsLastLetterOfWord(input, index) || IsBeforeSpecialCharacter(input, index) ? "т" : "д";


        private string ProcessCharacterZh(string input, int index)
            => IsLastLetterOfWord(input, index) || IsBeforeSpecialCharacter(input, index) ? "ш" : "ж";

        private string ProcessCharacterZ(string input, int index)
            => IsLastLetterOfWord(input, index) || IsBeforeSpecialCharacter(input, index) ? "с" : "з";

        private string ProcessCharacterShch(string input, int index) => "шт";

        private string ProcessTwoSoundingLetters(string input)
        {
            input = ReplacePhoneticCharacter('ю', "jу", input);
            input = ReplacePhoneticCharacter('я', "jа", input);
            return input;
        }
    }
}
