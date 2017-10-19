using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using Newtonsoft.Json;
using System.Threading;
using Model;
using BetSetup;
using System.Web.Script.Serialization;
//wixiii1
namespace AutoSubmit
{
    public partial class FrmBet : Form, IBetForm
    {
        //f_test1 529600
        #region var
        List<ModelBet> lstBet1 = new List<ModelBet>();
        List<ModelBet> lstBet2 = new List<ModelBet>();
        List<ModelBet> lstBet3 = new List<ModelBet>();
        List<ModelBet> lstBet4 = new List<ModelBet>();
        List<ModelBet> lstBet5 = new List<ModelBet>();
        List<ModelBet> lstBet6 = new List<ModelBet>();

        bool bAutoBet;
        bool bFirstAutoBet;
        int RefreshCount;
        List<ModelHistory> lstModelHistory = new List<ModelHistory>();
        bool bLogin;
        bool Ajaxloading;
        bool Betloading;

        List<ModelPlan> lstPlan0 = new List<ModelPlan>();
        List<ModelPlan> lstPlan1 = new List<ModelPlan>();
        List<ModelPlan> lstPlan2 = new List<ModelPlan>();
        List<ModelPlan> lstPlan3 = new List<ModelPlan>();
        List<ModelPlan> lstPlan4 = new List<ModelPlan>();
        List<ModelPlan> lstPlan5 = new List<ModelPlan>();
        List<ModelPlan> lstPlan6 = new List<ModelPlan>();
        List<ModelPlan> lstPlan7 = new List<ModelPlan>();
        List<ModelPlan> lstPlan8 = new List<ModelPlan>();
        List<ModelPlan> lstPlan9 = new List<ModelPlan>();

        List<ModelBet> lstPlanBet0 = new List<ModelBet>();
        List<ModelBet> lstPlanBet1 = new List<ModelBet>();
        List<ModelBet> lstPlanBet2 = new List<ModelBet>();
        List<ModelBet> lstPlanBet3 = new List<ModelBet>();
        List<ModelBet> lstPlanBet4 = new List<ModelBet>();
        List<ModelBet> lstPlanBet5 = new List<ModelBet>();
        List<ModelBet> lstPlanBet6 = new List<ModelBet>();
        List<ModelBet> lstPlanBet7 = new List<ModelBet>();
        List<ModelBet> lstPlanBet8 = new List<ModelBet>();
        List<ModelBet> lstPlanBet9 = new List<ModelBet>();

        List<ModelBetState> lstBetState = new List<ModelBetState>();

        int CurrentPeriod;
        int CurrentPlan;

        private string strGlobalURL;
        private string strBackupURL;
        #endregion
                    
        public FrmBet()
        {
            InitializeComponent();
            bLogin = false;
            bAutoBet = false;
            bFirstAutoBet = false;
        }

        private void SetUrl()
        {
            strGlobalURL = Tools.getSetting("URL");
            strBackupURL = Tools.getSetting("BackupURL");
            TxtUrl.Text = strGlobalURL;
        }

        private void FrmBet_Load(object sender, EventArgs e)
        {
            ShortcutCreator.CreateShortcutOnDesktop("时时彩", Application.ExecutablePath);
            SetUrl();
            bLogin = ConfigurationManager.AppSettings["NeedLogin"].ToString()=="Y"?true:false;                                   
            
            WbbBetWeb.ScriptErrorsSuppressed = true;
            WbbBetHistoryWeb.ScriptErrorsSuppressed = true;
            WbbAjax.ScriptErrorsSuppressed = true;
            WbbBet.ScriptErrorsSuppressed = true;

            LogInfo.ReadBetSetup(ref lstBet1, ref lstBet2, ref lstBet3, ref lstBet4, ref lstBet5, ref lstBet6);
            RefreshMyBet();

            LogInfo.ReadBetHistory(ref lstModelHistory);
            RefreshHistory();

            LogInfo.ReadLogState(ref lstBetState);
            GetTotal("", "");
            RefreshGridState();
            GrvBetState.AutoGenerateColumns = false;

            //Url.ToString().Contains(strURL))

            //TimerLastPeriod.Interval = 30000;

            string strTimer = ConfigurationManager.AppSettings["Timer"].ToString();
            TimerBet.Interval = int.Parse(strTimer);
            BindEvent();

           ModelCurrentBetState modelcurrentstate = LogInfo.ReadCurrentState();

           if (modelcurrentstate != null)
           {
               CbbPlan0.SelectedIndex = modelcurrentstate.Bet0PlanID;
               CbbPlan1.SelectedIndex = modelcurrentstate.Bet1PlanID;
               CbbPlan2.SelectedIndex = modelcurrentstate.Bet2PlanID;
               CbbPlan3.SelectedIndex = modelcurrentstate.Bet3PlanID;
               CbbPlan4.SelectedIndex = modelcurrentstate.Bet4PlanID;
               CbbPlan5.SelectedIndex = modelcurrentstate.Bet5PlanID;
               CbbPlan6.SelectedIndex = modelcurrentstate.Bet6PlanID;
               CbbPlan7.SelectedIndex = modelcurrentstate.Bet7PlanID;
               CbbPlan8.SelectedIndex = modelcurrentstate.Bet8PlanID;
               CbbPlan9.SelectedIndex = modelcurrentstate.Bet9PlanID;

               CbbPeriod0.SelectedIndex = modelcurrentstate.Bet0PeriodID > CbbPeriod0.Items.Count ? CbbPeriod0.Items.Count -1 : modelcurrentstate.Bet0PeriodID;
               CbbPeriod1.SelectedIndex = modelcurrentstate.Bet1PeriodID > CbbPeriod1.Items.Count ? CbbPeriod1.Items.Count -1 : modelcurrentstate.Bet1PeriodID;
               CbbPeriod2.SelectedIndex = modelcurrentstate.Bet2PeriodID > CbbPeriod2.Items.Count ? CbbPeriod2.Items.Count -1 : modelcurrentstate.Bet2PeriodID;
               CbbPeriod3.SelectedIndex = modelcurrentstate.Bet3PeriodID > CbbPeriod3.Items.Count ? CbbPeriod3.Items.Count -1 : modelcurrentstate.Bet3PeriodID;
               CbbPeriod4.SelectedIndex = modelcurrentstate.Bet4PeriodID > CbbPeriod4.Items.Count ? CbbPeriod4.Items.Count -1 : modelcurrentstate.Bet4PeriodID;
               CbbPeriod5.SelectedIndex = modelcurrentstate.Bet5PeriodID > CbbPeriod5.Items.Count ? CbbPeriod5.Items.Count -1 : modelcurrentstate.Bet5PeriodID;
               CbbPeriod6.SelectedIndex = modelcurrentstate.Bet6PeriodID > CbbPeriod6.Items.Count ? CbbPeriod6.Items.Count -1 : modelcurrentstate.Bet6PeriodID;
               CbbPeriod7.SelectedIndex = modelcurrentstate.Bet7PeriodID > CbbPeriod7.Items.Count ? CbbPeriod7.Items.Count -1 : modelcurrentstate.Bet7PeriodID;
               CbbPeriod8.SelectedIndex = modelcurrentstate.Bet8PeriodID > CbbPeriod8.Items.Count ? CbbPeriod8.Items.Count -1 : modelcurrentstate.Bet8PeriodID;
               CbbPeriod9.SelectedIndex = modelcurrentstate.Bet9PeriodID > CbbPeriod9.Items.Count ? CbbPeriod9.Items.Count -1 : modelcurrentstate.Bet9PeriodID;
           }
           else
           {
               modelcurrentstate = new ModelCurrentBetState();
           }
        }

