using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using BetSetup;
using System.IO;

namespace BetAnalysis
{
    public partial class FrmBetAnalysis : Form, IBetForm
    {
        List<ModelBet> lstBet1 = new List<ModelBet>();
        List<ModelBet> lstBet2 = new List<ModelBet>();
        List<ModelBet> lstBet3 = new List<ModelBet>();
        List<ModelBet> lstBet4 = new List<ModelBet>();
        List<ModelBet> lstBet5 = new List<ModelBet>();
        List<ModelBet> lstBet6 = new List<ModelBet>();
        List<ModelBetState> lstBetState = new List<ModelBetState>();

        List<ModelSSCPeriods> lstSSCPeriods = new List<ModelSSCPeriods>();

        List<string> lstPeriodsDateFrom = new List<string>();
        List<string> lstPeriodsDateTo = new List<string>();
        List<string> lstPeriodsNoFrom = new List<string>();
        List<string> lstPeriodsNoTo = new List<string>();

        int iMaxSetupPlanNo;

        public FrmBetAnalysis()
        {
            InitializeComponent();
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dr = ofdFile.ShowDialog();
            if (dr == DialogResult.OK)
            {
                TxtSSCFile.Text = ofdFile.FileName;
                ScatterSSC(TxtSSCFile.Text);
            }
        }

        private void FrmBetAnalysis_Load(object sender, EventArgs e)
        {
            TxtSSCFile.AllowDrop = true;
            this.TxtSSCFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.TxtFile_DragDrop);
            this.TxtSSCFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.TxtFile_DragEnter);

            LogInfo.ReadBetSetup(ref lstBet1, ref lstBet2, ref lstBet3, ref lstBet4, ref lstBet5, ref lstBet6);
            RefreshMyBet();
            GrvBetState.AutoGenerateColumns = false;

            RefreshGridState();

            LblStatus.Text = "";

