using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RBAConf
{
    public sealed class BizRuleFromString : IBizRule
    {
        private readonly IBizRule bizRule;

        public BizRuleFromString(string bizRulePath)
        {
            bizRule = new BizRule(GetFuncByName(bizRulePath));
        }

        public bool Check(IDictionary<string, string> parameters)
        {
            return bizRule.Check(parameters);
        }

        private Func<IDictionary<string, string>, bool> GetFuncByName(string methodPath)
        {
            return (Func<IDictionary<string, string>, bool>)Delegate.CreateDelegate(
                typeof(Func<IDictionary<string, string>, bool>),
                GetMethodInfoByPath(methodPath)
            );
        }

        private MethodInfo GetMethodInfoByPath(string methodPath)
        {
            var assemblyName = methodPath.Split(',').Last();
            var className = string.Join('.', methodPath.Split(',').First().Trim().Split('.').SkipLast(1));
            var typeName = string.Join(',', className, assemblyName);
            var methodName = methodPath.Split(',').First().Trim().Split('.').Last();
            return Type.GetType(typeName, true).GetMethod(methodName);
        }
    }
}
