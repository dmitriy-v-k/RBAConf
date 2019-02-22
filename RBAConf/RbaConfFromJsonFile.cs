using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace RBAConf
{
    public sealed class RbaConfFromJsonFile : IRbaConf
    {
        public sealed class RbacTaskDTO
        {
            public string Name { get; set; }
            public string Operation { get; set; }
            public string BizRule { get; set; }
        }

        public sealed class RbacRoleDTO
        {
            public string Name { get; set; }
            public IEnumerable<string> Operations { get; set; }
            public IEnumerable<string> Tasks { get; set; }
            public IEnumerable<string> Roles { get; set; }
        }

        private IRbaConf rbaConf;
        private readonly string filePath;

        public RbaConfFromJsonFile(string filePath)
        {
            this.filePath = filePath;
        }

        public bool CheckAccess(string name, IDictionary<string, string> parameters)
        {
            return GetRbaConf().CheckAccess(name, parameters);
        }

        public bool CheckAccess(string name)
        {
            return GetRbaConf().CheckAccess(name);
        }

        private IRbaConf GetRbaConf()
        {
            if(rbaConf == null)
            {
                (var rbacRoles, var rbacTasks, var rbacOperations) = LoadAndParsingFile(filePath);
                rbaConf = new RbaConf(rbacRoles, rbacTasks, rbacOperations);
            }
            return rbaConf;
        }

        private (IEnumerable<IRbacRole>, IEnumerable<IRbacTask>, IEnumerable<IRbacOperation>) LoadAndParsingFile(string filePath) {
            var objectTemplate = new
            {
                roles = Enumerable.Empty<RbacRoleDTO>(),
                tasks = Enumerable.Empty<RbacTaskDTO>(),
                operations = Enumerable.Empty<string>()
            };
            var dataObject = JsonConvert.DeserializeAnonymousType(File.ReadAllText(filePath), objectTemplate);

            var rbacOperations = dataObject.operations.Select(_ => new RbacOperation(_));
            var rbacTasks = dataObject.tasks.Select(_ => new RbacTask(
                _.Name,
                rbacOperations.Single(op => op.Name == _.Operation),
                new BizRuleFromString(_.BizRule)
            ));
            var rbacRoles = new List<IRbacRole>();

            foreach (var role in dataObject.roles)
            {
                rbacRoles.Add(new RbacRole(
                    role.Name,
                    rbacOperations.Where(_ => role.Operations?.Contains(_.Name) ?? false),
                    rbacTasks.Where(_ => role.Tasks?.Contains(_.Name) ?? false),
                    rbacRoles.Where(_ => role.Roles?.Contains(_.Name) ?? false)
                ));
            }

            return (rbacRoles, rbacTasks, rbacOperations);
        }
    }
}
