using Newtonsoft.Json;
using NUnit.Framework;
using RBAConf;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    public class RbaConfFromJsonFileTest
    {
        private string path;
        private RbaConfFromJsonFile config;

        [SetUp]
        public void Setup()
        {
            path = "test.json";

            var objectTemplate = new
            {
                roles = new List<RbaConfFromJsonFile.RbacRoleDTO>()
                {
                    new RbaConfFromJsonFile.RbacRoleDTO()
                    {
                        Name = "r1",
                        Operations = new List<string>()
                        {
                            "o3"
                        }
                    }
                },
                tasks = new List<RbaConfFromJsonFile.RbacTaskDTO>()
                {
                    new RbaConfFromJsonFile.RbacTaskDTO()
                    {
                        Name = "t1",
                        Operation = "o1",
                        BizRule = "Tests.TestClass.BizRuleLogic, RBAConf.Tests"
                    }
                },
                operations = new List<string>()
                {
                    "o1",
                    "o3"
                }
            };

            var dataString = JsonConvert.SerializeObject(objectTemplate);
            File.WriteAllText(path, dataString);

            config = new RbaConfFromJsonFile(path);
        }

        [Test]
        public void CheckAccess_ReturnTrue_Test()
        {
            Assert.IsTrue(config.CheckAccess("r1"));
        }

        [Test]
        public void CheckAccess2_ReturnTrue_Test()
        {
            Assert.IsTrue(config.CheckAccess("t1", new Dictionary<string, string>() { { "test", "test" } }));
        }

        [Test]
        public void CheckAccess3_ReturnTrue_Test()
        {
            Assert.IsTrue(config.CheckAccess("o1"));
        }

        [Test]
        public void CheckAccess4_ReturnTrue_Test()
        {
            Assert.IsTrue(config.CheckAccess("o3"));
        }

        [Test]
        public void CheckAccess_ReturnFalse_Test()
        {
            Assert.IsFalse(config.CheckAccess("Fake"));
        }

        [Test]
        public void CheckAccess2_ReturnFalse_Test()
        {
            Assert.IsFalse(config.CheckAccess("t1", new Dictionary<string, string>() { { "test", "test1" } }));
        }
    }
}