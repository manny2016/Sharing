using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sharing.Core.Tests.Database;
using Sharing.Core.Tests.Database.Entities;

namespace Sharing.Core.Tests
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void InsertOrUpdate()
        {
            var entity = new TestEntity() { Code = "T01", Name = "Sod" };
            using (var database = SharingConfigurations.GenerateDatabase("sharing-uat", true))
            {
                var executeSql = "INSERT INTO Test(Code,Name) VALUES(@Code,@Name)";
                var rtnVal = database.Execute(executeSql, entity);
                Assert.AreEqual(1, rtnVal);
            }
        }
        [TestMethod]
        public void Select()
        {

        }
        [TestInitialize]
        public void Initialize()
        {
            DatabaseTestHelper.InitializeDBSchema();
        }
        [TestCleanup]
        public void Cleanup()
        {
            //DatabaseTestHelper.Cleanup();
        }
    }
}
