using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace MailBoxMigrator.Utils
{
    public interface ILogWriter
    {
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
    }

    public class LogWriter : ILogWriter
    {

       private static Logger logger = LogManager.GetCurrentClassLogger();

     
       public  void Debug(string message)
       {
           logger.Debug(message);
       }
       
       public  void Info(string message)
       {
           logger.Info(message);
       }

       public  void Warning(string message)
       {
           logger.Warn(message);
       }

       public  void Error(string message)
       {
           logger.Error(message);
       }


   }
}
