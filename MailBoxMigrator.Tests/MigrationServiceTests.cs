using System;
using MailBoxMigrator.Core;
using MailBoxMigrator.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailBoxMigrator.Tests
{
    [TestClass]
    public class MigrationServiceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var sourceAccountParams = new ClientParameters
            {
                Host = "",
                Port = 993,
                Password = "",
                Username = "",
                SSL = true
            }; 

            var destinationAccountParams =  new ClientParameters
            {
                Host = "",
                Port = 993,
                Password = "",
                Username = "",
                SSL = true
            };

            var sourceMailBox = new MailBox(sourceAccountParams);
            var destinationMailBox = new MailBox(destinationAccountParams); 

            var sut = new MigrationService(new LogWriter(),sourceMailBox, destinationMailBox ); 
            
            sut.MigrateAccount();

        }
    }
}
