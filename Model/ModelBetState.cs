using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
      [Serializable]
    public class ModelBetState
    {
        public string Period { get; set; }       
        public string DrawingDateTime { get; set; }
        public string DrawingNumber { get; set; }
        public string TenThousand  {  get {  return (DrawingNumber == null || DrawingNumber.Length < 1) ? "": DrawingNumber.Substring(0, 1) ; } }
        public int Bet0PlanID { get; set; }
        public int Bet0PlanDescription { get; set; }
        public int Bet0PeriodID { get; set; }
        public int Bet0PeriodDescription { get; set; }
        public string Bet0Amount { get; set; }
        public int Bet1PlanID { get; set; }
        public int Bet1PlanDescription { get; set; }
        public int Bet1PeriodID { get; set; }
        public int Bet1PeriodDescription { get; set; }
        public string Bet1Amount { get; set; }
        public int Bet2PlanID { get; set; }
        public int Bet2PlanDescription { get; set; }
        public int Bet2PeriodID { get; set; }
        public int Bet2PeriodDescription { get; set; }
        public string Bet2Amount { get; set; }
        public int Bet3PlanID { get; set; }
        public int Bet3PlanDescription { get; set; }
        public int Bet3PeriodID { get; set; }
        public int Bet3PeriodDescription { get; set; }
        public string Bet3Amount { get; set; }
        public int Bet4PlanID { get; set; }
        public int Bet4PlanDescription { get; set; }
        public int Bet4PeriodID { get; set; }
        public int Bet4PeriodDescription { get; set; }
        public string Bet4Amount { get; set; }
        public int Bet5PlanID { get; set; }
        public int Bet5PlanDescription { get; set; }
        public int Bet5PeriodID { get; set; }
        public int Bet5PeriodDescription { get; set; }
        public string Bet5Amount { get; set; }
        public int Bet6PlanID { get; set; }
        public int Bet6PlanDescription { get; set; }
        public int Bet6PeriodID { get; set; }
        public int Bet6PeriodDescription { get; set; }
        public string Bet6Amount { get; set; }
        public int Bet7PlanID { get; set; }
        public int Bet7PlanDescription { get; set; }
        public int Bet7PeriodID { get; set; }
        public int Bet7PeriodDescription { get; set; }
        public string Bet7Amount { get; set; }
        public int Bet8PlanID { get; set; }
        public int Bet8PlanDescription { get; set; }
        public int Bet8PeriodID { get; set; }
        public int Bet8PeriodDescription { get; set; }
        public string Bet8Amount { get; set; }
        public int Bet9PlanID { get; set; }
        public int Bet9PlanDescription { get; set; }
        public int Bet9PeriodID { get; set; }
        public int Bet9PeriodDescription { get; set; }
        public string Bet9Amount { get; set; }

        public decimal TotalAmount { get; set; }

    }
}
