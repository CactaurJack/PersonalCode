using System;

namespace EmawEngineLibrary.Logging
{
    public interface ILog : IDisposable
    {
        void WriteLog(string message);
        void WriteLog(string message, params string[] parameters);

        void WriteError(string message);
        void WriteError(string message, params string[] parameters);
    }
}