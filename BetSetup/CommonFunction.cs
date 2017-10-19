using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace BetSetup
{
    public class CommonFunction
    {


        public static void GetTotal(string strPeriod, List<ModelBetState>  lstBetState)
        {
            decimal dallamount;

            List<ModelBetState> lsttmp = lstBetState.Where(p => p.Period == strPeriod).ToList();
            if (lsttmp.Count() > 0)
            {
                for (int i = 0; i < lsttmp.Count; i++)
                {

                    decimal dAmount0 = string.IsNullOrEmpty(lsttmp[i].Bet0Amount) ? 0 : decimal.Parse(lsttmp[i].Bet0Amount);
                    decimal dAmount1 = string.IsNullOrEmpty(lsttmp[i].Bet1Amount) ? 0 : decimal.Parse(lsttmp[i].Bet1Amount);
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
                    lsttmp[i].TotalAmount = -1 * dallamount;
                    decimal dwin = 0;
                    string strTemp = lsttmp[i].TenThousand; //(strCode == null || strCode.Length < 1) ? "" : strCode.Substring(0, 1);
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
        }
    }
}
