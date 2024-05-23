using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklady.Models
{
    public class Settings
    {      
        public bool SeparateAfterFirst { get; set; }
        public string SyllableSeparator { get; set; }
        public CharactersTable CharactersTable { get; set; }
        public bool PhoneticsMode { get; set; }
        public Languages Language { get; set; }
        public string LastOpenFolderPath { get; set; }
        public string LastSaveFolderPath { get; set; }
        public bool AbsoluteMeasures { get; set; }
        public List<string> CharactersToRemove { get; set; }
        public List<char> CharsToSkip { get; set; }
        public string readfileForWords { get; set; }

        public Settings()
        {
            SeparateAfterFirst = true;
            SyllableSeparator = "-";
            PhoneticsMode = true;
            Language = Languages.Ukrainian;
            LastOpenFolderPath = "";
            LastSaveFolderPath = "";
            AbsoluteMeasures = false;
            CharactersTable = new CharactersTable(Table.Table1);
            CharsToSkip = new List<char>();
            CharactersToRemove = new List<string>()
            {
                "!",
                ".",
                ",",
                "?",
                "/",
                "\"",
                "\\",
                "”",
                "“",
               //"’",
                //"'",
                "{",
                "}",
                "[",
                "]",
                "(",
                ")",
                "<",
                ">",
                ";",
                ":",
                "~",
                //"`",
                "|",
                "*",
                "@",
                "#",
                "$",
                "%",
                "^",
                "&",
                "+",
                "=",
                "—",
                "_",
                "…",
                "«",
                "»",
                "̑",
                "҂҃",
                "҃",
                "҂",
                "–",
                "„",
                "№"
            };
        }
    }
}
