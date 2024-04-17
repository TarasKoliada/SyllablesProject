using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TextProcessors.Transcribers
{
    public class AncientTranscriber : ISpellingTranscriber
    {
        private readonly Dictionary<string, string> charsToReplace;
        public AncientTranscriber()
        {
            charsToReplace = new Dictionary<string, string>
            {
                {"и", "і"},
                {"е", "jе"},
                {"ю", "jу"},
                {"я", "jа"},
                {"ы", "и"}
            };
        }
        public string Transcribe(string text)
        {
            /*var currentWord = new StringBuilder(text);

            foreach (var charsPair in charsToReplace)
                currentWord.Replace(charsPair.Key, charsPair.Value);*/

            return text;
        }
    }
}
