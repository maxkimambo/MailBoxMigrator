using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MailBoxMigrator.Utils;

namespace MailBoxMigrator.Core
{
    public class MigrationService
    {
        private readonly ILogWriter _logger;
        private readonly IMailBox _sourceMailAccount;
        private readonly IMailBox _destinationMailAccount;
        private IEnumerable<String> _listOfMailBoxes; 

        public MigrationService(ILogWriter logger, IMailBox sourceMailAccount, IMailBox destinationMailAccount)
        {
            _logger = logger;
            _sourceMailAccount = sourceMailAccount;
            _destinationMailAccount = destinationMailAccount;
        }


        public void MigrateAccount()
        {
            // Get the list of mailboxes 
           
            _listOfMailBoxes = _sourceMailAccount.GetList().ToArray();
           var mailboxesToProcess =  _listOfMailBoxes.Where(i => i.ToLower().Contains("inbox") || i.ToLower().Contains("sent")); 

            
            // place holder in case we would like to display to the user to select target folder. 
            var destinationFolders = _destinationMailAccount.GetList().ToArray();


            foreach (var mailBox in mailboxesToProcess)
            {
                var destinationMailbox =   CreateMailBoxAtDestination(mailBox);

                _logger.Info(String.Format("Started migrating {0}", mailBox));

                Console.WriteLine("Started migrating {0}", mailBox);

                if (!ShouldIgnore(mailBox))
                {
                     ProcessMailBox(mailBox, destinationMailbox);
                }

                _logger.Info(String.Format("Finished migrating {0}", mailBox));
            }
            // write them to the destination 
        }

        private string CreateMailBoxAtDestination(string mailBox)
        {
            if (!ShouldIgnore(mailBox))
            {
                var folder = mailBox.Replace("/", ".");
                folder = "INBOX." + folder; 
                _destinationMailAccount.CreateMailBox(folder);
                _logger.Info(String.Format("Created folder : {0}", mailBox));
                return folder; 
            }
            else
            {
                _logger.Info(String.Format("Ignoring folder : {0}", mailBox));
            }
            return string.Empty; 
        }
      


        private bool ShouldIgnore(string folderName)
        {
            var folderToIgnore = new string[] { "junk", "drafts", "chats", "deleted" };
            return folderToIgnore.Any(f => f.Contains(folderName.ToLower())); 

        }
        private IEnumerable<string> FormatMailBoxNames(IEnumerable<string> listOfMailBoxes)
        {
            var mailboxes = new List<string>(); 

            foreach (var folder in listOfMailBoxes)
            {
                var folderName = RenameDestinationFolder(folder);
                mailboxes.Add(folderName);
            }
            return mailboxes; 
        }

        private static string RenameDestinationFolder(string folder)
        {
            string folderName;
            if (folder.StartsWith("."))
            {
                folderName = "INBOX.Migration" + folder;
            }
            else
            {
                folderName = "INBOX.Migration." + folder;
            }
            return folderName;
        }


        private void ProcessMailBox(string mailBox, string destinationMailBox)
        {
            foreach (var mailMessage in _sourceMailAccount.FetchMessages(mailBox))
            {
                _destinationMailAccount.WriteMessage(mailMessage, destinationMailBox);
                Debug.WriteLine(String.Format("Processed message from {0}, {1}", mailMessage.From, mailMessage.Subject));
                Console.WriteLine(String.Format("Processed message from {0}, {1}", mailMessage.From, mailMessage.Subject));
                _logger.Info(String.Format("Processed message from {0}, {1}",mailMessage.From, mailMessage.Subject));
            }
        }

       
    }
}
