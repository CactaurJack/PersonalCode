using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Logging
{
    /// <summary>
    /// This is just currently to log events to make sure that's working properly, so I'm not concerned about making this
    /// too robust. If we need better logging, I can come back and expand on this quite a bit.
    /// -Ben
    /// </summary>
    public class FileSystemLog : GameComponent, ILog
    {
        public FileSystemLog(Game game) : base(game)
        {
            FileStream fileStream = File.Open("debugLog.txt", FileMode.Create, FileAccess.Write);
            StreamWriter file = new StreamWriter(fileStream);
            m_file = StreamWriter.Synchronized(file);
            WriteLog("Logging initialized.");
            game.Services.AddService(typeof(ILog), this);
        }

        private void Write(string errorlvl, string message)
        {
            m_file.WriteLine(string.Format(FORMAT, DateTime.Now, errorlvl, message));
        }

        public void WriteLog(string message)
        {
            Write(LOG, message);
        }

        public void WriteLog(string message, params string[] parameters)
        {
           WriteLog(string.Format(message, parameters));
        }

        public void WriteError(string message)
        {
            Write(ERROR, message);
        }

        public void WriteError(string message, params string[] parameters)
        {
            WriteLog(string.Format(message, parameters));
        }

        public void Dispose() { m_file.Close(); }

        private const string FORMAT = "[{0}] {1}: {2}";

        private const string LOG = "Log";
        private const string ERROR = "Error";

        private TextWriter m_file;        
    }
}
