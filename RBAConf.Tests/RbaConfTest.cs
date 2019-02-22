using NUnit.Framework;
using RBAConf;
using System.Collections.Generic;

namespace Tests
{
    public class RbaConfTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckAccess_ReturnTrue_Test()
        {
            var rbaConf = new RbaConf(
                new IRbacRole[] {
                    new RbacRole("r1")
                },
                new IRbacTask[] {
                    new RbacTask("t1", new RbacOperation("o1"), new BizRule(_ => _["str"] == "str"))
                },
                new IRbacOperation[] {
                    new RbacOperation("o1")
                }
            );
            Assert.IsTrue(rbaConf.CheckAccess("r1"));
        }

        [Test]
        public void CheckAccess2_ReturnTrue_Test()
        {
            var rbaConf = new RbaConf(
                new IRbacRole[] {
                    new RbacRole("r1")
                },
                new IRbacTask[] {
                    new RbacTask("t1", new RbacOperation("o1"), new BizRule(_ => _["str"] == "str"))
                },
                new IRbacOperation[] {
                    new RbacOperation("o1")
                }
            );
            Assert.IsTrue(rbaConf.CheckAccess("t1", new Dictionary<string, string>() { { "str", "str" } }));
        }

        [Test]
        public void CheckAccess3_ReturnTrue_Test()
        {
            var rbaConf = new RbaConf(
                new IRbacRole[] {
                    new RbacRole("r1")
                },
                new IRbacTask[] {
                    new RbacTask("t1", new RbacOperation("o1"), new BizRule(_ => _["str"] == "str"))
                },
                new IRbacOperation[] {
                    new RbacOperation("o1")
                }
            );
            Assert.IsTrue(rbaConf.CheckAccess("o1"));
        }

        [Test]
        public void CheckAccess_ReturnFalse_Test()
        {
            var oper = new RbacOperation("addTest");
            Assert.IsFalse(oper.CheckAccess("Fake"));
        }
    }
}