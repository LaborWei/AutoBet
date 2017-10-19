using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
namespace BetSetup
{
    public partial class FrmMyPeriod : Form
    {
        List<Model.ModelBet> lstModelBet;
        int MyPeriodId;
        IBetForm frmBet;
        public FrmMyPeriod()
        {
            InitializeComponent();
        }

        public FrmMyPeriod(IBetForm frmBet, List<ModelBet> lstModelBet, int MyPeriodId)
        {
            InitializeComponent();
            this.lstModelBet = lstModelBet;
            this.MyPeriodId = MyPeriodId;
            this.frmBet = frmBet;
        }       

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMyPeriod_Load(object sender, EventArgs e)
        {
            ScatterData();
        }

        private void ScatterData()
        {
            ModelBet modelbet = lstModelBet.Find(p => p.MyPeriodId == this.MyPeriodId);

            TxtPeriodDescription.Text = modelbet.MyPeriodDescription;
            TxtMyBetAmountPerBet.Text = modelbet.MyBetAmountPerBet;
            //TxtMyBetNumber.Text = modelbet.MyBetNumber;
            TxtMyBetTotalAmount.Text = modelbet.MyBetTotalAmount;

            //TxtBetNumber.Text = modelbet.MyBetNumber.Replace(",", "").Length.ToString();
            RefreshControl();
        }

        private void AssignData()
        {
            ModelBet modelbet = lstModelBet.Find(p => p.MyPeriodId == this.MyPeriodId);

            TxtPeriodDescription.Text = modelbet.MyPeriodDescription;
            modelbet.MyBetAmountPerBet = TxtMyBetAmountPerBet.Text;
            //modelbet.MyBetNumber = TxtMyBetNumber.Text;
            modelbet.MyBetTotalAmount = TxtMyBetTotalAmount.Text;
            RefreshControl();
        }


        private void RefreshControl()
        {
            BtnPre.Enabled = this.MyPeriodId == 1 ? false : true;
            BtnNext.Enabled = this.MyPeriodId == this.lstModelBet.Count ? false : true;            
        }

        private void BtnPre_Click(object sender, EventArgs e)
        {
            AssignData();
            this.MyPeriodId--;
            ScatterData();
            frmBet.RefreshMyBet();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            AssignData();
            this.MyPeriodId++;
            ScatterData();
            frmBet.RefreshMyBet();
        }

        private void BtnBet_Click(object sender, EventArgs e)
        {
            //string strBet = "";

            //strBet += Chb10000.Checked ? "0" : "";
            //strBet += Chb10001.Checked ? "1" : "";
            //strBet += Chb10002.Checked ? "2" : "";
            //strBet += Chb10003.Checked ? "3" : "";
            //strBet += Chb10004.Checked ? "4" : "";
            //strBet += Chb10005.Checked ? "5" : "";
            //strBet += Chb10006.Checked ? "6" : "";
            //strBet += Chb10007.Checked ? "7" : "";
            //strBet += Chb10008.Checked ? "8" : "";
            //strBet += Chb10009.Checked ? "9" : "";

            //strBet += ",";

            //strBet += Chb1000.Checked ? "0" : "";
            //strBet += Chb1001.Checked ? "1" : "";
            //strBet += Chb1002.Checked ? "2" : "";
            //strBet += Chb1003.Checked ? "3" : "";
            //strBet += Chb1004.Checked ? "4" : "";
            //strBet += Chb1005.Checked ? "5" : "";
            //strBet += Chb1006.Checked ? "6" : "";
            //strBet += Chb1007.Checked ? "7" : "";
            //strBet += Chb1008.Checked ? "8" : "";
            //strBet += Chb1009.Checked ? "9" : "";

            //strBet += ",";

            //strBet += Chb100.Checked ? "0" : "";
            //strBet += Chb101.Checked ? "1" : "";
            //strBet += Chb102.Checked ? "2" : "";
            //strBet += Chb103.Checked ? "3" : "";
            //strBet += Chb104.Checked ? "4" : "";
            //strBet += Chb105.Checked ? "5" : "";
            //strBet += Chb106.Checked ? "6" : "";
            //strBet += Chb107.Checked ? "7" : "";
            //strBet += Chb108.Checked ? "8" : "";
            //strBet += Chb109.Checked ? "9" : "";

            //strBet += ",";

            //strBet += Chb10.Checked ? "0" : "";
            //strBet += Chb11.Checked ? "1" : "";
            //strBet += Chb12.Checked ? "2" : "";
            //strBet += Chb13.Checked ? "3" : "";
            //strBet += Chb14.Checked ? "4" : "";
            //strBet += Chb15.Checked ? "5" : "";
            //strBet += Chb16.Checked ? "6" : "";
            //strBet += Chb17.Checked ? "7" : "";
            //strBet += Chb18.Checked ? "8" : "";
            //strBet += Chb19.Checked ? "9" : "";

            //strBet += ",";

            //strBet += Chb0.Checked ? "0" : "";
            //strBet += Chb1.Checked ? "1" : "";
            //strBet += Chb2.Checked ? "2" : "";
            //strBet += Chb3.Checked ? "3" : "";
            //strBet += Chb4.Checked ? "4" : "";
            //strBet += Chb5.Checked ? "5" : "";
            //strBet += Chb6.Checked ? "6" : "";
            //strBet += Chb7.Checked ? "7" : "";
            //strBet += Chb8.Checked ? "8" : "";
            //strBet += Chb9.Checked ? "9" : "";


            //TxtMyBetNumber.Text = strBet;
            //TxtBetNumber.Text = TxtMyBetNumber.Text.Replace(",", "").Length.ToString();
            //if (TxtBetNumber.Text == "0")
            //{
            //    MessageBox.Show("请选择投注号码");
            //    TxtMyBetNumber.Text = "";
            //    TxtBetNumber.Text = "0";
            //    TxtMyBetTotalAmount.Text = "0";
            //    return;
            //}
            //TxtMyBetTotalAmount.Text = (decimal.Parse(TxtMyBetAmountPerBet.Text) * int.Parse(TxtBetNumber.Text)).ToString("N");

            //AssignData();
            //frmBet.RefreshMyBet();
        }

        private void TxtMyBetAmountPerBet_ValueChanged(object sender, EventArgs e)
        {
            TxtMyBetTotalAmount.Text = TxtMyBetAmountPerBet.Text;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            AssignData();
            frmBet.RefreshMyBet();
        }
    }
}
