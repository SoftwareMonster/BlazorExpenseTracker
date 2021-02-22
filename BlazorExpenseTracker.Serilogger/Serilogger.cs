using System;
using Serilog;
using Serilog.Context;

namespace BlazorExpenseTracker.Serilogger
{
    public class Serilogger
    {
        public Serilogger()
        {
            ConfigureLogger(DateTime.Now.ToShortDateString().Replace("/", "-"));
        }

        private static void ConfigureLogger(string day)
        {
            // Create Logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Async(a => a.File($"{day}/log.log",
                    buffered: true,
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}]{UserId} - {Event} : {MachineName} {ThreadId} {Message} {Exception:1} {NewLine}",
                    // rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 10))
                .Enrich.WithThreadId()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .CreateLogger();
        }

        public void GetInformattion(Exception exception, string message, string eventLog, string user)
        {
            using (LogContext.PushProperty("UserId", user))
            using (LogContext.PushProperty("Event", eventLog))
            {
                Log.Information(exception, message);
            }

            Log.CloseAndFlush();
        }
    }
}