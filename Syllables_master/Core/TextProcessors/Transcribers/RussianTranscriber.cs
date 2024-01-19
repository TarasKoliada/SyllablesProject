using System.Collections.Generic;
using System.Text;

namespace Core.TextProcessors.Transcribers
{
    //TODO
    /*CHECK correct RemoveJAfterConsonant method working (which checks index of modified element)
     * CHECK word file with questions to the teacher
     * THINK what word also should be added to charsToReplace dictionary
     * also run program and wriite word 'вольяжный' it must be волjажниY --- DONE (NEED MORE TESTING)!!!
     * TEST program with other words
     */
    public class RussianTranscriber : ISpellingTranscriber
    {
        private readonly Dictionary<string, string> charsToReplace;
        private readonly HashSet<char> consonants;
        public RussianTranscriber()
        {
            charsToReplace = new Dictionary<string, string>
            {
                {"и", "і"},
                {"е", "jе"},
                {"э", "е"},
                {"ю", "jу"},
                {"я", "jа"},
                {"ы", "и"}
            };

            consonants = new HashSet<char>
            {'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'};
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
                if (word[i] == 'j' && !inputJindices.Contains(i))
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
