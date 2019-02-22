using System.Collections.Generic;
using System.Linq;

namespace RBAConf
{
    public sealed class RbacRole : IRbacRole
    {
        private readonly IEnumerable<IRbacRole> rbacRoles;
        private readonly IEnumerable<IRbacTask> rbacTasks;
        private readonly IEnumerable<IRbacOperation> rbacOperations;

        public string Name { get; }

        public RbacRole(string roleName)
            :this(roleName, Enumerable.Empty<IRbacOperation>())
        {
        }

        public RbacRole(string roleName, IEnumerable<IRbacOperation> rbacOperations)
            : this(roleName, rbacOperations, Enumerable.Empty<IRbacTask>())
        {
        }

        public RbacRole(string roleName, IEnumerable<IRbacOperation> rbacOperations, IEnumerable<IRbacTask> rbacTasks)
            : this(roleName, rbacOperations, rbacTasks, Enumerable.Empty<IRbacRole>())
        {
        }

        public RbacRole(string roleName, IEnumerable<IRbacOperation> rbacOperations, IEnumerable<IRbacTask> rbacTasks, IEnumerable<IRbacRole> rbacRoles)
        {
            Name = roleName;
            this.rbacRoles = rbacRoles;
            this.rbacTasks = rbacTasks;
            this.rbacOperations = rbacOperations;
        }

        public bool CheckAccess(string name, IDictionary<string,string> parameters)
        {
            return name == Name
                || rbacOperations.Any(_ => _.CheckAccess(name))
                || rbacTasks.Any(_ => _.CheckAccess(name, parameters))
                || rbacRoles.Any(_ => _.CheckAccess(name, parameters));
        }

        public bool CheckAccess(string name)
        {
            return CheckAccess(name, new Dictionary<string, string>());
        }
    }
}
