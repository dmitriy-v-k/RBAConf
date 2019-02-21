using NUnit.Framework;
using RBAConf;
using System.Collections.Generic;

namespace Tests
{
    public class RbacTaskTest
    {
        private RbacTask task;

        [SetUp]
        public void Setup()
        {
            task = new RbacTask("addTest", new DefaultBizRule(_ => _["userId"] == _["newsOwnerId"]));
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
    }
}