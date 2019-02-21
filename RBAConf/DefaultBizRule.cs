﻿using System;
using System.Collections.Generic;

namespace RBAConf
{
    public sealed class DefaultBizRule : IBizRule
    {
        private readonly Func<IDictionary<string, string>, bool> bizRuleLogic;

        public DefaultBizRule(Func<IDictionary<string, string>, bool> bizRuleLogic)
        {
            this.bizRuleLogic = bizRuleLogic;
        }

        public bool Check(IDictionary<string, string> parameters)
        {
            return bizRuleLogic.Invoke(parameters);
        }
    }
}