        private void BindEvent()
        {
            this.BtnBet0.Click += new System.EventHandler(this.BtnBet_Click);
            this.BtnBet1.Click += new System.EventHandler(this.BtnBet_Click);
            this.BtnBet2.Click += new System.EventHandler(this.BtnBet_Click);
            this.BtnBet3.Click += new System.EventHandler(this.BtnBet_Click);
            this.BtnBet4.Click += new System.EventHandler(this.BtnBet_Click);
            this.BtnBet5.Click += new System.EventHandler(this.BtnBet_Click);
            this.BtnBet6.Click += new System.EventHandler(this.BtnBet_Click);
            this.BtnBet7.Click += new System.EventHandler(this.BtnBet_Click);
            this.BtnBet8.Click += new System.EventHandler(this.BtnBet_Click);
            this.BtnBet9.Click += new System.EventHandler(this.BtnBet_Click);

            this.BtnReset0.Click += new System.EventHandler(this.BtnReset_Click);
            this.BtnReset1.Click += new System.EventHandler(this.BtnReset_Click);
            this.BtnReset2.Click += new System.EventHandler(this.BtnReset_Click);
            this.BtnReset3.Click += new System.EventHandler(this.BtnReset_Click);
            this.BtnReset4.Click += new System.EventHandler(this.BtnReset_Click);
            this.BtnReset5.Click += new System.EventHandler(this.BtnReset_Click);
            this.BtnReset6.Click += new System.EventHandler(this.BtnReset_Click);
            this.BtnReset7.Click += new System.EventHandler(this.BtnReset_Click);
            this.BtnReset8.Click += new System.EventHandler(this.BtnReset_Click);
            this.BtnReset9.Click += new System.EventHandler(this.BtnReset_Click);

            this.BtnAddPeriod0.Click += new System.EventHandler(this.BtnAddPeriod_Click);
            this.BtnAddPeriod1.Click += new System.EventHandler(this.BtnAddPeriod_Click);
            this.BtnAddPeriod2.Click += new System.EventHandler(this.BtnAddPeriod_Click);
            this.BtnAddPeriod3.Click += new System.EventHandler(this.BtnAddPeriod_Click);
            this.BtnAddPeriod4.Click += new System.EventHandler(this.BtnAddPeriod_Click);
            this.BtnAddPeriod5.Click += new System.EventHandler(this.BtnAddPeriod_Click);
            this.BtnAddPeriod6.Click += new System.EventHandler(this.BtnAddPeriod_Click);
            this.BtnAddPeriod7.Click += new System.EventHandler(this.BtnAddPeriod_Click);
            this.BtnAddPeriod8.Click += new System.EventHandler(this.BtnAddPeriod_Click);
            this.BtnAddPeriod9.Click += new System.EventHandler(this.BtnAddPeriod_Click);

            lstPlan0.Add(new ModelPlan() { MyPlanId = 0 });
            lstPlan0.Add(new ModelPlan() { MyPlanId = 1 });
            lstPlan0.Add(new ModelPlan() { MyPlanId = 2 });
            lstPlan0.Add(new ModelPlan() { MyPlanId = 3 });
            lstPlan0.Add(new ModelPlan() { MyPlanId = 4 });
            lstPlan0.Add(new ModelPlan() { MyPlanId = 5 });
            lstPlan0.Add(new ModelPlan() { MyPlanId = 6 });
            CbbPlan0.DisplayMember = "MyPlanDescription";
            CbbPlan0.ValueMember = "MyPlanId";
            CbbPlan0.DataSource = lstPlan0;


            lstPlan1.Add(new ModelPlan() { MyPlanId = 0 });
            lstPlan1.Add(new ModelPlan() { MyPlanId = 1 });
            lstPlan1.Add(new ModelPlan() { MyPlanId = 2 });
            lstPlan1.Add(new ModelPlan() { MyPlanId = 3 });
            lstPlan1.Add(new ModelPlan() { MyPlanId = 4 });
            lstPlan1.Add(new ModelPlan() { MyPlanId = 5 });
            lstPlan1.Add(new ModelPlan() { MyPlanId = 6 });
            CbbPlan1.DisplayMember = "MyPlanDescription";
            CbbPlan1.ValueMember = "MyPlanId";
            CbbPlan1.DataSource = lstPlan1;

            lstPlan2.Add(new ModelPlan() { MyPlanId = 0 });
            lstPlan2.Add(new ModelPlan() { MyPlanId = 1 });
            lstPlan2.Add(new ModelPlan() { MyPlanId = 2 });
            lstPlan2.Add(new ModelPlan() { MyPlanId = 3 });
            lstPlan2.Add(new ModelPlan() { MyPlanId = 4 });
            lstPlan2.Add(new ModelPlan() { MyPlanId = 5 });
            lstPlan2.Add(new ModelPlan() { MyPlanId = 6 });
            CbbPlan2.DisplayMember = "MyPlanDescription";
            CbbPlan2.ValueMember = "MyPlanId";
            CbbPlan2.DataSource = lstPlan2;

            lstPlan3.Add(new ModelPlan() { MyPlanId = 0 });
            lstPlan3.Add(new ModelPlan() { MyPlanId = 1 });
            lstPlan3.Add(new ModelPlan() { MyPlanId = 2 });
            lstPlan3.Add(new ModelPlan() { MyPlanId = 3 });
            lstPlan3.Add(new ModelPlan() { MyPlanId = 4 });
            lstPlan3.Add(new ModelPlan() { MyPlanId = 5 });
            lstPlan3.Add(new ModelPlan() { MyPlanId = 6 });
            CbbPlan3.DisplayMember = "MyPlanDescription";
            CbbPlan3.ValueMember = "MyPlanId";
            CbbPlan3.DataSource = lstPlan3;

            lstPlan4.Add(new ModelPlan() { MyPlanId = 0 });
            lstPlan4.Add(new ModelPlan() { MyPlanId = 1 });
            lstPlan4.Add(new ModelPlan() { MyPlanId = 2 });
            lstPlan4.Add(new ModelPlan() { MyPlanId = 3 });
            lstPlan4.Add(new ModelPlan() { MyPlanId = 4 });
            lstPlan4.Add(new ModelPlan() { MyPlanId = 5 });
            lstPlan4.Add(new ModelPlan() { MyPlanId = 6 });
            CbbPlan4.DisplayMember = "MyPlanDescription";
            CbbPlan4.ValueMember = "MyPlanId";
            CbbPlan4.DataSource = lstPlan4;

            lstPlan5.Add(new ModelPlan() { MyPlanId = 0 });
            lstPlan5.Add(new ModelPlan() { MyPlanId = 1 });
            lstPlan5.Add(new ModelPlan() { MyPlanId = 2 });
            lstPlan5.Add(new ModelPlan() { MyPlanId = 3 });
            lstPlan5.Add(new ModelPlan() { MyPlanId = 4 });
            lstPlan5.Add(new ModelPlan() { MyPlanId = 5 });
            lstPlan5.Add(new ModelPlan() { MyPlanId = 6 });
            CbbPlan5.DisplayMember = "MyPlanDescription";
            CbbPlan5.ValueMember = "MyPlanId";
            CbbPlan5.DataSource = lstPlan5;

            lstPlan6.Add(new ModelPlan() { MyPlanId = 0 });
            lstPlan6.Add(new ModelPlan() { MyPlanId = 1 });
            lstPlan6.Add(new ModelPlan() { MyPlanId = 2 });
            lstPlan6.Add(new ModelPlan() { MyPlanId = 3 });
            lstPlan6.Add(new ModelPlan() { MyPlanId = 4 });
            lstPlan6.Add(new ModelPlan() { MyPlanId = 5 });
            lstPlan6.Add(new ModelPlan() { MyPlanId = 6 });
            CbbPlan6.DisplayMember = "MyPlanDescription";
            CbbPlan6.ValueMember = "MyPlanId";
            CbbPlan6.DataSource = lstPlan6;

            lstPlan7.Add(new ModelPlan() { MyPlanId = 0 });
            lstPlan7.Add(new ModelPlan() { MyPlanId = 1 });
            lstPlan7.Add(new ModelPlan() { MyPlanId = 2 });
            lstPlan7.Add(new ModelPlan() { MyPlanId = 3 });
            lstPlan7.Add(new ModelPlan() { MyPlanId = 4 });
            lstPlan7.Add(new ModelPlan() { MyPlanId = 5 });
            lstPlan7.Add(new ModelPlan() { MyPlanId = 6 });
            CbbPlan7.DisplayMember = "MyPlanDescription";
            CbbPlan7.ValueMember = "MyPlanId";
            CbbPlan7.DataSource = lstPlan7;

            lstPlan8.Add(new ModelPlan() { MyPlanId = 0 });
            lstPlan8.Add(new ModelPlan() { MyPlanId = 1 });
            lstPlan8.Add(new ModelPlan() { MyPlanId = 2 });
            lstPlan8.Add(new ModelPlan() { MyPlanId = 3 });
            lstPlan8.Add(new ModelPlan() { MyPlanId = 4 });
            lstPlan8.Add(new ModelPlan() { MyPlanId = 5 });
            lstPlan8.Add(new ModelPlan() { MyPlanId = 6 });
            CbbPlan8.DisplayMember = "MyPlanDescription";
            CbbPlan8.ValueMember = "MyPlanId";
            CbbPlan8.DataSource = lstPlan8;

            lstPlan9.Add(new ModelPlan() { MyPlanId = 0 });
            lstPlan9.Add(new ModelPlan() { MyPlanId = 1 });
            lstPlan9.Add(new ModelPlan() { MyPlanId = 2 });
            lstPlan9.Add(new ModelPlan() { MyPlanId = 3 });
            lstPlan9.Add(new ModelPlan() { MyPlanId = 4 });
            lstPlan9.Add(new ModelPlan() { MyPlanId = 5 });
            lstPlan9.Add(new ModelPlan() { MyPlanId = 6 });
            CbbPlan9.DisplayMember = "MyPlanDescription";
            CbbPlan9.ValueMember = "MyPlanId";
            CbbPlan9.DataSource = lstPlan9;

            this.CbbPlan0.SelectedIndexChanged += new System.EventHandler(this.CbbPlan_SelectedIndexChanged);
            this.CbbPlan1.SelectedIndexChanged += new System.EventHandler(this.CbbPlan_SelectedIndexChanged);
            this.CbbPlan2.SelectedIndexChanged += new System.EventHandler(this.CbbPlan_SelectedIndexChanged);
            this.CbbPlan3.SelectedIndexChanged += new System.EventHandler(this.CbbPlan_SelectedIndexChanged);
            this.CbbPlan4.SelectedIndexChanged += new System.EventHandler(this.CbbPlan_SelectedIndexChanged);
            this.CbbPlan5.SelectedIndexChanged += new System.EventHandler(this.CbbPlan_SelectedIndexChanged);
            this.CbbPlan6.SelectedIndexChanged += new System.EventHandler(this.CbbPlan_SelectedIndexChanged);
            this.CbbPlan7.SelectedIndexChanged += new System.EventHandler(this.CbbPlan_SelectedIndexChanged);
            this.CbbPlan8.SelectedIndexChanged += new System.EventHandler(this.CbbPlan_SelectedIndexChanged);
            this.CbbPlan9.SelectedIndexChanged += new System.EventHandler(this.CbbPlan_SelectedIndexChanged);

            this.CbbPeriod0.SelectedIndexChanged += new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);
            this.CbbPeriod1.SelectedIndexChanged += new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);
            this.CbbPeriod2.SelectedIndexChanged += new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);
            this.CbbPeriod3.SelectedIndexChanged += new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);
            this.CbbPeriod4.SelectedIndexChanged += new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);
            this.CbbPeriod5.SelectedIndexChanged += new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);
            this.CbbPeriod6.SelectedIndexChanged += new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);
            this.CbbPeriod7.SelectedIndexChanged += new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);
            this.CbbPeriod8.SelectedIndexChanged += new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);
            this.CbbPeriod9.SelectedIndexChanged += new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);

            this.RdbPlan1.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
            this.RdbPlan2.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
            this.RdbPlan3.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
            this.RdbPlan4.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
            this.RdbPlan5.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
            this.RdbPlan6.CheckedChanged += new System.EventHandler(this.RdbPlan_CheckedChanged);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            RefreshCount = 1;

            WbbBetWeb.Url = new Uri(strGlobalURL);
            
            while (WbbBetWeb.ReadyState < WebBrowserReadyState.Complete) Application.DoEvents();            

            //while (WbbBetWeb.ReadyState < WebBrowserReadyState.Complete) Application.DoEvents();

            WbbBetHistoryWeb.Url = new Uri(strGlobalURL + @"/code2.aspx");

            while (WbbBetHistoryWeb.ReadyState < WebBrowserReadyState.Complete) Application.DoEvents();

            foreach (HtmlElement f in WbbBetHistoryWeb.Document.Images)
            {
                if (f.GetAttribute("src").ToLower().EndsWith("code2.aspx"))
                {
                    //将元素绝对定位到页面左上角
                    f.Style = "position: absolute; z-index: 9999; top: 0px; left: 0px";
                    //height:24px; margin-left:3px; width:80px;
                    //抓图
                    WbbBetHistoryWeb.Document.Body.ScrollIntoView(true);
                    var b = new Bitmap(f.ClientRectangle.Width, f.ClientRectangle.Height);
                    WbbBetHistoryWeb.DrawToBitmap(b, new Rectangle(new Point(), f.ClientRectangle.Size));                    
                    PibValid.Image = b;                    
                    break;
                }
            }
            //     WbbBetHistoryWeb.Navigate(strGlobalURL + @"/kaijiang/list.aspx?lot=ssc");
            //     while (WbbBetHistoryWeb.ReadyState < WebBrowserReadyState.Complete) Application.DoEvents();
            GetBalance();
            BtnModifyUrl.Enabled = false;
        }

        private void CbbPlanSelectChanged(ComboBox cbbPlan, ComboBox cbbPeriod, List<ModelBet> lst)
        {
            cbbPeriod.SelectedIndexChanged -= new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);
            cbbPeriod.SelectedIndex = -1;
            lst.Clear();
            if ((cbbPlan.Text) == "未投注") { lst.Add(new ModelBet() { MyPlanId = 0, MyPeriodId = 0, MyBetAmountPerBet = "0.0", MyBetTotalAmount = "0.0" }); }
            else if ((cbbPlan.Text) == "方案1") { lst.AddRange(lstBet1); lst.Add(new ModelBet() { MyPlanId = 1, MyPeriodId = 10000, MyBetAmountPerBet = "0.0", MyBetTotalAmount = "0.0" }); }
            else if ((cbbPlan.Text) == "方案2") { lst.AddRange(lstBet2); lst.Add(new ModelBet() { MyPlanId = 2, MyPeriodId = 10000, MyBetAmountPerBet = "0.0", MyBetTotalAmount = "0.0" }); }
            else if ((cbbPlan.Text) == "方案3") { lst.AddRange(lstBet3); lst.Add(new ModelBet() { MyPlanId = 3, MyPeriodId = 10000, MyBetAmountPerBet = "0.0", MyBetTotalAmount = "0.0" }); }
            else if ((cbbPlan.Text) == "方案4") { lst.AddRange(lstBet4); lst.Add(new ModelBet() { MyPlanId = 4, MyPeriodId = 10000, MyBetAmountPerBet = "0.0", MyBetTotalAmount = "0.0" }); }
            else if ((cbbPlan.Text) == "方案5") { lst.AddRange(lstBet5); lst.Add(new ModelBet() { MyPlanId = 4, MyPeriodId = 10000, MyBetAmountPerBet = "0.0", MyBetTotalAmount = "0.0" }); }
            else if ((cbbPlan.Text) == "方案6") { lst.AddRange(lstBet6); lst.Add(new ModelBet() { MyPlanId = 4, MyPeriodId = 10000, MyBetAmountPerBet = "0.0", MyBetTotalAmount = "0.0" }); }
            cbbPeriod.DataSource = null;
            cbbPeriod.DataSource = lst;
            cbbPeriod.DisplayMember = "MyPeriodDescription";
            cbbPeriod.ValueMember = "MyPeriodId";
            if (lst.Count > 0) { cbbPeriod.SelectedIndex = 0; }
            cbbPeriod.SelectedIndexChanged += new System.EventHandler(this.CbbPeriod_SelectedIndexChanged);
            CbbPeriod_SelectedIndexChanged(cbbPeriod, null);
        }

        private void CbbPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strname = ((ComboBox)sender).Name.ToString().Replace("CbbPlan", "");
            switch (strname)
            {
                case "0":
                    CbbPlanSelectChanged(CbbPlan0, CbbPeriod0, lstPlanBet0);
                    break;
                case "1":
                    CbbPlanSelectChanged(CbbPlan1, CbbPeriod1, lstPlanBet1);
                    break;
                case "2":
                    CbbPlanSelectChanged(CbbPlan2, CbbPeriod2, lstPlanBet2);
                    break;
                case "3":
                    CbbPlanSelectChanged(CbbPlan3, CbbPeriod3, lstPlanBet3);
                    break;
                case "4":
                    CbbPlanSelectChanged(CbbPlan4, CbbPeriod4, lstPlanBet4);
                    break;
                case "5":
                    CbbPlanSelectChanged(CbbPlan5, CbbPeriod5, lstPlanBet5);
                    break;
                case "6":
                    CbbPlanSelectChanged(CbbPlan6, CbbPeriod6, lstPlanBet6);
                    break;
                case "7":
                    CbbPlanSelectChanged(CbbPlan7, CbbPeriod7, lstPlanBet7);
                    break;
                case "8":
                    CbbPlanSelectChanged(CbbPlan8, CbbPeriod8, lstPlanBet8);
                    break;
                case "9":
                    CbbPlanSelectChanged(CbbPlan9, CbbPeriod9, lstPlanBet9);
                    break;
            }
        }

        private void CbbPeriodSelectedChanged(ComboBox cbbPlan, ComboBox cbbPeriod, List<ModelBet> lstPlanBet, NumericUpDown txtBetAmount)
        {
            ModelBet modelbet = null;
            if ((cbbPlan.Text) == "未投注") { txtBetAmount.Text = "0.0"; }
            else { modelbet = lstPlanBet.Find(p => p.MyPeriodDescription == cbbPeriod.Text); }
            txtBetAmount.Text = modelbet == null ? "0.0" : modelbet.MyBetAmountPerBet;
        }

        private void CbbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strname = ((ComboBox)sender).Name.ToString().Replace("CbbPeriod", "");
            switch (strname)
            {
                case "0":
                    CbbPeriodSelectedChanged(CbbPlan0, CbbPeriod0, lstPlanBet0, TxtBetAmount0);
                    break;
                case "1":
                    CbbPeriodSelectedChanged(CbbPlan1, CbbPeriod1, lstPlanBet1, TxtBetAmount1);
                    break;
                case "2":
                    CbbPeriodSelectedChanged(CbbPlan2, CbbPeriod2, lstPlanBet2, TxtBetAmount2);
                    break;
                case "3":
                    CbbPeriodSelectedChanged(CbbPlan3, CbbPeriod3, lstPlanBet3, TxtBetAmount3);
                    break;
                case "4":
                    CbbPeriodSelectedChanged(CbbPlan4, CbbPeriod4, lstPlanBet4, TxtBetAmount4);
                    break;
                case "5":
                    CbbPeriodSelectedChanged(CbbPlan5, CbbPeriod5, lstPlanBet5, TxtBetAmount5);
                    break;
                case "6":
                    CbbPeriodSelectedChanged(CbbPlan6, CbbPeriod6, lstPlanBet6, TxtBetAmount6);
                    break;
                case "7":
                    CbbPeriodSelectedChanged(CbbPlan7, CbbPeriod7, lstPlanBet7, TxtBetAmount7);
                    break;
                case "8":
                    CbbPeriodSelectedChanged(CbbPlan8, CbbPeriod8, lstPlanBet8, TxtBetAmount8);
                    break;
                case "9":
                    CbbPeriodSelectedChanged(CbbPlan9, CbbPeriod9, lstPlanBet9, TxtBetAmount9);
                    break;
            }
        }

        private void BtnLogon_Click(object sender, EventArgs e)
        {
            WbbBetWeb.Document.GetElementById("ctl00_txtUser").Focus();
            WbbBetWeb.Document.GetElementById("ctl00_txtUser").InnerText = TxtUserID.Text;

            WbbBetWeb.Document.GetElementById("ctl00_txtPwd").Focus();
            WbbBetWeb.Document.GetElementById("ctl00_txtPwd").InnerText = TxtPassword.Text;

            WbbBetWeb.Document.GetElementById("ctl00_txtcode").Focus();
            WbbBetWeb.Document.GetElementById("ctl00_txtcode").InnerText = TxtValidCode.Text;

            HtmlElement btnSubmit = WbbBetWeb.Document.GetElementById("ctl00_btnlogin");
            btnSubmit.InvokeMember("Click");
            

        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            //GetValidImage("ctl00_imgcode");
           // WriteBetSetup();
            
        }

        private void BtnAddNumber_Click(object sender, EventArgs e)
        {
           // HtmlElement helCode = WbbBetWeb.Document.GetElementById("txtcode");
           // helCode.Focus();
           //HtmlElement helOption = helCode.Document.CreateElement("option");
           //helOption.SetAttribute("value", "8,,,,");

            HtmlElement helCode = WbbBetWeb.Document.GetElementById("txtcode");
            HtmlElement helOption = WbbBetWeb.Document.CreateElement("option");
            helOption.InnerText = "8,,,,";
            helCode.AppendChild(helOption); 

         //   helCode.Document.CreateElement
           // WbbBetWeb.Document.GetElementById("txtcode").SetAttribute("value", "8,,,,");  
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            if (WbbBetHistoryWeb.Url == null)
                return;

            if (!WbbBetHistoryWeb.Url.ToString().Contains(strGlobalURL))
                return;
            HtmlElement helTable = WbbBetHistoryWeb.Document.GetElementById("ctl00_ContentPlaceHolder1_GridView1");
            string strHtml = helTable.InnerHtml;
           // string pat = @"<td>(?<text>.*?[^&nbsp;<])</td>";
            Regex reg = new Regex(@"(?is)<td[^>]*>(.*?)</td>");
            MatchCollection mc = reg.Matches(strHtml);
            
            for (int i = 0; i < mc.Count; i+=3)
            { 
                ModelHistory modelhistory =  new ModelHistory();
                modelhistory.Period = mc[i].ToString().Replace("<TD>", "").Replace(@"</TD>", "");
                modelhistory.DrawingDateTime = mc[i + 1].ToString().Replace("<TD>", "").Replace(@"</TD>", "");
                modelhistory.DrawingNumber = mc[i + 2].ToString().Replace("<TD>", "").Replace(@"</TD>", "");
                if (lstModelHistory.Find(p => p.Period == modelhistory.Period) == null)
                {
                    lstModelHistory.Add(modelhistory);
                }
                
            }
            RefreshHistory();
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

        public void RefreshHistory()
        {            
            GrvHistory.DataSource = new BindingList<ModelHistory>(lstModelHistory.OrderByDescending(p => p.Period).ToList());                      
            LblHistoryNo.Text = lstModelHistory.Count.ToString();
        }

        public void RefreshMyBet()
        {
            List<ModelBet> lstBet;
            if (RdbPlan1.Checked)         { lstBet = lstBet1; }
            else if (RdbPlan2.Checked)    { lstBet = lstBet2; }
            else if (RdbPlan3.Checked)    { lstBet = lstBet3; }
            else if (RdbPlan4.Checked)    { lstBet = lstBet4; }
            else if (RdbPlan5.Checked)    { lstBet = lstBet5; }
            else                          { lstBet = lstBet6; }

            //GrvMyBet.DataSource = new List<ModelBet>();
            GrvMyBet.DataSource = new BindingList<ModelBet>(lstBet) ;

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

            lstBet.RemoveAt(lstBet.Count-1);
            RefreshMyBet();
        }

        private void GetValidImage(string strId)
        {
            HtmlElement helTable = WbbBetHistoryWeb.Document.GetElementById(strId);
        }

        private void FrmBet_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimerBet.Enabled = false;
            LogInfo.WriteBetSetup(lstBet1, lstBet2, lstBet3, lstBet4, lstBet5, lstBet6);
            LogInfo.WriteBetHistory(lstModelHistory);
            LogInfo.WriteLogState(lstBetState);
            LogInfo.WriteCurrentState(CbbPlan0, CbbPeriod0,
                                    CbbPlan1, CbbPeriod1,
                                    CbbPlan2, CbbPeriod2,
                                    CbbPlan3, CbbPeriod3, 
                                    CbbPlan4, CbbPeriod4,
                                    CbbPlan5, CbbPeriod5,
                                    CbbPlan6, CbbPeriod6,
                                    CbbPlan7, CbbPeriod7,
                                    CbbPlan8, CbbPeriod8,
                                    CbbPlan9, CbbPeriod9);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //WbbBetWeb.Navigate(strGlobalURL + @"/lhc/index.aspx");   //六合彩
            TimerBet_Tick(null, null);
            TimerBet.Enabled = true;
        }

        private bool GetLastPeriod()
        {
            //timeout;
            DateTime dtStart = DateTime.Now;
            //System.TimeSpan

            //获取上期
            Ajaxloading =true;
            WbbAjax.Navigate(strBackupURL + @"/ssc/ajax.aspx?act=getlastkj" + "&testtick=" + DateTime.Now.Ticks.ToString());
            while (Ajaxloading) 
            {
                if ((DateTime.Now - dtStart).Seconds > 50)
                {
                    //TimerBet.Enabled = false;
                    //MessageBox.Show("连接超时。");                    
                    LogInfo.AddLog(LsbLog, "getlastkj, 连接超时。");
                    Ajaxloading = false;
                    return false;
                }
                Application.DoEvents(); 
            }
            return true;
        }

        private bool GetCurrentPeriod()
        {
            DateTime dtStart = DateTime.Now;

            Ajaxloading = true;
            WbbAjax.Navigate(strBackupURL + @"/ssc/ajax.aspx?act=getqihao" + "&testtick=" + DateTime.Now.Ticks.ToString());
            
            while (Ajaxloading)             
            {
                if ((DateTime.Now - dtStart).Seconds > 50)
                {                    
                    //MessageBox.Show("连接超时。");
                    //break;
                    LogInfo.AddLog(LsbLog, " getqihao, 连接超时。");
                    Ajaxloading = false;
                    return false;
                }
                Application.DoEvents(); 
            }            
            return true;

        }

        private void WbbAjax_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //MessageBox.Show(WbbAjax.Document.Body.OuterHtml);
            //MessageBox.Show(WbbAjax.Url.ToString());
            string wbUrl = WbbAjax.Url.ToString();
            string strReturn = "";
            if (wbUrl.Contains(@"347878.com"))
            {
                WbbAjax.Stop();
                Ajaxloading = false;
                return;
            }

            if (!wbUrl.Contains(strBackupURL))
            {
                WbbAjax.Stop();
                Ajaxloading = false;
                return;
            }

            //上次
            if (wbUrl.Contains(@"?act=getlastkj"))
            {                
                strReturn = WbbAjax.Document.Body.InnerText.Replace("[", "").Replace("]", "");
                LogInfo.AddLog(LsbLog, "getlastkj 成功获取上次期数。: " + strReturn);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                ModelLastPeriod lastperiod = serializer.Deserialize<ModelLastPeriod>(strReturn); //(ModelLastPeriod)JsonConvert.DeserializeObject(strReturn, typeof(ModelLastPeriod));
                if (LblLastPeriodId.Text != lastperiod.qihao)
                { 
                    List<ModelBetState> lsttmp = lstBetState.Where(p => p.Period == lastperiod.qihao).ToList();
                    if (lsttmp.Count() > 0)
                    {
                        lsttmp.ForEach(p => p.DrawingNumber = lastperiod.code);
                        GetTotal(lastperiod.qihao, lastperiod.code);
                        RefreshGridState();
                    }
                    else
                    {                    
                        ModelBetState modelBetState = new ModelBetState() { Period = lastperiod.qihao, DrawingNumber = lastperiod.code };
                        lstBetState.Add(modelBetState);
                        GetTotal(lastperiod.qihao, lastperiod.code);
                        RefreshGridState();
                    }
                    LblLastPeriodId.Text = lastperiod.qihao;
                    LblLastNo.Text = lastperiod.code;

                    LblTenthousand.Text = (lastperiod.code == null || lastperiod.code.Length < 1) ? "" : lastperiod.code.Substring(0, 1);
                    SetControlColor(LblTenthousand.Text);

                    if (bAutoBet)
                    {
                        if (!bFirstAutoBet)
                        {
                            AllBtnAddPeriodClick(true);
                        }
                        AllBetClick();
                        bFirstAutoBet = false;
                    }
                    
                }

            }
            else if (wbUrl.Contains(@"?act=getqihao"))
            {                
                strReturn = WbbAjax.Document.Body.InnerText.Replace("[", "").Replace("]", "");
                LogInfo.AddLog(LsbLog, "getqihao 成功获取当前期数。: " + strReturn);
                //string strSerializeJSON = "{\"qihao\":\"161022023\",\"code\":\"1,1,8,0,2\"}";
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                ModelCurrentPeriod currentPeriod = serializer.Deserialize<ModelCurrentPeriod>(strReturn); //(ModelCurrentPeriod)JsonConvert.DeserializeObject(strReturn, typeof(ModelCurrentPeriod));
                LblCurrentPeriodId.Text = currentPeriod.qihao;
                int istoptime = int.Parse(currentPeriod.stoptime);
                LblStopTime.Text = StampToTime(istoptime);
            }
            Ajaxloading = false;
            //当前期            
        }

        private void GetTotal(string strPeriod, string strCode)
        {
            decimal dallamount;

            List<ModelBetState> lsttmp = lstBetState.Where(p => p.Period == strPeriod).ToList();
            if (lsttmp.Count() > 0)
            {                
                for (int i = 0; i < lsttmp.Count; i++ )
                {
                    
                    decimal dAmount0 = string.IsNullOrEmpty(lsttmp[i].Bet0Amount)? 0 : decimal.Parse(lsttmp[i].Bet0Amount);
                    decimal dAmount1 = string.IsNullOrEmpty(lsttmp[i].Bet1Amount)? 0 : decimal.Parse(lsttmp[i].Bet1Amount);
                    decimal dAmount2 = string.IsNullOrEmpty(lsttmp[i].Bet2Amount) ? 0 : decimal.Parse(lsttmp[i].Bet2Amount);
                    decimal dAmount3 = string.IsNullOrEmpty(lsttmp[i].Bet3Amount) ? 0 : decimal.Parse(lsttmp[i].Bet3Amount);
                    decimal dAmount4 = string.IsNullOrEmpty(lsttmp[i].Bet4Amount) ? 0 : decimal.Parse(lsttmp[i].Bet4Amount);
                    decimal dAmount5 = string.IsNullOrEmpty(lsttmp[i].Bet5Amount) ? 0 : decimal.Parse(lsttmp[i].Bet5Amount);
                    decimal dAmount6 = string.IsNullOrEmpty(lsttmp[i].Bet6Amount) ? 0 : decimal.Parse(lsttmp[i].Bet6Amount);
                    decimal dAmount7 = string.IsNullOrEmpty(lsttmp[i].Bet7Amount) ? 0 : decimal.Parse(lsttmp[i].Bet7Amount);
                    decimal dAmount8 = string.IsNullOrEmpty(lsttmp[i].Bet8Amount) ? 0 : decimal.Parse(lsttmp[i].Bet8Amount);
                    decimal dAmount9 = string.IsNullOrEmpty(lsttmp[i].Bet9Amount) ? 0 : decimal.Parse(lsttmp[i].Bet9Amount);

                    dallamount = dAmount0 +
                                 dAmount1 +
                                 dAmount2 +
                                 dAmount3 +
                                 dAmount4 +
                                 dAmount5 +
                                 dAmount6 +
                                 dAmount7 +
                                 dAmount8 +
                                 dAmount9;
                    lsttmp[i].TotalAmount =-1* dallamount;
                    decimal dwin = 0;
                    string strTemp = (strCode == null || strCode.Length < 1) ? "" : strCode.Substring(0, 1);
                    if (strTemp != "")
                    {
                        switch (strTemp)
                        {
                            case "0": dwin = dAmount0 * ((decimal)9.7); break;
                            case "1": dwin = dAmount1 * ((decimal)9.7); break;
                            case "2": dwin = dAmount2 * ((decimal)9.7); break;
                            case "3": dwin = dAmount3 * ((decimal)9.7); break;
                            case "4": dwin = dAmount4 * ((decimal)9.7); break;
                            case "5": dwin = dAmount5 * ((decimal)9.7); break;
                            case "6": dwin = dAmount6 * ((decimal)9.7); break;
                            case "7": dwin = dAmount7 * ((decimal)9.7); break;
                            case "8": dwin = dAmount8 * ((decimal)9.7); break;
                            case "9": dwin = dAmount9 * ((decimal)9.7); break;
                        }
                    }
                    lsttmp[i].TotalAmount += dwin;
                }
            }
            LblTotalAmount.Text = lstBetState.Sum(p => p.TotalAmount).ToString();
        }

        private void SetControlColor(string str)
        {
            BtnReset0.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnReset1.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnReset2.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnReset3.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnReset4.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnReset5.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnReset6.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnReset7.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnReset8.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnReset9.ForeColor = System.Drawing.SystemColors.ControlText;

            if (str =="") {return;}
            switch (str)
            {
                case "0": BtnReset0.ForeColor = System.Drawing.Color.Red; break;
                case "1": BtnReset1.ForeColor = System.Drawing.Color.Red; break;
                case "2": BtnReset2.ForeColor = System.Drawing.Color.Red; break;
                case "3": BtnReset3.ForeColor = System.Drawing.Color.Red; break;
                case "4": BtnReset4.ForeColor = System.Drawing.Color.Red; break;
                case "5": BtnReset5.ForeColor = System.Drawing.Color.Red; break;
                case "6": BtnReset6.ForeColor = System.Drawing.Color.Red; break;
                case "7": BtnReset7.ForeColor = System.Drawing.Color.Red; break;
                case "8": BtnReset8.ForeColor = System.Drawing.Color.Red; break;
                case "9": BtnReset9.ForeColor = System.Drawing.Color.Red; break;
            }

            Grb0.ForeColor = BtnReset0.ForeColor;
            Grb1.ForeColor = BtnReset1.ForeColor;
            Grb2.ForeColor = BtnReset2.ForeColor;
            Grb3.ForeColor = BtnReset3.ForeColor;
            Grb4.ForeColor = BtnReset4.ForeColor;
            Grb5.ForeColor = BtnReset5.ForeColor;
            Grb6.ForeColor = BtnReset6.ForeColor;
            Grb7.ForeColor = BtnReset7.ForeColor;
            Grb8.ForeColor = BtnReset8.ForeColor;
            Grb9.ForeColor = BtnReset9.ForeColor;
        }

        private string StampToTime(int stamp)
        {
            int hours = stamp / 3600;
            int minutes = stamp % 3600 / 60;
            int seconds = stamp % 60;

            return hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        private void RdbPlan_CheckedChanged(object sender, EventArgs e)
        {
            RefreshMyBet();
        }

        private void WbbBetWeb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           ///          
            string wbUrl = WbbBetWeb.Url.ToString();
            string strReturn = "";
            if (wbUrl.Contains(@"347878.com"))
            {
                WbbBetWeb.Stop();
                return;
            }

            if (!wbUrl.Contains(strGlobalURL))
            {
                WbbBetWeb.Stop();
                return;
            }


            HtmlElement hel = WbbBetWeb.Document.GetElementById("ctl00_tdlogin");            
            if (hel!=null)
            {
                LblLoginDescription.Text = "已登录";
                bLogin = true;
                TimerBet_Tick(null, null);
                TimerBet.Enabled = true;
                GrInfo.Enabled = true;
            }

            hel = WbbBetWeb.Document.GetElementById("ctl00_tdnologin");            
            if (hel!=null)
            {
                LblLoginDescription.Text = "未登录";                
                bLogin = false;

            }           
        }

        private void WbbBetHistoryWeb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            string wbUrl = WbbBetHistoryWeb.Url.ToString();
            string strReturn = "";
            if (wbUrl.Contains(@"347878.com"))
            {
                WbbBetHistoryWeb.Stop();
                return;
            }

            if (!wbUrl.Contains(strGlobalURL))
            {
                WbbBetHistoryWeb.Stop();
                return;
            }

            //上次
            if (wbUrl.Contains(@"?act=balance"))
            {                
                strReturn = WbbBetHistoryWeb.Document.Body.InnerText.Replace("[", "").Replace("]", "");
                LogInfo.AddLog(LsbLog, "balance 成功获取余额。: " + strReturn);
                LblBalance.Text = strReturn;
            }
        }

        private void TimerBet_Tick(object sender, EventArgs e)
        {
            
            if (RefreshCount >= 6)
            {
                GetBalance();
                RefreshCount = 1;
            }
            RefreshCount++;

            if (Ajaxloading == true)
            {
                return;
            }
            TimerBet.Enabled = false;
            if (!GetCurrentPeriod())
            {
                TimerBet.Enabled = true;
                return;
            }
            if (!GetLastPeriod())
            {
                TimerBet.Enabled = true;
                return;
            }
            TimerBet.Enabled = true;
        }

        private void BtnResetClick(ComboBox cbbPlan, ComboBox cbbPeriod)
        {
            cbbPlan.SelectedIndex = -1;
            cbbPeriod.SelectedIndex = -1;

            if (cbbPlan.Items.Count > 1)
            {
                cbbPlan.SelectedIndex = 1;
            }
            //if (cbbPeriod.Items.Count >= 1)
            //{
            //    cbbPeriod.SelectedIndex = 1;
            //}
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            string strname = ((Button)sender).Name.ToString().Replace("BtnReset", "");
            switch (strname)
            {
                case "0":
                    BtnResetClick(CbbPlan0, CbbPeriod0);
                    break;
                case "1":
                    BtnResetClick(CbbPlan1, CbbPeriod1); 
                    break;
                case "2":
                    BtnResetClick(CbbPlan2, CbbPeriod2); 
                    break;
                case "3":
                    BtnResetClick(CbbPlan3, CbbPeriod3); 
                    break;
                case "4":
                    BtnResetClick(CbbPlan4, CbbPeriod4); 
                    break;
                case "5":
                    BtnResetClick(CbbPlan5, CbbPeriod5); 
                    break;
                case "6":
                    BtnResetClick(CbbPlan6, CbbPeriod6); 
                    break;
                case "7":
                    BtnResetClick(CbbPlan7, CbbPeriod7); 
                    break;
                case "8":
                    BtnResetClick(CbbPlan8, CbbPeriod8); 
                    break;
                case "9":
                    BtnResetClick(CbbPlan9, CbbPeriod9); 
                    break;
            }
        }

        private void BtnAddPeriodClick(ComboBox cbbPlan, ComboBox cbbPeriod, bool bContinue)
        {
            //cbbPlan.SelectedIndex = -1;
            //cbbPeriod.SelectedIndex = -1;
            if (cbbPlan.Items.Count == 0 && cbbPeriod.Items.Count == 0)
            {
                return;
            }

            //没有bContinue正常增加
            //bContinue    cbbPeriod.Parent.ForeColor == System.Drawing.Color.Red  暂停投注 --》 plan ++
            //            cbbPeriod.Parent.ForeColor == System.Drawing.Color.Red  不是暂停投注  --》 reset
            if (!bContinue)
            {
                if (cbbPeriod.Items.Count == cbbPeriod.SelectedIndex + 1)
                {
                    if (cbbPlan.Items.Count == cbbPlan.SelectedIndex + 1)//最大方案
                    {
                        return;
                    }
                    else if (cbbPlan.Items.Count > cbbPlan.SelectedIndex + 1)//
                    {
                        cbbPlan.SelectedIndex++;                     
                    }
                }
                else
                {
                    cbbPeriod.SelectedIndex++;
                }
                return;
            }
            

            if (cbbPeriod.Items.Count == cbbPeriod.SelectedIndex + 1)//最大期数
            {
                //不中 ，
                if (cbbPeriod.Parent.ForeColor != System.Drawing.Color.Red)  
                {
                    return;
                }

                //中
                if (cbbPlan.Items.Count == cbbPlan.SelectedIndex + 1)//最大方案         
                {
                    
                    BtnResetClick(cbbPlan,cbbPeriod);
                    return;
                }
                else if (cbbPlan.Items.Count > cbbPlan.SelectedIndex + 1)//
                {       
                    cbbPlan.SelectedIndex++;
                }
                else
                {
                    throw new Exception("出错 Plan");
                }
            }
            else if(cbbPeriod.Items.Count > cbbPeriod.SelectedIndex + 1)
            {
                //不中 ，
                if (cbbPeriod.Parent.ForeColor != System.Drawing.Color.Red)  
                {
                    cbbPeriod.SelectedIndex++;
                    return;
                }
                //中
                BtnResetClick(cbbPlan, cbbPeriod);
                return;
            }
            else
            {
                throw new Exception("出错 Period");
            }
        }

        private void BtnAddPeriod_Click(object sender, EventArgs e)
        {
            string strname = ((Button)sender).Name.ToString().Replace("BtnAddPeriod", "");
            switch (strname)
            {
                case "0": BtnAddPeriodClick(CbbPlan0, CbbPeriod0, false); break;
                case "1": BtnAddPeriodClick(CbbPlan1, CbbPeriod1, false); break;
                case "2": BtnAddPeriodClick(CbbPlan2, CbbPeriod2, false); break;
                case "3": BtnAddPeriodClick(CbbPlan3, CbbPeriod3, false); break;
                case "4": BtnAddPeriodClick(CbbPlan4, CbbPeriod4, false); break;
                case "5": BtnAddPeriodClick(CbbPlan5, CbbPeriod5, false); break;
                case "6": BtnAddPeriodClick(CbbPlan6, CbbPeriod6, false); break;
                case "7": BtnAddPeriodClick(CbbPlan7, CbbPeriod7, false); break;
                case "8": BtnAddPeriodClick(CbbPlan8, CbbPeriod8, false); break;
                case "9": BtnAddPeriodClick(CbbPlan9, CbbPeriod9, false); break;
            }                                                   
        }

        private void WbbBet_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string wbUrl = WbbBet.Url.ToString();
            string strReturn = "";
            //上次

            strReturn = WbbBet.Document.Body.InnerText.Trim('[').Trim(']');
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ModelReturnBet modelReturnBet = serializer.Deserialize<ModelReturnBet>(strReturn);//(ModelReturnBet)JsonConvert.DeserializeObject(strReturn, typeof(ModelReturnBet));
            
            //
            if (modelReturnBet.err == "0")
            {
                LogInfo.AddLog(LsbLog, " 投注号码：" + modelReturnBet.codes + " 金额：" + modelReturnBet.singleprice + "  期号：" + modelReturnBet.qihao + "  " + modelReturnBet.msg + "---------");

                ModelBetState modelBetState = null;
                List<ModelBetState> lsttempmodel;

                switch (modelReturnBet.codes)
                {
                    case "0,,,,":
                        lsttempmodel = lstBetState.Where(p => p.Period == modelReturnBet.qihao && p.Bet0PlanID == 0).ToList();
                        if (lsttempmodel.Count > 0)
                        {
                            lsttempmodel[0].Bet0PlanID = CurrentPlan;
                            lsttempmodel[0].Bet0PeriodID = CurrentPeriod;
                            lsttempmodel[0].Bet0Amount = modelReturnBet.singleprice;
                        }
                        else
                        {
                            modelBetState = new ModelBetState() { Period = modelReturnBet.qihao, Bet0PlanID = CurrentPlan, Bet0PeriodID = CurrentPeriod, Bet0Amount = modelReturnBet.singleprice };
                            lstBetState.Add(modelBetState);
                        }
                        break;
                    case "1,,,,":
                        lsttempmodel = lstBetState.Where(p => p.Period == modelReturnBet.qihao && p.Bet1PlanID == 0).ToList();
                        if (lsttempmodel.Count > 0)
                        {
                            lsttempmodel[0].Bet1PlanID = CurrentPlan;
                            lsttempmodel[0].Bet1PeriodID = CurrentPeriod;
                            lsttempmodel[0].Bet1Amount = modelReturnBet.singleprice;
                        }
                        else
                        {
                            modelBetState = new ModelBetState() { Period = modelReturnBet.qihao, Bet1PlanID = CurrentPlan, Bet1PeriodID = CurrentPeriod, Bet1Amount = modelReturnBet.singleprice };
                            lstBetState.Add(modelBetState);
                        }
                        break;
                    case "2,,,,":
                        lsttempmodel = lstBetState.Where(p => p.Period == modelReturnBet.qihao && p.Bet2PlanID == 0).ToList();
                        if (lsttempmodel.Count > 0)
                        {
                            lsttempmodel[0].Bet2PlanID = CurrentPlan;
                            lsttempmodel[0].Bet2PeriodID = CurrentPeriod;
                            lsttempmodel[0].Bet2Amount = modelReturnBet.singleprice;
                        }
                        else
                        {
                            modelBetState = new ModelBetState() { Period = modelReturnBet.qihao, Bet2PlanID = CurrentPlan, Bet2PeriodID = CurrentPeriod, Bet2Amount = modelReturnBet.singleprice };
                            lstBetState.Add(modelBetState);
                        }
                        break;
                    case "3,,,,":
                        lsttempmodel = lstBetState.Where(p => p.Period == modelReturnBet.qihao && p.Bet3PlanID == 0).ToList();
                        if (lsttempmodel.Count > 0)
                        {
                            lsttempmodel[0].Bet3PlanID = CurrentPlan;
                            lsttempmodel[0].Bet3PeriodID = CurrentPeriod;
                            lsttempmodel[0].Bet3Amount = modelReturnBet.singleprice;
                        }
                        else
                        {
                            modelBetState = new ModelBetState() { Period = modelReturnBet.qihao, Bet3PlanID = CurrentPlan, Bet3PeriodID = CurrentPeriod, Bet3Amount = modelReturnBet.singleprice };
                            lstBetState.Add(modelBetState);
                        }
                        break;
                    case "4,,,,":
                        lsttempmodel = lstBetState.Where(p => p.Period == modelReturnBet.qihao && p.Bet4PlanID == 0).ToList();
                        if (lsttempmodel.Count > 0)
                        {
                            lsttempmodel[0].Bet4PlanID = CurrentPlan;
                            lsttempmodel[0].Bet4PeriodID = CurrentPeriod;
                            lsttempmodel[0].Bet4Amount = modelReturnBet.singleprice;
                        }
                        else
                        {
                            modelBetState = new ModelBetState() { Period = modelReturnBet.qihao, Bet4PlanID = CurrentPlan, Bet4PeriodID = CurrentPeriod, Bet4Amount = modelReturnBet.singleprice };
                            lstBetState.Add(modelBetState);
                        }
                        break;
                    case "5,,,,":
                        lsttempmodel = lstBetState.Where(p => p.Period == modelReturnBet.qihao && p.Bet5PlanID == 0).ToList();
                        if (lsttempmodel.Count > 0)
                        {
                            lsttempmodel[0].Bet5PlanID = CurrentPlan;
                            lsttempmodel[0].Bet5PeriodID = CurrentPeriod;
                            lsttempmodel[0].Bet5Amount = modelReturnBet.singleprice;
                        }
                        else
                        {
                            modelBetState = new ModelBetState() { Period = modelReturnBet.qihao, Bet5PlanID = CurrentPlan, Bet5PeriodID = CurrentPeriod, Bet5Amount = modelReturnBet.singleprice };
                            lstBetState.Add(modelBetState);
                        }
                        break;
                    case "6,,,,":
                        lsttempmodel = lstBetState.Where(p => p.Period == modelReturnBet.qihao && p.Bet6PlanID == 0).ToList();
                        if (lsttempmodel.Count > 0)
                        {
                            lsttempmodel[0].Bet6PlanID = CurrentPlan;
                            lsttempmodel[0].Bet6PeriodID = CurrentPeriod;
                            lsttempmodel[0].Bet6Amount = modelReturnBet.singleprice;
                        }
                        else
                        {
                            modelBetState = new ModelBetState() { Period = modelReturnBet.qihao, Bet6PlanID = CurrentPlan, Bet6PeriodID = CurrentPeriod, Bet6Amount = modelReturnBet.singleprice };
                            lstBetState.Add(modelBetState);
                        }
                        break;
                    case "7,,,,":
                        lsttempmodel = lstBetState.Where(p => p.Period == modelReturnBet.qihao && p.Bet7PlanID == 0).ToList();
                        if (lsttempmodel.Count > 0)
                        {
                            lsttempmodel[0].Bet7PlanID = CurrentPlan;
                            lsttempmodel[0].Bet7PeriodID = CurrentPeriod;
                            lsttempmodel[0].Bet7Amount = modelReturnBet.singleprice;
                        }
                        else
                        {
                            modelBetState = new ModelBetState() { Period = modelReturnBet.qihao, Bet7PlanID = CurrentPlan, Bet7PeriodID = CurrentPeriod, Bet7Amount = modelReturnBet.singleprice };
                            lstBetState.Add(modelBetState);
                        }
                        break;
                    case "8,,,,":
                        lsttempmodel = lstBetState.Where(p => p.Period == modelReturnBet.qihao && p.Bet8PlanID == 0).ToList();
                        if (lsttempmodel.Count > 0)
                        {
                            lsttempmodel[0].Bet8PlanID = CurrentPlan;
                            lsttempmodel[0].Bet8PeriodID = CurrentPeriod;
                            lsttempmodel[0].Bet8Amount = modelReturnBet.singleprice;
                        }
                        else
                        {
                            modelBetState = new ModelBetState() { Period = modelReturnBet.qihao, Bet8PlanID = CurrentPlan, Bet8PeriodID = CurrentPeriod, Bet8Amount = modelReturnBet.singleprice };
                            lstBetState.Add(modelBetState);
                        }
                        break;
                    case "9,,,,":
                        lsttempmodel = lstBetState.Where(p => p.Period == modelReturnBet.qihao && p.Bet9PlanID == 0).ToList();
                        if (lsttempmodel.Count > 0)
                        {
                            lsttempmodel[0].Bet9PlanID = CurrentPlan;
                            lsttempmodel[0].Bet9PeriodID = CurrentPeriod;
                            lsttempmodel[0].Bet9Amount = modelReturnBet.singleprice;
                        }
                        else
                        {
                            modelBetState = new ModelBetState() { Period = modelReturnBet.qihao, Bet9PlanID = CurrentPlan, Bet9PeriodID = CurrentPeriod, Bet9Amount = modelReturnBet.singleprice };
                            lstBetState.Add(modelBetState);
                        }
                        break;
                }
                GetTotal(modelReturnBet.qihao, "");
                RefreshGridState();
            }
            else if (modelReturnBet.err == "-1")
            {
                if (modelReturnBet.msg.Contains("您尚未登陆"))
                {
                    //[{"msg":"您尚未登陆，请重新登陆！","err":"-1"}]
                    bLogin = false;
                    LblLoginDescription.Text = "未登录";
                }
                LogInfo.AddLog(LsbLog, modelReturnBet.msg);
            }

            //int CurrentPeriod;
            //int CurrentPlan;
            
            Betloading = false;
        }

        private bool Beting(string strtemp_code, string strplayway, string dBetAmount, string strqihao, int iPlan, int iPeriod)
        {          
             decimal temp_single = 0;
             temp_single = decimal.Parse(dBetAmount);
            string strUrl = strGlobalURL + @"/ssc/ajax.aspx";
            if (iPlan == 0)
            {
                return true;
            }
            //if (strqihao == "-" || strqihao == "")
            //{
            //    return true;
            //}
            if (temp_single == 0)
            {
                return true;
            }

            DateTime dtStart = DateTime.Now;

            LogInfo.AddLog(LsbLog, "投注号码：" + strtemp_code + "  玩法：" + strplayway + "  金额：" + dBetAmount + "  期号：" + strqihao);            
            CurrentPlan = iPlan;
            CurrentPeriod = iPeriod + 1;
            Betloading = true;
            string strPost = "act=postsn&code=" + strtemp_code + "&playway=" + strplayway + "&single=" + temp_single.ToString("###0.#") + "&qihao=" + strqihao;
            Byte[] postBuffer = System.Text.Encoding.UTF8.GetBytes(strPost);
            string strHeaders = @"Content-Type: application/x-www-form-urlencoded";
            WbbBet.Navigate(strUrl, "", postBuffer, strHeaders);

            while (Betloading)
            {
                if ((DateTime.Now - dtStart).Seconds > 60)
                {
                    TimerBet.Enabled = false;
                    MessageBox.Show("连接超时。");
                    Betloading = false;
                    return false;
                }
                Application.DoEvents();
            }
            return true;
        }

        private void BtnBet_Click(object sender, EventArgs e)
        {
            if (!bLogin)
            {
                LogInfo.AddLog(LsbLog, "您尚未登陆 请重新登陆 才能投注！");
                return;
            }

            string strname = ((Button)sender).Name.ToString().Replace("BtnBet", "");
            switch (strname)
            {
                case "0":
                    if (ChbBet0.Checked == false)
                        return;
                    Beting("0,,,,", "10", TxtBetAmount0.Text, LblCurrentPeriodId.Text, CbbPlan0.SelectedIndex, CbbPeriod0.SelectedIndex); 
                    break;
                case "1":
                    if (ChbBet1.Checked == false)
                        return;
                    Beting("1,,,,", "10", TxtBetAmount1.Text, LblCurrentPeriodId.Text, CbbPlan1.SelectedIndex, CbbPeriod1.SelectedIndex); 
                    break;
                case "2":
                    if (ChbBet2.Checked == false)
                        return;
                    Beting("2,,,,", "10", TxtBetAmount2.Text, LblCurrentPeriodId.Text, CbbPlan2.SelectedIndex, CbbPeriod2.SelectedIndex); 
                    break;
                case "3":
                    if (ChbBet3.Checked == false)
                        return;
                    Beting("3,,,,", "10", TxtBetAmount3.Text, LblCurrentPeriodId.Text, CbbPlan3.SelectedIndex, CbbPeriod3.SelectedIndex); 
                    break;
                case "4":
                    if (ChbBet4.Checked == false)
                        return;
                    Beting("4,,,,", "10", TxtBetAmount4.Text, LblCurrentPeriodId.Text, CbbPlan4.SelectedIndex, CbbPeriod4.SelectedIndex); 
                    break;
                case "5":
                    if (ChbBet5.Checked == false)
                        return;
                    Beting("5,,,,", "10", TxtBetAmount5.Text, LblCurrentPeriodId.Text, CbbPlan5.SelectedIndex, CbbPeriod5.SelectedIndex); 
                    break;
                case "6":
                    if (ChbBet6.Checked == false)
                        return;
                    Beting("6,,,,", "10", TxtBetAmount6.Text, LblCurrentPeriodId.Text, CbbPlan6.SelectedIndex, CbbPeriod6.SelectedIndex); 
                    break;
                case "7":
                    if (ChbBet7.Checked == false)
                        return;
                    Beting("7,,,,", "10", TxtBetAmount7.Text, LblCurrentPeriodId.Text, CbbPlan7.SelectedIndex, CbbPeriod7.SelectedIndex); 
                    break;
                case "8":
                    if (ChbBet8.Checked == false)
                        return;
                    Beting("8,,,,", "10", TxtBetAmount8.Text, LblCurrentPeriodId.Text, CbbPlan8.SelectedIndex, CbbPeriod8.SelectedIndex); 
                    break;
                case "9":
                    if (ChbBet9.Checked == false)
                        return;
                    Beting("9,,,,", "10", TxtBetAmount9.Text, LblCurrentPeriodId.Text, CbbPlan9.SelectedIndex, CbbPeriod9.SelectedIndex); 
                    break;
            }
            GetBalance();
        }

        private void BtnResetAll_Click(object sender, EventArgs e)
        {            
            AllBtnResetClick();
        }

        private void BtnAddPeriodAll_Click(object sender, EventArgs e)
        {
            AllBtnAddPeriodClick(false);
        }

        private void BtnBetAll_Click(object sender, EventArgs e)
        {
            AllBetClick();
        }

        private void EnableControl(bool bEnable)
        {
            Grb0.Enabled = bEnable;
            Grb1.Enabled = bEnable;
            Grb2.Enabled = bEnable;
            Grb3.Enabled = bEnable;
            Grb4.Enabled = bEnable;
            Grb5.Enabled = bEnable;
            Grb6.Enabled = bEnable;
            Grb7.Enabled = bEnable;
            Grb8.Enabled = bEnable;
            Grb9.Enabled = bEnable;
            BtnResetAll.Enabled = bEnable;
            BtnAddPeriodAll.Enabled = bEnable;
            BtnBetAll.Enabled = bEnable;
        }

        private void GetBalance()
        {
            WbbBetHistoryWeb.Url = new Uri(strGlobalURL + @"/user/ajax.aspx?act=balance" + "&testtick=" + DateTime.Now.Ticks.ToString());
            LogInfo.AddLog(LsbLog, "获取余额------------！");
            while (WbbBetHistoryWeb.ReadyState < WebBrowserReadyState.Complete) Application.DoEvents();
        }

        private void RefreshGridState()
        {
            GrvBetState.DataSource = new BindingList<ModelBetState>(lstBetState.OrderByDescending(p => p.Period).ToList());
        }

        private void AllBtnResetClick()
        {
            BtnResetClick(CbbPlan0, CbbPeriod0);
            BtnResetClick(CbbPlan1, CbbPeriod1);
            BtnResetClick(CbbPlan2, CbbPeriod2);
            BtnResetClick(CbbPlan3, CbbPeriod3);
            BtnResetClick(CbbPlan4, CbbPeriod4);
            BtnResetClick(CbbPlan5, CbbPeriod5);
            BtnResetClick(CbbPlan6, CbbPeriod6);
            BtnResetClick(CbbPlan7, CbbPeriod7);
            BtnResetClick(CbbPlan8, CbbPeriod8);
            BtnResetClick(CbbPlan9, CbbPeriod9);  
        }

        private void AllBtnAddPeriodClick(bool bContinue)
        {
            BtnAddPeriodClick(CbbPlan0, CbbPeriod0, bContinue);
            BtnAddPeriodClick(CbbPlan1, CbbPeriod1, bContinue);
            BtnAddPeriodClick(CbbPlan2, CbbPeriod2, bContinue);
            BtnAddPeriodClick(CbbPlan3, CbbPeriod3, bContinue);
            BtnAddPeriodClick(CbbPlan4, CbbPeriod4, bContinue);
            BtnAddPeriodClick(CbbPlan5, CbbPeriod5, bContinue);
            BtnAddPeriodClick(CbbPlan6, CbbPeriod6, bContinue);
            BtnAddPeriodClick(CbbPlan7, CbbPeriod7, bContinue);
            BtnAddPeriodClick(CbbPlan8, CbbPeriod8, bContinue);
            BtnAddPeriodClick(CbbPlan9, CbbPeriod9, bContinue);
        }

        private void AllBetClick()
        {
            if (!bLogin)
            {
                LogInfo.AddLog(LsbLog, "您尚未登陆 请重新登陆 才能投注！");
                return;
            }
            EnableControl(false);                            
            if (ChbBet0.Checked && !Beting("0,,,,", "10", TxtBetAmount0.Text, LblCurrentPeriodId.Text, CbbPlan0.SelectedIndex, CbbPeriod0.SelectedIndex)) { EnableControl(true); return; }
            Thread.Sleep(500);
            if (ChbBet1.Checked && !Beting("1,,,,", "10", TxtBetAmount1.Text, LblCurrentPeriodId.Text, CbbPlan1.SelectedIndex, CbbPeriod1.SelectedIndex)) { EnableControl(true); return; }
            Thread.Sleep(500);
            if (ChbBet2.Checked && !Beting("2,,,,", "10", TxtBetAmount2.Text, LblCurrentPeriodId.Text, CbbPlan2.SelectedIndex, CbbPeriod2.SelectedIndex)) { EnableControl(true); return; }
            Thread.Sleep(500);
            if (ChbBet3.Checked && !Beting("3,,,,", "10", TxtBetAmount3.Text, LblCurrentPeriodId.Text, CbbPlan3.SelectedIndex, CbbPeriod3.SelectedIndex)) { EnableControl(true); return; }
            Thread.Sleep(500);
            if (ChbBet4.Checked && !Beting("4,,,,", "10", TxtBetAmount4.Text, LblCurrentPeriodId.Text, CbbPlan4.SelectedIndex, CbbPeriod4.SelectedIndex)) { EnableControl(true); return; }
            Thread.Sleep(500);
            if (ChbBet5.Checked && !Beting("5,,,,", "10", TxtBetAmount5.Text, LblCurrentPeriodId.Text, CbbPlan5.SelectedIndex, CbbPeriod5.SelectedIndex)) { EnableControl(true); return; }
            Thread.Sleep(500);
            if (ChbBet6.Checked && !Beting("6,,,,", "10", TxtBetAmount6.Text, LblCurrentPeriodId.Text, CbbPlan6.SelectedIndex, CbbPeriod6.SelectedIndex)) { EnableControl(true); return; }
            Thread.Sleep(500);
            if (ChbBet7.Checked && !Beting("7,,,,", "10", TxtBetAmount7.Text, LblCurrentPeriodId.Text, CbbPlan7.SelectedIndex, CbbPeriod7.SelectedIndex)) { EnableControl(true); return; }
            Thread.Sleep(500);
            if (ChbBet8.Checked && !Beting("8,,,,", "10", TxtBetAmount8.Text, LblCurrentPeriodId.Text, CbbPlan8.SelectedIndex, CbbPeriod8.SelectedIndex)) { EnableControl(true); return; }
            Thread.Sleep(500);
            if (ChbBet9.Checked && !Beting("9,,,,", "10", TxtBetAmount9.Text, LblCurrentPeriodId.Text, CbbPlan9.SelectedIndex, CbbPeriod9.SelectedIndex)) { EnableControl(true); return; }
            EnableControl(true);
            GetBalance();
        }
        private void BtnContinue_Click(object sender, EventArgs e)
        {
            AllBtnAddPeriodClick(true);
        }

        private void BtnAutoBet_Click(object sender, EventArgs e)
        {
            //AllBtnResetClick();
            bAutoBet = true;
            //bFirstAutoBet = true;
            GrbBetState.Enabled = false;

            BtnAutoBet.Enabled = false;
            BtnManualBet.Enabled = false;
            BtnStop.Enabled = true;
        }

        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            LsbLog.Items.Clear();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            bAutoBet = false;

            GrbBetState.Enabled = true;            

            BtnAutoBet.Enabled = true;
            BtnManualBet.Enabled = true;
            BtnStop.Enabled = false;            
        }

        private void BtnManualBet_Click(object sender, EventArgs e)
        {
            bAutoBet = false;           
 
            GrbBetState.Enabled = true;
            BtnManualBet.Enabled = false;
            BtnAutoBet.Enabled = false;
            BtnStop.Enabled = true;
        }

        private void BtnModifyUrl_Click(object sender, EventArgs e)
        {
            FrmConfigSetup frmTemp = new FrmConfigSetup();
            frmTemp.ShowDialog();
            SetUrl();
        }

    }
}
