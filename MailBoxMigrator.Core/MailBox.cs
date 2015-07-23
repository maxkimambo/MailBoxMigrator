using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using S22.Imap;

namespace MailBoxMigrator.Core
{
    public class MailBox :  IDisposable, IMailBox
    {
        private readonly ClientParameters _clientParameters;
        private readonly ImapClient _client; 

        public MailBox(ClientParameters clientParameters)
        {
            _clientParameters = clientParameters;
            _client = new ImapClient(_clientParameters.Host, _clientParameters.Port, _clientParameters.Username,
                    _clientParameters.Password, AuthMethod.Auto, true); 
        }

        /// <summary>
        /// Returns a connection
        /// </summary>
        /// <returns></returns>
        public ImapClient Connect()
        {
            try
            {
                    return _client;
            }
            catch (Exception)
            {

                return null; 
            }
        }
        /// <summary>
        /// Fetches a List of Mailboxes / folders on the IMAP account. 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetList()
        {
            try
            {
                    var mailboxes = _client.ListMailboxes();
                    return mailboxes; 
            }
            catch (Exception)
            {
                
                return null; 
            }
           
        }

      


        /// <summary>
        /// Gets the mailbox Information
        /// </summary>
        /// <param name="mailBox"></param>
        /// <returns></returns>
        public MailboxInfo GetMailBoxInfo(string mailBox)
        {
            var mailboxInfo = _client.GetMailboxInfo(mailBox);

            return mailboxInfo; 
        }

        /// <summary>
        /// Creates a specified mailbox.
        /// </summary>
        /// <param name="mailbox"></param>
        public void CreateMailBox(string mailbox)
        {
            try
            {
                _client.CreateMailbox(mailbox);
            }
            catch (BadServerResponseException ex)
            {
                
            }

        }

        /// <summary>
        /// Retrieves email messages from a specified Inbox. 
        /// </summary>
        /// <param name="mailBoxToFetch"></param>
        /// <returns></returns>
        public IEnumerable<MailMessage> FetchMessages(string mailBoxToFetch)
        {
                var mailboxes = _client.ListMailboxes();
                var mailbox = mailboxes.SingleOrDefault(m => m.Equals(mailBoxToFetch));
                var messagesIds = _client.Search(SearchCondition.All(), mailbox);

                foreach (var mId in messagesIds)
                {
                    var message = _client.GetMessage(mId, FetchOptions.Normal, true, mailbox);
                    yield return message; 
                }
            
        }


        /// <summary>
        /// Writes email message to the specified Inbox
        /// </summary>
        /// <param name="message"></param>
        /// <param name="mailBox"></param>
        public void WriteMessage(MailMessage message, String mailBox)
        {

            try
            {
                 _client.StoreMessage(message, true, mailBox); 
            }
            catch (BadServerResponseException serverResponse)
            {
                // Normally its the missing mailbox 
                _client.CreateMailbox(mailBox);
                _client.StoreMessage(message, true, mailBox); 
            }
        }

        public virtual void Dispose()
        {
           _client.Dispose();
           
        }
    }
}