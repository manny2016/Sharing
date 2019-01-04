
namespace Sharing.Core.Tests.Database
{
    using System;
    using System.Collections.Generic;
    using Sharing.Core;
    public class DatabaseTestHelper
    {
        public static void InitializeDBSchema()
        {
            using (var database = SharingConfigurations.GenerateDatabase("sharing-uat", true))
            {
                var executeSql = @"
DROP TABLE IF EXISTS Test;
CREATE TABLE `Test` (
`Id`  bigint NOT NULL AUTO_INCREMENT ,
`Code`  varchar(255) NULL ,
`Name`  varchar(255) NULL ,
PRIMARY KEY (`Id`)
);";
                database.Execute(executeSql);
            }
        }
        public static void Cleanup()
        {
            var executeSql = "DROP TABLE IF EXISTS Test;";
            using(var database = SharingConfigurations.GenerateDatabase("sharing-uat", true))
            {
                database.Execute(executeSql);
            }
               

        }
    }
}
