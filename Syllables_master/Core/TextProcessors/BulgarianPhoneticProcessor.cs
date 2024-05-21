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
        private readonly char[] specialChars = new char[] { 'п', 'ф', 'т', 'с', 'ц', 'ш', 'ч', 'к' };
        private Dictionary<char, Func<string, int, char>> characterMap;
        public BulgraianProneticProcessor(CharactersTable charactersTable, bool[] isCheckbox)
            : base(charactersTable, isCheckbox)
        {    }


        
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
                    if (processedChar != '\0')
                        res.Append(processedChar);
                }
                else res.Append(c);
            }

            return res.ToString();
        }

        private void GenerateCharacterMap(StringBuilder res)
        {
            characterMap = new Dictionary<char, Func<string, int, char>>()
            {
                {'б', ProcessCharacterB},
                {'в', ProcessCharacterV},
                {'г', ProcessCharacterG},
                {'д', ProcessCharacterD},
                {'ж', ProcessCharacterZh},
                {'з', ProcessCharacterZ},
                //{'и', (str, index) => 'і'},
                //{'щ', (str, index) => { res.Append("шт"); return '\0'; }},
                //{'ъ', (str, index) => 'а'},
                {'ь', (str, index) => '\0'},
                //{'й', (str, index) => 'j'},
                //{'ю', (str, index) => { res.Append("jу"); return '\0'; }},
                //{'я', (str, index) => { res.Append("jа"); return '\0'; }},
            };
        }
        private bool IsFirstLetterOfWord(string input, int index) => index == 0 || input[index - 1] == ' ';

        private bool IsInMiddleOfWord(string input, int index)
            => index > 0 && index < input.Length - 1 && input[index - 1] != ' ' && input[index + 1] != ' ';

        private bool IsAfterSpecialCharacter(string input, int index)
            => IsInMiddleOfWord(input, index - 1) && IsInMiddleOfWord(input, index) && specialChars.Contains(input[index - 1]);

        private bool IsBeforeSpecialCharacter(string input, int index)
            => IsInMiddleOfWord(input, index) && IsInMiddleOfWord(input, index + 1) && specialChars.Contains(input[index + 1]);

        private bool IsLastLetterOfWord(string input, int index)
            => index + 1 == input.Length || input[index + 1] == ' ';

        public override string RemoveTechnicalCharacters(string word) => word;


        //remove all characters that do not belong to the Bulgarian alphabet
        public override string ProcessNonStableCharacters(string word, bool isPhoneticsMode = true)
        {
            string bulgarianAlphabetPattern = @"[А-Яа-яЁёҐґJjІі]";
            var builder = new StringBuilder();
            foreach (char c in word.Where(c => Regex.IsMatch(c.ToString(), bulgarianAlphabetPattern)))
                builder.Append(c);

            return builder.ToString();
        }

        private char ProcessCharacterB(string input, int index)
            => IsLastLetterOfWord(input, index) || IsBeforeSpecialCharacter(input, index) ? 'п' : 'б';

        private char ProcessCharacterV(string input, int index)
            => IsLastLetterOfWord(input, index) /*|| IsFirstLetterOfWord(input, index)*/ || IsBeforeSpecialCharacter(input, index) ? 'ф' : 'в';

        private char ProcessCharacterG(string input, int index)
            => IsLastLetterOfWord(input, index) || IsBeforeSpecialCharacter(input, index)
                ? 'к' : 'ґ';/*(IsFirstLetterOfWord(input, index) ? 'ґ' : 'г');*/

        private char ProcessCharacterD(string input, int index)
            => IsLastLetterOfWord(input, index) || IsBeforeSpecialCharacter(input, index) ? 'т' : 'д';


        private char ProcessCharacterZh(string input, int index)
            => IsLastLetterOfWord(input, index) || IsBeforeSpecialCharacter(input, index) ? 'ш' : 'ж';

        private char ProcessCharacterZ(string input, int index)
            => IsLastLetterOfWord(input, index) || IsBeforeSpecialCharacter(input, index) ? 'с' : 'з';
    }
}
