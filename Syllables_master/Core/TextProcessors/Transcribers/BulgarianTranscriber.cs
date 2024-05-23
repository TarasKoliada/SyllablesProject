using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TextProcessors.Transcribers
{
    public class BulgarianTranscriber : ISpellingTranscriber
    {
        private readonly Dictionary<string, string> charsToReplace;
        private readonly HashSet<char> consonants;

        public BulgarianTranscriber()
        {
            charsToReplace = new Dictionary<string, string>
            {
                {"jа", "JА"},
                {"jу", "JУ"},
                {"я", "JА"},
                {"ю", "JУ"},
                {"и", "І"},
                {"щ", "ШТ"},
                {"шт", "ШТ"},
                {"й", "J"},
                {"j", "J"},
                {"ъ", "А"},
            };
            consonants = new HashSet<char>
            {'б', 'в', 'г', 'ґ', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'};
        }
        public string Transcribe(string inputWord)
        {
            var currentWord = new StringBuilder(inputWord);
            
            foreach (var charsPair in charsToReplace)
                currentWord.Replace(charsPair.Key, charsPair.Value);

            var transcribedWord = RemoveJAfterConsonant(currentWord.ToString());
            transcribedWord = RemoveConsecutiveDuplicates(transcribedWord);

            return transcribedWord;
        }

        private string RemoveJAfterConsonant(string word)
        {
            bool[] isProcessed = new bool[word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == 'J')
                {
                    //if previous symbol is consonant
                    if (i > 0 && consonants.Contains(word[i - 1]))
                    {
                        if (!isProcessed[i])
                        {
                            word = word.Remove(i, 1);
                            isProcessed[i] = true;
                        }
                    }
                }
            }
            return word;
        }

        //Remove chars duplication like `нн`, 'лл' ...
        public static string RemoveConsecutiveDuplicates(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder result = new StringBuilder();
            char previousChar = input[0];
            result.Append(previousChar);

            for (int i = 1; i < input.Length; i++)
            {
                char currentChar = input[i];
                if (currentChar != previousChar)
                {
                    result.Append(currentChar);
                    previousChar = currentChar;
                }
            }

            return result.ToString();
        }
    }
}
