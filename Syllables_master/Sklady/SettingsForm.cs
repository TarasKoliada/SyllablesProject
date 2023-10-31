using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sklady
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            tbSeparator.Text = GlobalSettings.SyllableSeparator;
            cbSeparationMode.SelectedIndex = GlobalSettings.SeparateAfterFirst ? 0 : 1;
            cbCharactersTable.SelectedIndex = (int) GlobalSettings.CharactersTable.SelectedTable;
            cbPhoneticsMode.Checked = GlobalSettings.PhoneticsMode;
            cbbLanguage.DataSource = new BindingList<string>(Enum.GetNames(typeof(Languages)));
            cbbLanguage.SelectedIndex = (int)GlobalSettings.Language;
            cbAbsoluteMeasures.Checked = GlobalSettings.AbsoluteMeasures;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalSettings.SyllableSeparator = tbSeparator.Text;
            GlobalSettings.SeparateAfterFirst = cbSeparationMode.SelectedIndex == 0;
            GlobalSettings.CharactersTable.SelectedTable = (Table) cbCharactersTable.SelectedIndex;
            GlobalSettings.PhoneticsMode = cbPhoneticsMode.Checked;
            GlobalSettings.Language = (Languages)cbbLanguage.SelectedIndex;
            GlobalSettings.AbsoluteMeasures = cbAbsoluteMeasures.Checked;

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbCharactersTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