            this.RdbPlan1.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
            this.RdbPlan2.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
            this.RdbPlan3.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
            this.RdbPlan4.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
            this.RdbPlan5.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
            this.RdbPlan6.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
        }

        private void TxtFile_DragDrop(object sender, DragEventArgs e)
        {
            ((TextBox)sender).Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            ScatterSSC(TxtSSCFile.Text);
        }

        private void TxtFile_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        public void RefreshMyBet()
        {
            List<ModelBet> lstBet;
            if (RdbPlan1.Checked) { lstBet = lstBet1; }
            else if (RdbPlan2.Checked) { lstBet = lstBet2; }
            else if (RdbPlan3.Checked) { lstBet = lstBet3; }
            else if (RdbPlan4.Checked) { lstBet = lstBet4; }
            else if (RdbPlan5.Checked) { lstBet = lstBet5; }
            else { lstBet = lstBet6; }

            //GrvMyBet.DataSource = new List<ModelBet>();
            GrvMyBet.DataSource = new BindingList<ModelBet>(lstBet);

            if (lstBet.Count > 0)
            {
                BtnEdit.Enabled = true;
                BtnDel.Enabled = true;
            }
            else
            {
                BtnEdit.Enabled = false;
                BtnDel.Enabled = false;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ModelBet modelbet = new ModelBet();
            List<ModelBet> lstBet;
            if (RdbPlan1.Checked) { lstBet = lstBet1; modelbet.MyPlanId = 1; }
            else if (RdbPlan2.Checked) { lstBet = lstBet2; modelbet.MyPlanId = 2; }
            else if (RdbPlan3.Checked) { lstBet = lstBet3; modelbet.MyPlanId = 3; }
            else if (RdbPlan4.Checked) { lstBet = lstBet4; modelbet.MyPlanId = 4; }
            else if (RdbPlan5.Checked) { lstBet = lstBet5; modelbet.MyPlanId = 5; }
            else { lstBet = lstBet6; modelbet.MyPlanId = 6; }

            modelbet.MyPeriodId = lstBet.Count + 1;
            modelbet.MyBetAmountPerBet = "0";
            //modelbet.MyBetNumber = "";
            modelbet.MyBetTotalAmount = "0";
            
            lstBet.Add(modelbet);
            RefreshMyBet();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            List<ModelBet> lstBet;
            if (RdbPlan1.Checked) { lstBet = lstBet1; }
            else if (RdbPlan2.Checked) { lstBet = lstBet2; }
            else if (RdbPlan3.Checked) { lstBet = lstBet3; }
            else if (RdbPlan4.Checked) { lstBet = lstBet4; }
            else if (RdbPlan5.Checked) { lstBet = lstBet5; }
            else { lstBet = lstBet6; }

            if (lstBet.Count == 0)
                return;

            lstBet.RemoveAt(lstBet.Count - 1);
            RefreshMyBet();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            List<ModelBet> lstBet;
            if (RdbPlan1.Checked) { lstBet = lstBet1; }
            else if (RdbPlan2.Checked) { lstBet = lstBet2; }
            else if (RdbPlan3.Checked) { lstBet = lstBet3; }
            else if (RdbPlan4.Checked) { lstBet = lstBet4; }
            else if (RdbPlan5.Checked) { lstBet = lstBet5; }
            else { lstBet = lstBet6; }

            if (lstBet.Count == 0)
            {
                MessageBox.Show("请添加期数");
                BtnAdd.Focus();
                return;
            }

            if (GrvMyBet.CurrentCell == null)
            {
                MessageBox.Show("请选择期数");
                GrvMyBet.Focus();
                return;
            }
            BetSetup.FrmMyPeriod frm = new BetSetup.FrmMyPeriod(this, lstBet, int.Parse(GrvMyBet.Rows[GrvMyBet.CurrentCell.RowIndex].Cells["MyPeriodId"].Value.ToString()));
            frm.ShowDialog();
        }

        private void RdbPlan_CheckedChanged(object sender, EventArgs e)
        {
            RefreshMyBet();
        }

        private void FrmBetAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogInfo.WriteBetSetup(lstBet1, lstBet2, lstBet3, lstBet4, lstBet5, lstBet6);
        }

        private void RefreshGridState()
        {
            GrvBetState.DataSource = new BindingList<ModelBetState>(lstBetState.OrderByDescending(p => p.Period).ToList());
        }

        private void ScatterSSC(String strFilePath)
        {
            StreamReader sr = File.OpenText(strFilePath);
            lstSSCPeriods.Clear();
            lstPeriodsDateFrom.Clear();
            lstPeriodsDateTo.Clear();
            lstPeriodsNoFrom.Clear();
            lstPeriodsNoTo.Clear();
            int i = 0;
            String str = "";
            while ((str = sr.ReadLine()) != null)
            {
                i++;
                ModelSSCPeriods period = new ModelSSCPeriods();
                period.PeriodDate = str.Substring(0, 8);
                period.PeriodNo = str.Substring(9, 3);
                period.Period = period.PeriodDate + period.PeriodNo;    //有20开头            
                period.No5 = int.Parse(str.Substring(13, 1));
                period.No4 = int.Parse(str.Substring(14, 1));
                period.No3 = int.Parse(str.Substring(15, 1));
                period.No2 = int.Parse(str.Substring(16, 1));
                period.No1 = int.Parse(str.Substring(17, 1));	        //第二个参数 和java 的substring 不一样   		
                period.DrawingNumber = str.Substring(13, 5);
                lstSSCPeriods.Add(period);
            }
            lstSSCPeriods.Reverse();
            lstPeriodsDateFrom = lstSSCPeriods.Select(x => x.PeriodDate).Distinct().ToList();
            lstPeriodsDateTo = lstSSCPeriods.Select(x => x.PeriodDate).Distinct().ToList();
            lstPeriodsNoFrom = lstSSCPeriods.Select(x => x.PeriodNo).Distinct().ToList();
            lstPeriodsNoTo = lstSSCPeriods.Select(x => x.PeriodNo).Distinct().ToList(); 
            sr.Close();            
            CbbPeriodDateFrom.DataSource = lstPeriodsDateFrom;
            CbbPeriodDateTo.DataSource = lstPeriodsDateTo;
            CbbPeriodNoFrom.DataSource = lstPeriodsNoFrom;
            CbbPeriodNoTo.DataSource = lstPeriodsNoTo;
            LblStatus.Text = "Success";
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            lstBetState.Clear();
            LblMaxReset.Text = "";

            if (lstBet6.Count > 0 && lstBet5.Count > 0) {  iMaxSetupPlanNo = 6; }
            else if(lstBet6.Count == 0 && lstBet5.Count > 0) { iMaxSetupPlanNo = 5; }
            else if (lstBet5.Count > 0 && lstBet4.Count > 0) { iMaxSetupPlanNo = 4; }
            else if (lstBet4.Count > 0 && lstBet3.Count > 0) { iMaxSetupPlanNo = 3; }
            else if (lstBet3.Count > 0 && lstBet2.Count > 0) { iMaxSetupPlanNo = 2; }
            else if (lstBet3.Count > 0 && lstBet2.Count > 0) { iMaxSetupPlanNo = 1; }
            
            //range
            ProcessData();
            RefreshGridState();
            LblTotalAmount.Text= lstBetState.Sum(p => p.TotalAmount).ToString();
        }

        private void ProcessData()
        {
           // s1.CompareTo(s2)>0
            string strPeriodFrom = CbbPeriodDateFrom.Text + CbbPeriodNoFrom.Text;
            string strPeriodTo = CbbPeriodDateTo.Text + CbbPeriodNoTo.Text;

            List<ModelSSCPeriods> lstFilterSSCPeriods = lstSSCPeriods.Where(p => p.Period.CompareTo(strPeriodFrom) >= 0
                            && p.Period.CompareTo(strPeriodTo) <= 0).ToList();

            //current bet 保存状态
            ModelCurrentBetState modelCurrentBetState = new ModelCurrentBetState() 
            { 
                Bet0PlanID =1, Bet0PeriodID =1,
                Bet1PlanID = 1, Bet1PeriodID = 1,
                Bet2PlanID = 1, Bet2PeriodID = 1,
                Bet3PlanID = 1, Bet3PeriodID = 1,
                Bet4PlanID = 1, Bet4PeriodID = 1,
                Bet5PlanID = 1, Bet5PeriodID = 1,
                Bet6PlanID = 1, Bet6PeriodID = 1,
                Bet7PlanID = 1, Bet7PeriodID = 1,
                Bet8PlanID = 1, Bet8PeriodID = 1,
                Bet9PlanID = 1, Bet9PeriodID = 1,
            };

            ModelCurrentBetState modelNextBetState = new ModelCurrentBetState();

     //       GetBetAmount(modelCurrentBetState);
            //
            for (int i = 0; i < lstFilterSSCPeriods.Count; i++)
            {                 
                string strPeriod = lstFilterSSCPeriods[i].Period.Substring(2, lstFilterSSCPeriods[i].Period.Length - 2);                
                //process
                ModelBetState modelBetState = new ModelBetState()
                {
                    Period = strPeriod,
                    DrawingNumber = lstFilterSSCPeriods[i].DrawingNumber,
                    Bet0PlanID = modelCurrentBetState.Bet0PlanID,
                    Bet1PlanID = modelCurrentBetState.Bet1PlanID,
                    Bet2PlanID = modelCurrentBetState.Bet2PlanID,
                    Bet3PlanID = modelCurrentBetState.Bet3PlanID,
                    Bet4PlanID = modelCurrentBetState.Bet4PlanID,
                    Bet5PlanID = modelCurrentBetState.Bet5PlanID,
                    Bet6PlanID = modelCurrentBetState.Bet6PlanID,
                    Bet7PlanID = modelCurrentBetState.Bet7PlanID,
                    Bet8PlanID = modelCurrentBetState.Bet8PlanID,
                    Bet9PlanID = modelCurrentBetState.Bet9PlanID,
                    Bet0PeriodID = modelCurrentBetState.Bet0PeriodID,
                    Bet1PeriodID = modelCurrentBetState.Bet1PeriodID,
                    Bet2PeriodID = modelCurrentBetState.Bet2PeriodID,
                    Bet3PeriodID = modelCurrentBetState.Bet3PeriodID,
                    Bet4PeriodID = modelCurrentBetState.Bet4PeriodID,
                    Bet5PeriodID = modelCurrentBetState.Bet5PeriodID,
                    Bet6PeriodID = modelCurrentBetState.Bet6PeriodID,
                    Bet7PeriodID = modelCurrentBetState.Bet7PeriodID,
                    Bet8PeriodID = modelCurrentBetState.Bet8PeriodID,
                    Bet9PeriodID = modelCurrentBetState.Bet9PeriodID,
                    Bet0Amount = "",
                    Bet1Amount = "",
                    Bet2Amount = "",
                    Bet3Amount = "",
                    Bet4Amount = "",
                    Bet5Amount = "",
                    Bet6Amount = "",
                    Bet7Amount = "",
                    Bet8Amount = "",
                    Bet9Amount = ""
                };

                lstBetState.Add(modelBetState);

                AddPeriod(modelCurrentBetState, modelNextBetState, lstFilterSSCPeriods[i], lstFilterSSCPeriods, i);
                GetBetAmount(modelBetState);
                modelCurrentBetState = modelNextBetState;
                CommonFunction.GetTotal(strPeriod, lstBetState);
            }            
            //filterdata
            //
            //
        }

        private void GetBetAmount(ModelBetState modelBetState)
        {
            List<ModelBet> lsttempModelBet;

            Type t = modelBetState.GetType();

            for (int i = 0; i < 10; i++)
            { 
                System.Reflection.PropertyInfo propertyInfoPlan = t.GetProperty("Bet" + i + "PlanID");
                System.Reflection.PropertyInfo propertyInfoPeriod = t.GetProperty("Bet" + i + "PeriodID"); 
                System.Reflection.PropertyInfo propertyInfoAmount = t.GetProperty("Bet" + i + "Amount");


                if ((int)propertyInfoPlan.GetValue(modelBetState) == 1)
                {
                    lsttempModelBet = lstBet1;
                }
                else if ((int)propertyInfoPlan.GetValue(modelBetState) == 2)
                {
                    lsttempModelBet = lstBet2;
                }
                else if ((int)propertyInfoPlan.GetValue(modelBetState) == 3)
                {
                    lsttempModelBet = lstBet3;
                }
                else if ((int)propertyInfoPlan.GetValue(modelBetState) == 4)
                {
                    lsttempModelBet = lstBet4;
                }
                else if ((int)propertyInfoPlan.GetValue(modelBetState) == 5)
                {
                    lsttempModelBet = lstBet5;
                }
                else if ((int)propertyInfoPlan.GetValue(modelBetState) == 6)
                {
                    lsttempModelBet = lstBet6;
                }
                else
                {
                    lsttempModelBet = new List<ModelBet>();
                }

                ModelBet modetempbet = lsttempModelBet.Find(p => p.MyPeriodId == (int)propertyInfoPeriod.GetValue(modelBetState));

                if (modetempbet != null)
                {
                    //modelCurrentBetState.Bet0Amount = modetempbet.MyBetAmountPerBet;
                    propertyInfoAmount.SetValue(modelBetState, modetempbet.MyBetAmountPerBet.ToString());
                }
            
            }
        }

        private void AddPeriod(ModelCurrentBetState modelCurrentBetState, ModelCurrentBetState modelNextBetState, 
                    ModelSSCPeriods modelSSCPeriods, List<ModelSSCPeriods> lstTempmodelSSCPeriods, int iPeriodIndex)
        { 
            //+1
            //Bet0PlanID
            //Bet0PeriodID
            //modelCurrentBetState.Bet0PeriodID += 1;
            //modelCurrentBetState.Bet1PeriodID += 1;
            //modelCurrentBetState.Bet2PeriodID += 1;
            //modelCurrentBetState.Bet3PeriodID += 1;
            //modelCurrentBetState.Bet4PeriodID += 1;
            //modelCurrentBetState.Bet5PeriodID += 1;
            //modelCurrentBetState.Bet6PeriodID += 1;
            //modelCurrentBetState.Bet7PeriodID += 1;
            //modelCurrentBetState.Bet8PeriodID += 1;
            //modelCurrentBetState.Bet9PeriodID += 1;
            bool bReset = false;
            if (iPeriodIndex == lstTempmodelSSCPeriods.Count - 1)
            {
                bReset = false;
            }
            else
            {
                if (RdbNoReset.Checked == true)
                {
                    bReset = false;
                }
                else if (RdbResetByDay.Checked == true)
                { 
                    //20170101
                    if (lstTempmodelSSCPeriods[iPeriodIndex].PeriodDate.Substring(6, 2) != lstTempmodelSSCPeriods[iPeriodIndex + 1].PeriodDate.Substring(6, 2))
                    {
                        bReset = true;
                    }
                }
                else if (RdbResetByMonth.Checked == true)
                {
                    if (lstTempmodelSSCPeriods[iPeriodIndex].PeriodDate.Substring(4, 2) != lstTempmodelSSCPeriods[iPeriodIndex + 1].PeriodDate.Substring(4, 2))
                    {
                        bReset = true;
                    }
                }
                else if (RdbResetByYear.Checked == true)
                {
                    if (lstTempmodelSSCPeriods[iPeriodIndex].PeriodDate.Substring(0, 4) != lstTempmodelSSCPeriods[iPeriodIndex + 1].PeriodDate.Substring(0, 4))
                    {
                        bReset = true;
                    }
                }
            }

            List<ModelBet> lsttempCurrentModelBet;
            List<ModelBet> lsttempNextModelBet;

            Type tcurrent = modelCurrentBetState.GetType();
            Type tnext = modelNextBetState.GetType();

            //0  -- 9
            for (int i = 0; i < 10; i++)
            {
                System.Reflection.PropertyInfo propertyInfoPlan = tcurrent.GetProperty("Bet" + i + "PlanID");
                System.Reflection.PropertyInfo propertyInfoPeriod = tcurrent.GetProperty("Bet" + i + "PeriodID");
                System.Reflection.PropertyInfo propertyInfoAmount = tcurrent.GetProperty("Bet" + i + "Amount");

                System.Reflection.PropertyInfo propertyInfoNextPlan = tnext.GetProperty("Bet" + i + "PlanID");
                System.Reflection.PropertyInfo propertyInfoNextPeriod = tnext.GetProperty("Bet" + i + "PeriodID");
                System.Reflection.PropertyInfo propertyInfoNextAmount = tnext.GetProperty("Bet" + i + "Amount");

                int icurrentplan = (int)propertyInfoPlan.GetValue(modelCurrentBetState);
                int icurrentperiod = (int)propertyInfoPeriod.GetValue(modelCurrentBetState);
                //plan
                if ( icurrentplan== 1)
                {
                    lsttempCurrentModelBet = lstBet1;
                    lsttempNextModelBet = lstBet2;
                }
                else if (icurrentplan == 2)
                {
                    lsttempCurrentModelBet = lstBet2;
                    lsttempNextModelBet = lstBet3;
                }
                else if (icurrentplan == 3)
                {
                    lsttempCurrentModelBet = lstBet3;
                    lsttempNextModelBet = lstBet4;
                }
                else if (icurrentplan == 4)
                {
                    lsttempCurrentModelBet = lstBet4;
                    lsttempNextModelBet = lstBet5;
                }
                else if (icurrentplan == 5)
                {
                    lsttempCurrentModelBet = lstBet5;
                    lsttempNextModelBet = lstBet6;
                }
                else if (icurrentplan == 6)
                {
                    lsttempCurrentModelBet = lstBet6;
                    lsttempNextModelBet = lstBet1;
                }
                else
                {
                    lsttempCurrentModelBet = new List<ModelBet>();
                    lsttempNextModelBet = new List<ModelBet>();
                }

                if (modelSSCPeriods.No5 == i)//中
                {
                    if ((int)propertyInfoPeriod.GetValue(modelCurrentBetState) == 0)
                    {//
                        if (icurrentplan == iMaxSetupPlanNo + 1)  //最后一个，重来
                        {
                            propertyInfoNextPlan.SetValue(modelNextBetState, 1);
                            propertyInfoNextPeriod.SetValue(modelNextBetState, 1);

                            LblMaxReset.Text = LblMaxReset.Text + modelSSCPeriods.Period + ",";
                        }
                        else
                        {
                            propertyInfoNextPlan.SetValue(modelNextBetState, icurrentplan+ 1); //+1
                            propertyInfoNextPeriod.SetValue(modelNextBetState, 1);
                        }
                    }
                    else
                    {
                        propertyInfoNextPlan.SetValue(modelNextBetState, 1);
                        propertyInfoNextPeriod.SetValue(modelNextBetState, 1);
                    }
                }
                else
                {
                    if (icurrentperiod == 0)
                    {
                        propertyInfoNextPlan.SetValue(modelNextBetState, icurrentplan);
                        propertyInfoNextPeriod.SetValue(modelNextBetState, 0);
                    }
                    else if (icurrentperiod == lsttempCurrentModelBet.Count)
                    {
                        propertyInfoNextPlan.SetValue(modelNextBetState, icurrentplan);
                        propertyInfoNextPeriod.SetValue(modelNextBetState, 0);
                        
                    }
                    else
                    {
                        propertyInfoNextPlan.SetValue(modelNextBetState, icurrentplan);
                        propertyInfoNextPeriod.SetValue(modelNextBetState, icurrentperiod+1);
                    }                    
                }

                if (bReset)
                { 
                    propertyInfoNextPlan.SetValue(modelNextBetState, 1);
                    propertyInfoNextPeriod.SetValue(modelNextBetState, 1);
                }

                //if (cbbPlan.Items.Count == 0 && cbbPeriod.Items.Count == 0)
                //{
                //    return;
                //}

                ////没有bContinue正常增加
                ////bContinue    cbbPeriod.Parent.ForeColor == System.Drawing.Color.Red  暂停投注 --》 plan ++
                ////            cbbPeriod.Parent.ForeColor == System.Drawing.Color.Red  不是暂停投注  --》 reset
                //if (!bContinue)
                //{
                //    if (cbbPeriod.Items.Count == cbbPeriod.SelectedIndex + 1)
                //    {
                //        if (cbbPlan.Items.Count == cbbPlan.SelectedIndex + 1)//最大方案
                //        {
                //            return;
                //        }
                //        else if (cbbPlan.Items.Count > cbbPlan.SelectedIndex + 1)//
                //        {
                //            cbbPlan.SelectedIndex++;
                //        }
                //    }
                //    else
                //    {
                //        cbbPeriod.SelectedIndex++;
                //    }
                //    return;
                //}


                //if (cbbPeriod.Items.Count == cbbPeriod.SelectedIndex + 1)//最大期数
                //{
                //    //不中 ，
                //    if (cbbPeriod.Parent.ForeColor != System.Drawing.Color.Red)
                //    {
                //        return;
                //    }

                //    //中
                //    if (cbbPlan.Items.Count == cbbPlan.SelectedIndex + 1)//最大方案         
                //    {

                //        BtnResetClick(cbbPlan, cbbPeriod);
                //        return;
                //    }
                //    else if (cbbPlan.Items.Count > cbbPlan.SelectedIndex + 1)//
                //    {
                //        cbbPlan.SelectedIndex++;
                //    }
                //    else
                //    {
                //        throw new Exception("出错 Plan");
                //    }
                //}
                //else if (cbbPeriod.Items.Count > cbbPeriod.SelectedIndex + 1)
                //{
                //    //不中 ，
                //    if (cbbPeriod.Parent.ForeColor != System.Drawing.Color.Red)
                //    {
                //        cbbPeriod.SelectedIndex++;
                //        return;
                //    }
                //    //中
                //    BtnResetClick(cbbPlan, cbbPeriod);
                //    return;
                //}
                //else
                //{
                //    throw new Exception("出错 Period");
                //}
            }

        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            int iIndex = lstBetState.FindIndex(p => p.Period == TxtFindPeriod.Text.Trim());
           // this.GrvBetState.CurrentRow = this.GrvBetState.Rows[iIndex];

            this.GrvBetState.Rows[iIndex].Selected = true;
            this.GrvBetState.CurrentCell = this.GrvBetState.Rows[iIndex].Cells[0];
        }
    }
}
