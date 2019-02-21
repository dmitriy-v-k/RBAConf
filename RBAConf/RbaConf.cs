using System.Collections.Generic;
using System.Linq;

namespace RBAConf
{
    public sealed class RbaConf : IRbaConf
    {
        private readonly IEnumerable<IRbacRole> rbacRoles;
        private readonly IEnumerable<IRbacTask> rbacTasks;
        private readonly IEnumerable<IRbacOperation> rbacOperations;

        public RbaConf(IEnumerable<IRbacRole> rbacRoles, IEnumerable<IRbacTask> rbacTasks, IEnumerable<IRbacOperation> rbacOperations)
        {
            this.rbacRoles = rbacRoles;
            this.rbacTasks = rbacTasks;
            this.rbacOperations = rbacOperations;
        }

        public bool CheckAccess(string name, IDictionary<string, string> parameters)
        {
            return rbacOperations.Any(_ => _.CheckAccess(name))
                || rbacTasks.Any(_ => _.CheckAccess(name, parameters))
                || rbacRoles.Any(_ => _.CheckAccess(name, parameters));
        }

        public bool CheckAccess(string name)
        {
            return CheckAccess(name, new Dictionary<string, string>());
        }
    }
}
