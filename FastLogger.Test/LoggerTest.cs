namespace FastLogger.Test
{
    public class LoggerTest
    {
        [Fact]
        public void Log()
        {
            // Output path relative to project folder (~/FastLogger/FastLogger.Test/log.txt). This can be changed
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "log.txt");
            FastLogger logger = new FastLogger(path);

            for (int i = 0; i < 10000; i++)
            {
                logger.Log(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + $" - Log Entry {i}");
            }
        }
    }
}
