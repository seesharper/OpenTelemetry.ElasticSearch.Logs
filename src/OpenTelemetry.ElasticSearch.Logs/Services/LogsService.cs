using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Nest;
using Opentelemetry.Proto.Collector.Logs.V1;
using Opentelemetry.Proto.Logs.V1;
using OpenTelemetry.ElasticSearch.Logs.Exporter;

public class LogsService : Opentelemetry.Proto.Collector.Logs.V1.LogsService.LogsServiceBase
{
    private readonly IElasticClient elasticClient;
    private readonly ILogger<LogsService> logger;

    public LogsService(IElasticClient elasticClient, ILogger<LogsService> logger)
    {
        this.elasticClient = elasticClient;
        this.logger = logger;
    }

    public async override Task<ExportLogsServiceResponse> Export(ExportLogsServiceRequest request, ServerCallContext context)
    {
        foreach (var resourceLog in request.ResourceLogs)
        {


            var attributes = resourceLog.Resource.Attributes.ToDictionary(a => a.Key, a => a.Value.StringValue);

            var indexFormat = $"{attributes["service.name"].ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";

            var logEntries = new List<LogEntry>();
            foreach (var instrumentedLibraryLogs in resourceLog.InstrumentationLibraryLogs)
            {
                foreach (LogRecord logRecord in instrumentedLibraryLogs.Logs)
                {
                    var logEntry = new LogEntry
                    {
                        Level = logRecord.SeverityText,
                        TraceId = logRecord.TraceId.ToHexString(),
                        SpanId = logRecord.SpanId.ToHexString(),
                        Message = logRecord.Body.StringValue,
                        MessageTemplate = logRecord.Body.StringValue,
                        CategoryName = logRecord.Name,
                        TimeStamp = DateTimeOffset.FromUnixTimeMilliseconds((long)logRecord.TimeUnixNano / 1000000)
                    };
                    logEntries.Add(logEntry);
                }
            }
            if (logEntries.Count > 0)
            {
                logger.LogInformation($"Indexing {logEntries.Count} to elastic search");
                await elasticClient.IndexManyAsync(logEntries, indexFormat);
            }
        }

        var response = new ExportLogsServiceResponse();
        return response;
    }


    public class LogEntry
    {
        public string Id { get => Guid.NewGuid().ToString(); }

        [Date(Name = "@timeStamp")]
        public DateTimeOffset TimeStamp { get; set; }

        public string Level { get; set; }

        public string MessageTemplate { get; set; }

        public string Message { get; set; }

        [Text(Name = "trace.id")]
        public string TraceId { get; set; }

        public string SpanId { get; set; }

        public string CategoryName { get; set; }
    }
}