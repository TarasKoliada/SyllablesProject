using System;
using System.Windows.Forms;
using Sklady.Export;
using Sklady.Models;

namespace Sklady
{
    public partial class TestView : UserControl
    {
        public TestView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();

            var settings = new Settings()
            {
                AbsoluteMeasures = GlobalSettings.AbsoluteMeasures,
                CharactersTable = GlobalSettings.CharactersTable,
                CharactersToRemove = GlobalSettings.CharactersToRemove,
                Language = GlobalSettings.Language,
                LastOpenFolderPath = GlobalSettings.LastOpenFolderPath,
                LastSaveFolderPath = GlobalSettings.LastSaveFolderPath,
                PhoneticsMode = GlobalSettings.PhoneticsMode,
                SeparateAfterFirst = GlobalSettings.SeparateAfterFirst,
                SyllableSeparator = GlobalSettings.SyllableSeparator,
                CharsToSkip = GlobalSettings.CharsToSkip
            };


            var export = new ResultsExporter(settings);
            bool[] test = new bool[1];
            var analyzer = new TextAnalyzer(richTextBox1.Text, "", settings, export, test);

            //add output in richTextBox4 
            //var transcribed = result.transcribedToUkrainianSpelling (or something else)

            var result = analyzer.GetResults(test);
            var resText = export.GetSyllables(result.ReadableResults);
            richTextBox2.Text = resText;

            //var resCVV = export.GetSyllablesCVV(result.CvvResults);
            (var resCVV, _) = export.GetSyllablesCVVUnified(result.CvvResults);
            richTextBox3.Text = resCVV;

            richTextBox4.Text = export.GetTranscribedToUkrainianText(result.TranscribedToUkrainianSpellingWords);
        }

    }
}
