using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoSubmit
{
    public partial class FrmConfigSetup : Form
    {
        public FrmConfigSetup()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Tools.updateSeeting("URL", TxtWeb.Text.Trim());
            Tools.updateSeeting("BackupURL", TxtBackupWeb.Text.Trim());
            this.Close();
        }

        private void FrmConfigSetup_Load(object sender, EventArgs e)
        {
            TxtWeb.Text = Tools.getSetting("URL");
            TxtBackupWeb.Text = Tools.getSetting("BackupURL");            
        }
    }
}
