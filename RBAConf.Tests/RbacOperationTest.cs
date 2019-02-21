using NUnit.Framework;
using RBAConf;

namespace Tests
{
    public class RbacOperationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckAccess_ReturnTrue_Test()
        {
            var oper = new RbacOperation("addTest");
            Assert.IsTrue(oper.CheckAccess("addTest"));
        }

        [Test]
        public void CheckAccess_ReturnFalse_Test()
        {
            var oper = new RbacOperation("addTest");
            Assert.IsFalse(oper.CheckAccess("Fake"));
        }
    }
}