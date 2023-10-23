# FastLogger
### This is a unit test project where I want to log 10k lines as fast as possible.

**In my attempt to achieve this and to maximize its performance, the FastLogger class enqueues log entries into a ConcurrentQueue and starts a background task to write them to the log file. This allows multiple callers to log entries simultaneously while ensuring that the log entries are written in the order they were added.** <br/> <br/>
**The program waits for a while to allow the background tasks to complete before exiting.** <br/>
**You can adjust the delay according to your requirements.**
