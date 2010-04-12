using System;

namespace ExportSales.Classes
{
    /// <summary>
    /// WindowHandleNotFoundException
    /// </summary>
    public class WindowNotFoundException : Exception
    {

        private string m_windowName;

        public WindowNotFoundException(string windowName)
        {
            m_windowName = windowName;
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <value></value>
        /// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
        /// <filterPriority>1</filterPriority>
        public override string Message
        {
            get
            {
                return "Das Fenster mit dem Namen " + m_windowName + " wurde nicht gefunden!";
            }
        }
    }
}