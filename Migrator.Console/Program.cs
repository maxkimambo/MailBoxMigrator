using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailBoxMigrator.Core;
using MailBoxMigrator.Utils;

namespace Migrator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Starting migration");

            var sourceAccountParams = new ClientParameters
            {
                Host = "kimambo.com",
                Port = 993,
                Password = "6o8in4ooD@78",
                Username = "gerald@feldman.co.tz",
                SSL = true
            };

            var destinationAccountParams = new ClientParameters
            {
                Host = "n1plcpnl0045.prod.ams1.secureserver.net",
                Port = 993,
                Password = "6o8in4ooD@78",
                Username = "gerald@feldman.co.tz",
                SSL = true
            };

            var sourceMailBox = new MailBox(sourceAccountParams);
            var destinationMailBox = new MailBox(destinationAccountParams);

            var migration = new MigrationService(new LogWriter(), sourceMailBox, destinationMailBox); 
            migration.MigrateAccount();

            System.Console.ReadLine(); 
        }
    }
}
