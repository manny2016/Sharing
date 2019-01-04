using Sharing.Core.Configuration;
using System;
using System.Configuration;
using Sharing.Core;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var section = new DberverConfigurationSection()
            {
                MasterDatabaseServer = new DatabaseServer() { Type = Sharing.Core.DatabaseTypes.MySql }
            };
            
            var str = section.SerializeToXml();
        }
    }
}
