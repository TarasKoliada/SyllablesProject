using Sklady.Export;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Models;
using Sklady.Models;
using System.Text.RegularExpressions;
using System.Globalization;
using Sklady.TextProcessors;


namespace Sklady
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mainView1.OnFilesProcessed += MainView1_OnFilesProcessed1;
        }

        private ExportResults _exportResults;

        private void MainView1_OnFilesProcessed1(ExportResults result)
        {
            _exportResults = result;
            UpdateSaveButton(result.FileExportResults);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = GlobalSettings.LastOpenFolderPath;
            dialog.Description = "Open folder with text files.";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var path = dialog.SelectedPath;
                var di = new DirectoryInfo(path);

                var files = di.GetFiles("*.txt");

                var texts = new List<InputFileModel>();
                foreach (var file in files)
                {
                    texts.Add(new InputFileModel()
                    {
                        FileName = file.Name,
                        Text = File.ReadAllText(file.FullName, Encoding.UTF8)
                    });
                }

                mainView1.InputData = texts;
            }

            GlobalSettings.LastOpenFolderPath = dialog.SelectedPath;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string SyllablesFolderName = "Syllables";
            const string FirstSyllablesFolderName = "FirstSyllables";
            const string SyllablesCVVFolderName = "SyllablesCVV";
            const string FirstSyllablesCVVFolderName = "FirstSyllablesCVV";
            const string TranscribedToUkrainianSpelling = "Transcribed";

            var folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Save results to folder.";
            folderDialog.SelectedPath = GlobalSettings.LastSaveFolderPath;

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                var syllablesDirectory = Directory.CreateDirectory(Path.Combine(folderDialog.SelectedPath, SyllablesFolderName)).FullName;
                var syllablesFirstDirectory = Directory.CreateDirectory(Path.Combine(folderDialog.SelectedPath, FirstSyllablesFolderName)).FullName;
                var syllablesCVVDirectory = Directory.CreateDirectory(Path.Combine(folderDialog.SelectedPath, SyllablesCVVFolderName)).FullName;
                var syllablesFirstCVVDirectory = Directory.CreateDirectory(Path.Combine(folderDialog.SelectedPath, FirstSyllablesCVVFolderName)).FullName;
                var transcribedWordsDirectory = Directory.CreateDirectory(Path.Combine(folderDialog.SelectedPath, TranscribedToUkrainianSpelling)).FullName;

                foreach (var fileResult in _exportResults.FileExportResults)
                {
                    SaveFile(fileResult.Syllables, syllablesDirectory, fileResult.FileName);
                    SaveFile(fileResult.FirstSyllables, syllablesFirstDirectory, fileResult.FileName);
                    SaveFile(fileResult.SyllablesCVV, syllablesCVVDirectory, fileResult.FileName);
                    SaveFile(fileResult.SyllablesFirstCVV, syllablesFirstCVVDirectory, fileResult.FileName);
                    SaveFile(fileResult.TranscribedToUkrainianSpelling, transcribedWordsDirectory, fileResult.FileName);
                }

                SaveCvv(_exportResults.StatisticsTableCsv, folderDialog.SelectedPath);
                LoadCsv(_exportResults.StatisticsTableCsv, folderDialog.SelectedPath);
            }

            GlobalSettings.LastSaveFolderPath = folderDialog.SelectedPath;
        }

        private void SaveCvv(string statisticsTableCsv, string path)
        {
            var fullPath = Path.Combine(path, "Statistics.csv");
            File.WriteAllText(fullPath, statisticsTableCsv, Encoding.UTF8);
        }

        private void SaveFile(string result, string path, string fileName)
        {
            var fullPath = Path.Combine(path, fileName);
            File.WriteAllText(fullPath, result, Encoding.UTF8);
        }
        /*
        private void LoadCsvOld ( string statisticsTableCsv, string path )
        {
            var fullPath = Path.Combine(path, "Statistics.csv");

            //try
            //{
                string CSVFilePathName = fullPath;
                string[] Lines = File.ReadAllLines(CSVFilePathName);
                string[] Fields;
                Fields = Lines[0].Split(new char[] { '\t' });
                int Cols = 8;
                DataTable dt = new DataTable();
                for (int i = 0; i < Cols; i++)
                    dt.Columns.Add(Fields[i].ToLower(), typeof(string));
                DataRow Row;
                for (int i = 1; i < Lines.GetLength(0); i++)
                {
                    Fields = Lines[i].Split(new char[] { '\t' });
                    if (Fields.Length < Cols)
                    {
                        continue;
                    }
                    Row = dt.NewRow();
                    for (int f = 0; f < Cols; f++)
                        Row[f] = Fields[f];
                    dt.Rows.Add(Row);
                }
                dataGridView1.DataSource = dt;
        }
        */

        private void LoadCsv(string statisticsTableCsv, string path)
        {
            var fullPath = Path.Combine(path, "Statistics.csv");

            try
            {
                string CSVFilePathName = fullPath;
                string[] lines = File.ReadAllLines(CSVFilePathName);

                // Assuming the first line contains column headers
                string[] headers = lines[0].Split(new char[] { '\t' });

                List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(new char[] { '\t' });
                    if (fields.Length < headers.Length)
                    {
                        continue;
                    }

                    Dictionary<string, string> row = new Dictionary<string, string>();
                    for (int f = 0; f < headers.Length; f++)
                    {
                        row[headers[f].ToLower()] = fields[f];
                    }

                    data.Add(row);
                }

                // You can now use the 'data' list to perform further operations if needed.
                // For example, you can bind it to a DataGridView if necessary.

                // Example: Binding data to a DataGridView (if needed)
                // dataGridView1.DataSource = data.Select(d => new { /* your columns here */ }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is " + ex.ToString());
                throw;
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            saveToolStripMenuItem.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.ShowDialog();
        }

        private void charactersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new CharactersBase();
            form.ShowDialog();
        }

        private void UpdateSaveButton(List<FileExportResults> results)
        {
            if (menuStrip1.InvokeRequired)
            {
                menuStrip1.Invoke((MethodInvoker)delegate ()
               {
                   saveToolStripMenuItem.Enabled = results.Any();
               });
            }
            else
            {
                saveToolStripMenuItem.Enabled = results.Any();
            }
        }



        //word statistics english
        public void button1_Click(object sender, EventArgs e)
        {
            button3.PerformClick();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                string txt = File.ReadAllText(fileName);
                txt = txt.ToLower();
                txt = Regex.Replace(txt, @"[\d-]", string.Empty);
                // Use regular expressions to replace characters
                // that are not letters or numbers with spaces.
                Regex reg_exp = new Regex("[^a-zA-Z0-9]");
                txt = reg_exp.Replace(txt, " ");
                Regex re = new Regex(@"\b\w{0,1}\b");
                txt = re.Replace(txt, "");

                // Split the text into words.
                string[] words = txt.Split(
                new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

                // Use LINQ to get the unique words.
                var word_query =
                    (from string word in words
                     orderby word
                     select word).Distinct();

                // Display the result.
                string[] result = word_query.ToArray();


                DataGridView dataGridView2 = new DataGridView();
                this.dataGridView2.Columns.Add("word", "word");
                this.dataGridView2.Columns.Add("length", "length");
                this.dataGridView2.Columns.Add("syllables", "syllables");
                this.dataGridView2.Columns.Add("syllable length", "syllable length");


                int syllables = 0;
                //avgSylLength = Math.Round((double)result[i].Length / syllables, 4);
                //avgSylLengthPh = Math.Round((double)phonems / syllables, 4);
                for (int i = 0; i < result.Length; i++)
                {
                    syllables = Regex.Matches(result[i], @"a|e|i|o|u|y").Count;
                    if (syllables == 0)
                    {
                        syllables += 1;
                    }
                    if (result[i].Length < 5)
                    {
                        syllables = 1;
                    }
                    this.dataGridView2.Rows.Add(result[i], result[i].Length, syllables, Math.Round((double)result[i].Length / syllables, 10));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();

        }

        // word statistics for ua/ru/bg
        private void button2_Click(object sender, EventArgs e)
        {
            button4.PerformClick();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                string txt = File.ReadAllText(fileName);
                txt = txt.ToLower();
                txt = Regex.Replace(txt, @"[\d-]", string.Empty);
                // Use regular expressions to replace characters
                // that are not letters or numbers with spaces.
                Regex reg_exp = new Regex("[^а-яА-ЯєЄІіЇїъё0-9]");
                txt = reg_exp.Replace(txt, " ");
                Regex re = new Regex(@"\b\w{0,1}\b");
                txt = re.Replace(txt, "");

                // Split the text into words.
                string[] words = txt.Split(
                new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

                // Use LINQ to get the unique words.
                var word_query =
                    (from string word in words
                     orderby word
                     select word).Distinct();

                // Display the result.
                string[] result = word_query.ToArray();


                DataGridView dataGridView3 = new DataGridView();
                this.dataGridView3.Columns.Add("word", "word");
                this.dataGridView3.Columns.Add("length", "length");
                this.dataGridView3.Columns.Add("phonems", "phonems");
                this.dataGridView3.Columns.Add("syllables", "syllables");
                this.dataGridView3.Columns.Add("avg syllable length", "avg syllable length");
                this.dataGridView3.Columns.Add("avg syllable length(phonems)", "avg syllable length(phonems)");



                int syllables = 0;
                int phonems = 0;
                int phonemsReduction = 0;
                double avgSylLength = 0;
                double avgSylLengthPh = 0;

                for (int i = 0; i < result.Length; i++)
                {

                    phonems = result[i].Length + Regex.Matches(result[i], @"я|ю|є|ї|щ|ё").Count;
                    syllables = Regex.Matches(result[i], @"а|о|у|е|і|я|ю|є|ї|и|ё").Count;
                    phonemsReduction = Regex.Matches(result[i], @"ь|ъ").Count;
                    if (phonemsReduction > 0)
                    {
                        phonems -= phonemsReduction;
                    }
                    if (syllables == 0)
                    {
                        syllables += 1;
                    }
                    if (result[i].Length < 4)
                    {
                        syllables = 1;
                    }
                    avgSylLength = Math.Round((double)result[i].Length / syllables, 10);
                    avgSylLengthPh = Math.Round((double)phonems / syllables, 10);
                    this.dataGridView3.Rows.Add(result[i], result[i].Length, phonems, syllables, avgSylLength, avgSylLengthPh);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView3.Columns.Clear();
            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            exportToCSV();
        }



        private void exportToCSV()
        {
            DataGridView dg = new DataGridView();

            if (tabControl1.SelectedTab == tabPage5)
            {
                dg = dataGridView3;
            }
            else
            {
                dg = dataGridView2;
            }

            // Don't save if no data is returned
            if (dg.Rows.Count == 0)
            {
                MessageBox.Show("No data to export");
                return;
            }
            StringBuilder sb = new StringBuilder();
            // Column headers
            string columnsHeader = "";
            for (int i = 0; i < dg.Columns.Count; i++)
            {
                columnsHeader += dg.Columns[i].Name + ",";
            }
            sb.Append(columnsHeader + Environment.NewLine);
            // Go through each cell in the datagridview
            foreach (DataGridViewRow dgRow in dg.Rows)
            {
                // Make sure it's not an empty row.
                if (!dgRow.IsNewRow)
                {
                    for (int c = 0; c < dgRow.Cells.Count; c++)
                    {
                        // Append the cells data followed by a comma to delimit.

                        sb.Append(dgRow.Cells[c].Value + ",");
                    }
                    // Add a new line in the text file.
                    sb.Append(Environment.NewLine);
                }
            }
            // Load up the save file dialog with the default option as saving as a .csv file.
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // If they've selected a save location...
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false, Encoding.UTF8))
                {
                    sw.WriteLine(sb.ToString());
                }
            }
            // Confirm to the user it has been completed.
            MessageBox.Show("CSV file saved.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            exportToCSV();
        }

        //word statistics for polish
        private void button7_Click(object sender, EventArgs e)
        {
            button8.PerformClick();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                string txt = File.ReadAllText(fileName);
                txt = txt.ToLower();
                txt = Regex.Replace(txt, @"[\d-]", string.Empty);
                // Use regular expressions to replace characters
                // that are not letters or numbers with spaces.
                Regex reg_exp = new Regex("[^[a-żA-Ż0-9]");
                txt = reg_exp.Replace(txt, " ");
                Regex re = new Regex(@"\b\w{0,1}\b");
                txt = re.Replace(txt, "");

                // Split the text into words.
                string[] words = txt.Split(
                new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

                // Use LINQ to get the unique words.
                var word_query =
                    (from string word in words
                     orderby word
                     select word).Distinct();

                // Display the result.
                string[] result = word_query.ToArray();


                DataGridView dataGridView4 = new DataGridView();
                this.dataGridView4.Columns.Add("word", "word");
                this.dataGridView4.Columns.Add("length", "length");
                this.dataGridView4.Columns.Add("phonems", "phonems");
                this.dataGridView4.Columns.Add("syllables", "syllables");
                this.dataGridView4.Columns.Add("avg syllable length", "avg syllable length");
                this.dataGridView4.Columns.Add("avg syllable length(phonems)", "avg syllable length(phonems)");



                int syllables = 0;
                int phonems = 0;
                int phonemsReduction = 0;
                double avgSylLength = 0;
                double avgSylLengthPh = 0;

                for (int i = 0; i < result.Length; i++)
                {

                    phonems = result[i].Length;
                    syllables = Regex.Matches(result[i], @"a|e|i|o|u|y|ó|ę|ą").Count;
                    phonemsReduction = Regex.Matches(result[i], @"ch|cz|dz|dź|dż|rz|sz|dzi").Count;
                    if (phonemsReduction > 0)
                    {
                        phonems -= phonemsReduction;
                    }
                    if (syllables == 0)
                    {
                        syllables += 1;
                    }
                    if (result[i].Length < 4)
                    {
                        syllables = 1;
                    }
                    avgSylLength = Math.Round((double)result[i].Length / syllables, 4);
                    avgSylLengthPh = Math.Round((double)phonems / syllables, 4);
                    this.dataGridView4.Rows.Add(result[i], result[i].Length, phonems, syllables, avgSylLength, avgSylLengthPh);
                }

            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView4.Columns.Clear();
            dataGridView4.Rows.Clear();
            dataGridView4.Refresh();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mainView1_Load(object sender, EventArgs e)
        {

        }

        private void testView1_Load(object sender, EventArgs e)
        {

        }
    }
}




