using System;
using System.Collections.Generic;

namespace RBAConf
{
    public sealed class BizRule : IBizRule
    {
        private readonly Func<IDictionary<string, string>, bool> bizRuleLogic;

        public BizRule(Func<IDictionary<string, string>, bool> bizRuleLogic)
        {
            this.bizRuleLogic = bizRuleLogic;
        }

        public bool Check(IDictionary<string, string> parameters)
        {
            return bizRuleLogic.Invoke(parameters);
        }
    }
}
