using System.Collections.Concurrent;

namespace FastLogger
{
    // the FastLogger class enqueues log entries into a ConcurrentQueue and starts a background task to write them to the log file.
    // This allows multiple callers to log entries simultaneously while ensuring that the log entries are written in the order they were added.
    // The program waits for a while to allow the background tasks to complete before exiting.
    public class FastLogger
    {
        private readonly ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();
        private readonly string logFilePath;
        private readonly object fileLock = new object();
        private bool isWriting;

        public FastLogger(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        public void Log(string logEntry)
        {
            logQueue.Enqueue(logEntry);

            if (!isWriting)
            {
                lock (fileLock)
                {
                    if (!isWriting)
                    {
                        isWriting = true;
                        Task.Factory.StartNew(WriteLogEntries, TaskCreationOptions.LongRunning);
                    }
                }
            }
        }

        private void WriteLogEntries()
        {
            using (var writer = new StreamWriter(logFilePath, true))
            {
                while (logQueue.TryDequeue(out string logEntry))
                {
                    writer.WriteLine(logEntry);
                }
            }

            isWriting = false;
        }
    }
}
