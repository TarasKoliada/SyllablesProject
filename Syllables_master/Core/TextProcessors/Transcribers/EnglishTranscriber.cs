using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TextProcessors.Transcribers
{
    public class EnglishTranscriber : ISpellingTranscriber
    {
        public string Transcribe(string text)
        {
            return text;
        }
    }
}
