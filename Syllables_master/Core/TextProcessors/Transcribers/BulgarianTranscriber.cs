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
                {"я", "JА"},
                {"ю", "JУ"},
                {"и", "І"},
                {"щ", "ШТ"},
                {"й", "J"},
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

            return transcribedWord;
            //return inputWord;
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
    }
}
