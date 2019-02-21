using NUnit.Framework;
using RBAConf;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class RbacRoleTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckAccess_ReturnTrue_Test()
        {
            var role = new RbacRole("testRole");
            Assert.IsTrue(role.CheckAccess("testRole"));
        }

        [Test]
        public void CheckAccess_ReturnFalse_Test()
        {
            var role = new RbacRole("testRole");
            Assert.IsFalse(role.CheckAccess("Fake"));
        }

        [Test]
        public void CheckAccess_WithOperation_ReturnTrue_Test()
        {
            var role = new RbacRole("testRole", new IRbacOperation[] {
                new RbacOperation("op1"),
                new RbacOperation("op2")
            });
            Assert.IsTrue(role.CheckAccess("op1"));
        }

        [Test]
        public void CheckAccess_WithOperation2_ReturnTrue_Test()
        {
            var role = new RbacRole("testRole", new IRbacOperation[] {
                new RbacOperation("op1"),
                new RbacOperation("op2")
            });
            Assert.IsTrue(role.CheckAccess("op2"));
        }

        [Test]
        public void CheckAccess_WithOperation_ReturnFalse_Test()
        {
            var role = new RbacRole("testRole", new IRbacOperation[] {
                new RbacOperation("op1"),
                new RbacOperation("op2")
            });
            Assert.IsFalse(role.CheckAccess("Fake"));
        }

        [Test]
        public void CheckAccess_WithTasks_ReturnTrue_Test()
        {
            var role = new RbacRole("testRole", Enumerable.Empty<IRbacOperation>(), new IRbacTask[] {
                new RbacTask("t1",new DefaultBizRule(_ => !string.IsNullOrEmpty(_["id"]))),
                new RbacTask("t2",new DefaultBizRule(_ => !string.IsNullOrEmpty(_["id"])))
            });
            Assert.IsTrue(role.CheckAccess("t1", new Dictionary<string, string>() {
                { "id" , "test"}
            }));
        }

        [Test]
        public void CheckAccess_WithTasks2_ReturnTrue_Test()
        {
            var role = new RbacRole("testRole", Enumerable.Empty<IRbacOperation>(), new IRbacTask[] {
                new RbacTask("t1",new DefaultBizRule(_ => !string.IsNullOrEmpty(_["id"]))),
                new RbacTask("t2",new DefaultBizRule(_ => !string.IsNullOrEmpty(_["id"])))
            });
            Assert.IsTrue(role.CheckAccess("t2",new Dictionary<string, string>() {
                { "id" , "test"}
            }));
        }

        [Test]
        public void CheckAccess_WithTasks_ReturnFalse_Test()
        {
            var role = new RbacRole("testRole", Enumerable.Empty<IRbacOperation>(), new IRbacTask[] {
                new RbacTask("t1",new DefaultBizRule(_ => !string.IsNullOrEmpty(_["id"])))
            });
            Assert.IsFalse(role.CheckAccess("t3", new Dictionary<string, string>() {
                { "id" , "test"}
            }));
        }

        [Test]
        public void CheckAccess_WithTasks_bizRuleReturnFalse_ReturnFalse_Test()
        {
            var role = new RbacRole("testRole", Enumerable.Empty<IRbacOperation>(), new IRbacTask[] {
                new RbacTask("t1",new DefaultBizRule(_ => !string.IsNullOrEmpty(_["id"]))),
                new RbacTask("t2",new DefaultBizRule(_ => !string.IsNullOrEmpty(_["id"])))
            });
            Assert.IsFalse(role.CheckAccess("t1", new Dictionary<string, string>() {
                { "id" , null}
            }));
        }

        [Test]
        public void CheckAccess_WithRoles_ReturnTrue_Test()
        {
            var role = new RbacRole("testRole", Enumerable.Empty<IRbacOperation>(), Enumerable.Empty<IRbacTask>(), new IRbacRole[] {
                new RbacRole("r1"),
                new RbacRole("r2",new IRbacOperation[] {
                    new RbacOperation("op1"),
                    new RbacOperation("op2")
                })
            });
            Assert.IsTrue(role.CheckAccess("r1"));
        }

        [Test]
        public void CheckAccess_WithRoles2_ReturnTrue_Test()
        {
            var role = new RbacRole("testRole", Enumerable.Empty<IRbacOperation>(), Enumerable.Empty<IRbacTask>(), new IRbacRole[] {
                new RbacRole("r1"),
                new RbacRole("r2",new IRbacOperation[] {
                    new RbacOperation("op1"),
                    new RbacOperation("op2")
                })
            });
            Assert.IsTrue(role.CheckAccess("r2"));
        }

        [Test]
        public void CheckAccess_WithRoles3_ReturnTrue_Test()
        {
            var role = new RbacRole("testRole", Enumerable.Empty<IRbacOperation>(), Enumerable.Empty<IRbacTask>(), new IRbacRole[] {
                new RbacRole("r1"),
                new RbacRole("r2",new IRbacOperation[] {
                    new RbacOperation("op1"),
                    new RbacOperation("op2")
                })
            });
            Assert.IsTrue(role.CheckAccess("op1"));
        }

        [Test]
        public void CheckAccess_WithRoles4_ReturnTrue_Test()
        {
            var role = new RbacRole("testRole", Enumerable.Empty<IRbacOperation>(), Enumerable.Empty<IRbacTask>(), new IRbacRole[] {
                new RbacRole("r1"),
                new RbacRole("r2",Enumerable.Empty<IRbacOperation>(), new IRbacTask[] {
                new RbacTask("t1",new DefaultBizRule(_ => _["id"] == "id1"))
            })
            });
            Assert.IsTrue(role.CheckAccess("t1", new Dictionary<string, string>() {
                { "id" , "id1"}
            }));
        }
    }
}