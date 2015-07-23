using System;
using System.Collections.Generic;
using System.Net.Mail;
using S22.Imap;

namespace MailBoxMigrator.Core
{
    public interface IMailBox
    {
        /// <summary>
        /// Returns a connection
        /// </summary>
        /// <returns></returns>
        ImapClient Connect();

        /// <summary>
        /// Fetches a List of Mailboxes / folders on the IMAP account. 
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetList();

        /// <summary>
        /// Gets the mailbox Information
        /// </summary>
        /// <param name="mailBox"></param>
        /// <returns></returns>
        MailboxInfo GetMailBoxInfo(string mailBox);

        /// <summary>
        /// Creates a specified mailbox.
        /// </summary>
        /// <param name="mailbox"></param>
        void CreateMailBox(string mailbox);

        /// <summary>
        /// Retrieves email messages from a specified Inbox. 
        /// </summary>
        /// <param name="mailBoxToFetch"></param>
        /// <returns></returns>
        IEnumerable<MailMessage> FetchMessages(string mailBoxToFetch);

        /// <summary>
        /// Writes email message to the specified Inbox
        /// </summary>
        /// <param name="message"></param>
        /// <param name="mailBox"></param>
        void WriteMessage(MailMessage message, String mailBox);

        void Dispose();
    }
}