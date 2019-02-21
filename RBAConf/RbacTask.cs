using System;
using System.Collections.Generic;

namespace RBAConf
{
    public sealed class RbacTask : IRbacTask
    {
        private readonly IRbacOperation operation;
        private readonly IBizRule biznessRule;

        public RbacTask(string operation, Func<IDictionary<string, string>, bool> biznessRule)
            : this(operation, new DefaultBizRule(biznessRule))
        {
        }

        public RbacTask(string operation, IBizRule biznessRule)
            :this(new RbacOperation(operation), biznessRule)
        {
        }

        public RbacTask(IRbacOperation operation, IBizRule biznessRule)
        {
            this.operation = operation;
            this.biznessRule = biznessRule;
        }

        public bool CheckAccess(string name, IDictionary<string, string> parameters)
        {
            return operation.CheckAccess(name) && biznessRule.Check(parameters);
        }

        public bool CheckAccess(string name)
        {
            return CheckAccess(name, new Dictionary<string,string>());
        }
    }
}
