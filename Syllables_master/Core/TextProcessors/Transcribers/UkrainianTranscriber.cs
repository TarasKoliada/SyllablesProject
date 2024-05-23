using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TextProcessors.Transcribers
{
    public class UkrainianTranscriber : ISpellingTranscriber
    {
        private readonly Dictionary<string, string> charsToReplace;
        private readonly HashSet<char> consonants;
        public UkrainianTranscriber()
        {
            charsToReplace = new Dictionary<string, string>
            {
                {"jу", "JУ"},
                {"jе", "JЕ"},
                {"jа", "JА"},
                {"jі", "JІ"},
                {"ї", "JІ"},
                {"є", "JЕ"},
                {"ю", "JУ"},
                {"я", "JА"},
                {"j", "J"},
                {"щ", "ШЧ"},
                {"шч", "ШЧ"},
                {"ь", ""} 
                
            };

            consonants = new HashSet<char>
            {'б', 'в', 'v', 'u', 'w', 'г', 'ґ', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'};
        }
        public string Transcribe(string inputWord)
        {
            List<int> noDeletableJindexes = new();

            var currentWord = new StringBuilder(inputWord);

            if (inputWord.Contains('j'))
                noDeletableJindexes = GetNotToDeleteJindexes(inputWord);

            foreach (var charsPair in charsToReplace)
                currentWord.Replace(charsPair.Key, charsPair.Value);

            var transcribedWord = RemoveJAfterConsonant(currentWord.ToString(), noDeletableJindexes);
            return transcribedWord;
        }
        //Returns list with existing letter 'j' indexes in input string to save them
        private List<int> GetNotToDeleteJindexes(string word)
        {
            List<int> indexes = new();

            for (int i = 0; i < word.Length; i++)
                if (word[i] == 'j')
                    indexes.Add(i);

            return indexes;
        }

        private string RemoveJAfterConsonant(string word, List<int> inputJindices)
        {
            bool[] isProcessed = new bool[word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == 'J' && !inputJindices.Contains(i))
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
