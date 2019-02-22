using System;
using System.Collections.Generic;
using System.Linq;

namespace RBAConf
{
    public sealed class RbacTask : IRbacTask
    {
        private readonly IRbacOperation operation;
        private readonly IBizRule biznessRule;

        public string Name { get; }

        public RbacTask(string taskName, IRbacOperation operation, Func<IDictionary<string, string>, bool> biznessRule)
            : this(taskName, operation, new BizRule(biznessRule))
        {
        }

        public RbacTask(string taskName, IRbacOperation operation, IBizRule biznessRule)
        {
            Name = taskName;
            this.operation = operation;
            this.biznessRule = biznessRule;
        }

        public bool CheckAccess(string name, IDictionary<string, string> parameters)
        {
            return (Name == name || operation.CheckAccess(name)) && biznessRule.Check(parameters);
        }
    }
}
