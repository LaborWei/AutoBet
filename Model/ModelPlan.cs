using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ModelPlan
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
                {
                    return "未投注";
                }
            }
        }
    }
}
