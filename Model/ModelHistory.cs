using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ModelHistory
    {
        public string Period
        {
            get;
            set;
        }

        public string DrawingDateTime
        {
            get;
            set;
        }

        public string DrawingNumber
        {
            get;
            set;
        }

        public string IsWin
        {
            get;
            set;
        }

        public string MyPeriodId
        {
            get;
            set;
        }

        public string MyPeriodDescription
        {
            get;
            set;
        }

        public string MyBetNumber
        {
            get;
            set;
        }

        public string MyBetAmountPerBet
        {
            get;
            set;
        }

        public string MyBetTotalAmount
        {
            get;
            set;
        }
    }
}
