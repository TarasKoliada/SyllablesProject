using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Sklady.Export;
using Sklady.Models;

namespace Sklady
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        public event Action<ExportResults> OnFilesProcessed;
        private const int UPDATE_UI_EVERY_N_ITEMS = 2000;

        private CharactersTable charsTable = GlobalSettings.CharactersTable;
        private ResultsExporter _export; 

        private List<InputFileModel> _inputData;
        public List<InputFileModel> InputData
        {
            get
            {
                return _inputData;
            }
            set
            {
                if (value != null)
                {
                    _inputData = value;
                    OnInputDataChanged();
                }                
            }
        }

        private void OnInputDataChanged()
        {
            progressBar1.Value = 0;
            CreateProgressBars();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (InputData == null || !InputData.Any())
            {
                MessageBox.Show("No text file selected.");
                return;
            }
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

            _export = new ResultsExporter(settings);

            bool[] checkboxArr = new bool[5];
            checkboxArr[0] = GetCheckboxValue();
            checkboxArr[1] = GetCheckboxValue2();
            checkboxArr[2] = GetCheckboxValue3();
            checkboxArr[3] = GetCheckboxValue4();
            checkboxArr[4] = GetCheckboxValue5();

            var analyzers = new List<TextAnalyzer>();
            for (var i = 0; i < InputData.Count; i++)
            {
                var textAnalyzer = new TextAnalyzer(InputData[i].Text, InputData[i].FileName, settings, _export, checkboxArr);

                textAnalyzer.OnWordAnalyzed += Analyzer_OnWordAnalyzed;
                textAnalyzer.OnErrorOccured += Analyzer_OnErrorOccured;

                analyzers.Add(textAnalyzer);
            }

            progressBar1.Value = 0;

            var exportResult = new ExportResults();
            var fileProcessingResults = new List<FileProcessingResult>();

            var task = Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(analyzers, textAnalyzer =>
                {
                    var res = textAnalyzer.GetResults(checkboxArr);
                    fileProcessingResults.Add(res);
                    
                    UpdateProcessingPanel(true);
                  
                    exportResult.FileExportResults.Add(new FileExportResults()
                    {
                        Syllables = _export.GetSyllables(res.ReadableResults),
                        FirstSyllables = _export.GetFirstSyllables(res.ReadableResults),
                        SyllablesCVV = _export.GetSyllablesCVV(res.CvvResults),
                        SyllablesFirstCVV = _export.GetSyllablesFirstCVV(res.CvvResults),
                        FileName = textAnalyzer.FileName
                    });
                    UpdateMainProgressBar(analyzers.Count);
                });

                exportResult.StatisticsTableCsv = _export.GetStatisticsTableCsv(fileProcessingResults);

                UpdateProcessingPanel(false);

                if (OnFilesProcessed != null)
                    OnFilesProcessed(exportResult);
            });         

            
        }

        private void UpdateProcessingPanel(bool visible)
        {
            if (processingPanel.InvokeRequired)
            {
                processingPanel.Invoke((MethodInvoker)delegate ()
                {
                    processingPanel.Visible = visible;
                });
            }
            else
            {
                processingPanel.Visible = visible;
            }
        }

        private void OnFileProcessed()
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke((MethodInvoker)delegate ()
                {
                    progressBar1.Value += 1;
                });
            }
            else
            {
                progressBar1.Value += 1;
            }
        }

        private void CreateProgressBars()
        {
            panel1.Controls.Clear();

            for (var i = 0; i < InputData.Count; i++)
            {
                var item = InputData[i];

                var label = new Label();
                label.Text = item.FileName;
                label.Top = 15 + i * 30;
                label.Left = 20;
                label.Height = 15;
                label.Name = item.FileName + "lbl";

                var progressBar = new ProgressBar();
                progressBar.Top = 15 + i * 30;
                progressBar.Left = 170;
                progressBar.Height = 15;
                progressBar.Name = item.FileName + "pb";

                panel1.Controls.Add(label);
                panel1.Controls.Add(progressBar);
            }

            button1.PerformClick();
        }

        private void Analyzer_OnErrorOccured(Exception arg1, string word, string file)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.BeginInvoke((MethodInvoker)delegate ()
                {
                    richTextBox1.Text += String.Format("{0} Error occured processing next word - {1}\n", file, word);
                });
            }
            else
            {
                richTextBox1.Text += String.Format("{0} Error occured processing next word - {1}\n", file, word);
            }
        }

        private void Analyzer_OnWordAnalyzed(int current, int total, string fileName)
        {
            var progressBar = (ProgressBar)panel1.Controls.Find(fileName + "pb", false).First();

            UpdateProgressBar(current, total, progressBar);
        }

        private void UpdateProgressBar(int current, int total, ProgressBar progressBar)
        {
            if (current % UPDATE_UI_EVERY_N_ITEMS != 0 && total - current > UPDATE_UI_EVERY_N_ITEMS)
            {
                return;
            }

            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke((MethodInvoker)delegate ()
                {
                    progressBar.Maximum = total;
                    progressBar.Value = current;
                });
            }
            else
            {
                progressBar.Maximum = total;
                progressBar.Value = current;
            }
        }

        private void MainView_Load(object sender, EventArgs e)
        {
           
        }

        private void UpdateMainProgressBar(int total)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke((MethodInvoker)delegate ()
                {
                    progressBar1.Maximum = total;
                    progressBar1.Value += 1;
                });
            }
            else
            {
                progressBar1.Maximum = total;
                progressBar1.Value += 1;
            }
        }

        public bool GetCheckboxValue ()
        {
            return checkBox1.Checked;
        }

        public bool GetCheckboxValue2 ()
        {
            return checkBox2.Checked;
        }

        public bool GetCheckboxValue3 ( )
        {
            return checkBox3.Checked;
        }

        //checked if ę=е

        public bool GetCheckboxValue4 ( )
        {
            return checkBox4.Checked;
        }

        //checked if ą=о
        public bool GetCheckboxValue5 ( )
        {
            return checkBox5.Checked;
        }

    }
}
