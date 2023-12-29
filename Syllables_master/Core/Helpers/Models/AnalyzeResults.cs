using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklady
{   

    public class AnalyzeResults : IDisposable
    {
        public string Word { get; set; }

        public string[] Syllables { get; set; }

        public void Dispose()
        {
            Word = null;
            Syllables = null;
        }
    }
}
