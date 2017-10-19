using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using Model;

namespace BetSetup
{
    public class LogInfo
    {
        public static void AddLog(ListBox LsbLog, string str)
        {
            string strWrite = DateTime.Now.ToString("HH:mm:ss") + " " + str;
            LsbLog.Items.Add(strWrite);

            if (!Directory.Exists("Log"))
            {
                Directory.CreateDirectory("Log");
            }

            FileStream fs = new FileStream(@"Log\Log_" + DateTime.Today.ToString("yyyyMMdd") + ".txt", FileMode.Append | FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(strWrite);
            sw.Close();
            fs.Close();
        }

        public static void WriteLogState(List<ModelBetState> lstBetState)
        {
            Stream s;
            BinaryFormatter bf;

            if (!Directory.Exists("State"))
            {
                Directory.CreateDirectory("State");
            }

            string strTime = "";
            //第二天
            if (int.Parse(DateTime.Now.ToString("HHmmss")) < 20000)
            {
                strTime = DateTime.Today.AddDays(-1).ToString("yyyyMMdd") + "10" + "-" + DateTime.Today.ToString("yyyyMMdd") + "02";
            }
            else
            {
                strTime = DateTime.Today.ToString("yyyyMMdd") + "10" + "-" + DateTime.Today.AddDays(1).ToString("yyyyMMdd") + "02";
            }

            s = File.Open(@"State\State_" + strTime + ".txt", FileMode.OpenOrCreate);
            bf = new BinaryFormatter();
            bf.Serialize(s, lstBetState);
            s.Close();
        }

        public static void ReadLogState(ref List<ModelBetState> lstBetState)
        {
            string strTime = "";
            //第二天
            if (int.Parse(DateTime.Now.ToString("HHmmss")) < 20000)
            {
                strTime = DateTime.Today.AddDays(-1).ToString("yyyyMMdd") + "10" + "-" + DateTime.Today.ToString("yyyyMMdd") + "02";
            }
            else
            {
                strTime = DateTime.Today.ToString("yyyyMMdd") + "10" + "-" + DateTime.Today.AddDays(1).ToString("yyyyMMdd") + "02";
            }

            if (File.Exists(@"State\State_" + strTime + ".txt"))
            {
                Stream s = File.Open(@"State\State_" + strTime + ".txt", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                lstBetState = (List<ModelBetState>)bf.Deserialize(s);
                s.Close();
            }            
        }

        public static void ReadBetSetup(ref List<ModelBet> lstBet1, ref List<ModelBet> lstBet2, 
                                        ref List<ModelBet> lstBet3, ref List<ModelBet> lstBet4,
                                        ref List<ModelBet> lstBet5, ref List<ModelBet> lstBet6)
        {
            Stream s;
            BinaryFormatter bf;

            if (File.Exists(@"Setup\BetSetup1.txt"))
            {
                s = File.Open(@"Setup\BetSetup1.txt", FileMode.Open);
                bf = new BinaryFormatter();
                lstBet1 = (List<ModelBet>)bf.Deserialize(s);
                s.Close();
            }

            if (File.Exists(@"Setup\BetSetup2.txt"))
            {
                s = File.Open(@"Setup\BetSetup2.txt", FileMode.Open);
                bf = new BinaryFormatter();
                lstBet2 = (List<ModelBet>)bf.Deserialize(s);
                s.Close();
            }

            if (File.Exists(@"Setup\BetSetup3.txt"))
            {
                s = File.Open(@"Setup\BetSetup3.txt", FileMode.Open);
                bf = new BinaryFormatter();
                lstBet3 = (List<ModelBet>)bf.Deserialize(s);
                s.Close();
            }

            if (File.Exists(@"Setup\BetSetup4.txt"))
            {
                s = File.Open(@"Setup\BetSetup4.txt", FileMode.Open);
                bf = new BinaryFormatter();
                lstBet4 = (List<ModelBet>)bf.Deserialize(s);
                s.Close();
            }       
            if (File.Exists(@"Setup\BetSetup5.txt"))
            {
                s = File.Open(@"Setup\BetSetup5.txt", FileMode.Open);
                bf = new BinaryFormatter();
                lstBet4 = (List<ModelBet>)bf.Deserialize(s);
                s.Close();
            }  
            if (File.Exists(@"Setup\BetSetup6.txt"))
            {
                s = File.Open(@"Setup\BetSetup6.txt", FileMode.Open);
                bf = new BinaryFormatter();
                lstBet4 = (List<ModelBet>)bf.Deserialize(s);
                s.Close();
            }       
        }

        public static void WriteBetSetup(List<ModelBet> lstBet1, List<ModelBet> lstBet2, 
                                        List<ModelBet> lstBet3, List<ModelBet> lstBet4,
                                        List<ModelBet> lstBet5, List<ModelBet> lstBet6)
        {
            Stream s;
            BinaryFormatter bf;
            if (!Directory.Exists("Setup"))
            {
                Directory.CreateDirectory("Setup");
            }

            s = File.Open(@"Setup\BetSetup1.txt", FileMode.OpenOrCreate);
            bf = new BinaryFormatter();
            bf.Serialize(s, lstBet1);
            s.Close();

            s = File.Open(@"Setup\BetSetup2.txt", FileMode.OpenOrCreate);
            bf = new BinaryFormatter();
            bf.Serialize(s, lstBet2);
            s.Close();

            s = File.Open(@"Setup\BetSetup3.txt", FileMode.OpenOrCreate);
            bf = new BinaryFormatter();
            bf.Serialize(s, lstBet3);
            s.Close();

            s = File.Open(@"Setup\BetSetup4.txt", FileMode.OpenOrCreate);
            bf = new BinaryFormatter();
            bf.Serialize(s, lstBet4);
            s.Close();

            s = File.Open(@"Setup\BetSetup5.txt", FileMode.OpenOrCreate);
            bf = new BinaryFormatter();
            bf.Serialize(s, lstBet3);
            s.Close();

            s = File.Open(@"Setup\BetSetup6.txt", FileMode.OpenOrCreate);
            bf = new BinaryFormatter();
            bf.Serialize(s, lstBet4);
            s.Close();
        }

        public static void ReadBetHistory(ref List<ModelHistory> lstModelHistory)
        {
            if (File.Exists(@"History\BetHistory.txt"))
            {
                Stream s = File.Open(@"History\BetHistory.txt", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                lstModelHistory = (List<ModelHistory>)bf.Deserialize(s);
                s.Close();
            }
            
        }

        public static void WriteBetHistory(List<ModelHistory> lstModelHistory)
        {
            if (!Directory.Exists("History"))
            {
                Directory.CreateDirectory("History");
            }

            Stream s = File.Open(@"History\BetHistory.txt", FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, lstModelHistory);
            s.Close();
        }

        public static void WriteCurrentState(params ComboBox[] cbb)
        {
            Stream s;
            BinaryFormatter bf;
            ModelCurrentBetState modelcurrentstate = new ModelCurrentBetState();
            modelcurrentstate.Bet0PlanID    = cbb[0].SelectedIndex;
            modelcurrentstate.Bet0PeriodID  = cbb[1].SelectedIndex;
            modelcurrentstate.Bet1PlanID    = cbb[2].SelectedIndex;
            modelcurrentstate.Bet1PeriodID  = cbb[3].SelectedIndex;
            modelcurrentstate.Bet2PlanID    = cbb[4].SelectedIndex;
            modelcurrentstate.Bet2PeriodID  = cbb[5].SelectedIndex;
            modelcurrentstate.Bet3PlanID    = cbb[6].SelectedIndex;
            modelcurrentstate.Bet3PeriodID  = cbb[7].SelectedIndex;
            modelcurrentstate.Bet4PlanID    = cbb[8].SelectedIndex;
            modelcurrentstate.Bet4PeriodID  = cbb[9].SelectedIndex;
            modelcurrentstate.Bet5PlanID    = cbb[10].SelectedIndex;
            modelcurrentstate.Bet5PeriodID  = cbb[11].SelectedIndex;
            modelcurrentstate.Bet6PlanID    = cbb[12].SelectedIndex;
            modelcurrentstate.Bet6PeriodID  = cbb[13].SelectedIndex;
            modelcurrentstate.Bet7PlanID    = cbb[14].SelectedIndex;
            modelcurrentstate.Bet7PeriodID  = cbb[15].SelectedIndex;
            modelcurrentstate.Bet8PlanID    = cbb[16].SelectedIndex;
            modelcurrentstate.Bet8PeriodID  = cbb[17].SelectedIndex;
            modelcurrentstate.Bet9PlanID    = cbb[18].SelectedIndex;
            modelcurrentstate.Bet9PeriodID  = cbb[19].SelectedIndex;

            if (!Directory.Exists("State"))
            {
                Directory.CreateDirectory("State");
            }

            s = File.Open(@"State\CurrentState.txt", FileMode.OpenOrCreate);
            bf = new BinaryFormatter();
            bf.Serialize(s, modelcurrentstate);
            s.Close();
        }

        public static ModelCurrentBetState ReadCurrentState()
        {
            ModelCurrentBetState modelcurrentstate = new ModelCurrentBetState();

            if (File.Exists(@"State\CurrentState.txt"))
            {
                Stream s = File.Open(@"State\CurrentState.txt", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                modelcurrentstate = (ModelCurrentBetState)bf.Deserialize(s);
                s.Close();
                return modelcurrentstate;
            }
            else
            {
                return null;
            }           
        }
    }
}
