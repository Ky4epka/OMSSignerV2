using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace OMSSigner
{
    public class Log
    {
        public delegate void MessageHandler(string message, LogLevel level);

        public static event MessageHandler LogHandlers;

        public static string MessageFormatter(string message, LogLevel level)
        {
            return string.Format("[{0} {1}] {2}", new object[3] { DateTime.Now.ToString("HH:mm:ss yy.MM.dd"), level.ToString(), message });
        }

        public static void _(string message, LogLevel level)
        {
            LogHandlers.Invoke(MessageFormatter(message, level), level);
        }

        public static void _(Exception e)
        {
            _(e, "");
        }

        public static void _(Exception e, string message)
        {
            _(message + e.Message, LogLevel.Error);
        }
    }
}
