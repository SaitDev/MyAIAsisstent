using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace MyAssistant.Utils
{
    public class DebugHelper
    {
        public static void Log(string msg)
        {
            Debug.WriteLine("[" + TimeNow + "] " + msg);
        }

        public static void Log(object value)
        {
            Debug.WriteLine("[" + TimeNow + "] " + value.ToString());
        }

        public static void File(string msg)
        {
            System.IO.File.AppendAllText("logs.txt", "[" + TimeNow + "] " + msg + Environment.NewLine);
        }

        static string TimeNow
        {
            get => DateTime.Now.ToString("HH:mm:ss.fff");
        }
    }
}
