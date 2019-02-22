using NUnit.Framework;
using RBAConf;
using System.Collections.Generic;

namespace Tests
{
    public static class TestClass
    {
        public static bool BizRuleLogic(IDictionary<string, string> parameters)
        {
            return parameters["test"] == "test";
        }
    }

    public class RbacTaskTest
    {
        private RbacTask task;

        [SetUp]
        public void Setup()
        {
            task = new RbacTask("addTestBiz", new RbacOperation("addTest"), new BizRule(_ => _["userId"] == _["newsOwnerId"]));
        }

        [Test]
        public void CheckAccess_ReturnTrue_Test()
        {
            Assert.IsTrue(task.CheckAccess("addTest", new Dictionary<string, string>() {
                { "userId", "1"},
                { "newsOwnerId", "1"}
            }));
        }

        [Test]
        public void CheckAccess_IncorrectName_ReturnFalse_Test()
        {
            Assert.IsFalse(task.CheckAccess("addTest1", new Dictionary<string, string>() {
                { "userId", "1"},
                { "newsOwnerId", "1"}
            }));
        }

        [Test]
        public void CheckAccess_bizRuleReturnFalse_ReturnFalse_Test()
        {
            Assert.IsFalse(task.CheckAccess("addTest", new Dictionary<string, string>() {
                { "userId", "1"},
                { "newsOwnerId", "2"}
            }));
        }

        [Test]
        public void CheckAccess_withDelegateAsString_ReturnTrue_Test()
        {
            var task = new RbacTask("addTestBiz", new RbacOperation("addTest"), new BizRuleFromString("Tests.TestClass.BizRuleLogic, RBAConf.Tests"));
            Assert.IsTrue(task.CheckAccess("addTest", new Dictionary<string, string>() {
                { "test", "test"},
            }));
        }
    }
}