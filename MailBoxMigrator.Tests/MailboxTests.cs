using System.Linq;
using System.Reflection;
using MailBoxMigrator.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using S22.Imap;

namespace MailBoxMigrator.Tests
{
    [TestClass]
    public class MailboxTests
    {
        private MailBox mailbox;
        private MailBox destinationMailBox; 

        [TestInitialize]
        public void Init()
        {
            var sourceClientParameters = new ClientParameters
            {
                Host = "",
                Password = "",
                Port = 993,
                Username = ""
            }; 

            mailbox = new MailBox(sourceClientParameters);

            var destinationClientParameters = new ClientParameters
            {
                Host = "",
                Password = "",
                Port = 993,
                Username = ""
            }; 

            destinationMailBox = new MailBox(destinationClientParameters);    

        }

        [TestMethod]
        public void Can_Connect_to_MailBox()
        {
            
            var result = mailbox.Connect(); 

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void Can_Get_The_List_Of_MailBoxes()
        {
            var mailboxes = mailbox.GetList(); 
            Assert.IsTrue(mailboxes.ToList().Count> 1);


            var destinationMailboxes = destinationMailBox.GetList(); 


        }

       [TestMethod]
        public void Can_Write_Message_To_MailBox()
        {
            foreach (var mailMessage in mailbox.FetchMessages("INBOX"))
            {
               destinationMailBox.WriteMessage(mailMessage, "INBOX");    
            }
        }

    }
}
