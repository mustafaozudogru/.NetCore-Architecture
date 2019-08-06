using System;
using System.IO;
using System.Net;
using CoreArchitectureDesign.Business.Abstractions;
using CoreArchitectureDesign.Core.Enums;
using CoreArchitectureDesign.Core.Interfaces;
using CoreArchitectureDesign.Core.Log;
using CoreArchitectureDesign.Core.Services;
using CoreArchitectureDesign.Core.Utilities;
using CoreArchitectureDesign.Data.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreArchitectureDesign.Business
{
    public class EventLogService : EntityService<EventLog>, IEventLog, ILogger
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEventLogDal eventLog;

        public EventLogService(IEventLogDal eventLog, IUnitOfWork unitOfWork) : base(eventLog, unitOfWork)
        {
            this.eventLog = eventLog;
            this.unitOfWork = unitOfWork;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel.GetHashCode() >= Utility.DefaultLogLevel.GetHashCode();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            if (exception != null)
            {
                message += "\n" + exception;
            }

            var eventLog = new EventLog
            {
                Message = message,
                EventId = eventId.Id,
                LogLevel = logLevel.ToString(),
                CreatedTime = DateTime.UtcNow,
                IpAdress = Utility.GetIp4Adress(),
                HostName = Dns.GetHostName()
            };

            InsertLog(eventLog);
        }

        private void InsertLog(EventLog eventLog)
        {
            if (Utility.LogType.Contains(LogType.Db.ToString()))
            {
                InsertDbLog(eventLog);
            }
            if (Utility.LogType.Contains(LogType.File.ToString()))
            {
                WriteTextToFile(eventLog);
            }
        }

        private void InsertDbLog(EventLog eventLog)
        {
            try
            {
                this.eventLog.Add(eventLog);
                this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                WriteTextToFile(eventLog);
                WriteTextToFile(new EventLog
                {
                    EventId = LoggingEvents.InsertItem,
                    HostName = eventLog.HostName,
                    IpAdress = eventLog.IpAdress,
                    LogLevel = eventLog.LogLevel,
                    Message = ex.ToString()
                });
            }
        }

        private void WriteTextToFile(EventLog eventLog)
        {
            try
            {
                var message = JsonConvert.SerializeObject(eventLog) + ",";

                var filePath = Utility.LogWriteTextToFile;
                var fileInfo = new FileInfo(filePath);
                if (!Directory.Exists(fileInfo.DirectoryName))
                {
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                }
                using (var streamWriter = new StreamWriter(filePath, true))
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
