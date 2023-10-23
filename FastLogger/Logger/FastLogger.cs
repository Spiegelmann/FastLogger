using System.Collections.Concurrent;

namespace FastLogger
{
    // To log 10,000 lines as fast as possible to a text file in C#,
    // you can utilize a combination of multithreading and buffering to achieve better performance.
    // Below is a demonstration on how to achieve this.
    // The class uses a ConcurrentQueue to collect log entries from multiple callers and writes them to a text file in an ordered manner.
    // It also uses multithreading to maximize performance.
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
