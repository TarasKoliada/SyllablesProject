using System.Collections.Generic;
using System.Text;

namespace Core.TextProcessors.Transcribers
{
    //TODO
    /*CHECK correct RemoveJAfterConsonant method working (which checks index of modified element)
     * CHECK word file with questions to the teacher
     * THINK what word also should be added to charsToReplace dictionary
     * also run program and wriite word 'вольяжный' it must be волjажниY
     * TEST program with other words
     */
    public class RussianTranscriber : ISpellingTranscriber
    {
        private readonly Dictionary<string, string> charsToReplace;
        private readonly HashSet<char> consonants;
        private bool[] modifiedIndices;
        public RussianTranscriber()
        {
            charsToReplace = new Dictionary<string, string>
            {
                {"ы", "и"},
                {"и", "і"},
                {"е", "jе"},
                {"э", "е"},
                {"ю", "jу"},
                {"я", "jа"}

            };

            consonants = new HashSet<char>
            {'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'};
        }
        public string Transcribe(string text)
        {
            var stringBuilder = new StringBuilder(text);

            foreach (var pattern in charsToReplace)
            {
                stringBuilder.Replace(pattern.Key, pattern.Value);
            }

            var result = RemoveJAfterConsonant(stringBuilder.ToString());
            return result;
        }


        /*private string RemoveJAfterConsonant(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                // Якщо поточний символ - 'j'
                if (text[i] == 'j')
                {
                    // Якщо перед ним стоїть символ із consonantCharacters
                    if (i > 0 && consonants.Contains(text[i - 1]))
                    {
                        // Видаляємо символ 'j'
                        text = text.Remove(i, 1);
                    }
                }
            }
            return text;
        }*/
        
        private string RemoveJAfterConsonant(string text)
        {
            int currentIndex = 0;

            bool[] isProcessed = new bool[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == 'j')
                {
                    //if previous symbol is consonant
                    if (i > 0 && consonants.Contains(text[i - 1]))
                    {
                        if (!isProcessed[i])
                        {
                            text = text.Remove(i, 1);
                            currentIndex = i;
                            isProcessed[i] = true;
                        }
                    }
                }
            }
            return text;
        }
           

        }
}
