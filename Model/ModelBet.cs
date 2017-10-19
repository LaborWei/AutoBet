using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ModelBet
    {
        public int MyPlanId
        {
            get;
            set;
        }

        public string MyPlanDescription
        {
            get
            {
                if (MyPlanId > 0)
                {
                    return "方案" + MyPlanId;
                }
                else
                {
                    return "未投注";
                }
            }
        }

        public int MyPeriodId
        {
            get;
            set;
        }

        public string MyPeriodDescription
        {               
            get
            {
                if (MyPeriodId > 0)
                {
                    if (MyPeriodId == 10000)
                    {
                        return "暂停投注";
                    }
                    return "第" + MyPeriodId + "期";
                }
                else
                {
                    return "未投注";
                }
            }
        }
        
        public string MyBetAmountPerBet
        {
            get;
            set;
        }

        //public string MyBetNumber
        //{
        //    get;
        //    set;
        //}

        public string MyBetTotalAmount
        {
            get;
            set;
        }
    }
}
