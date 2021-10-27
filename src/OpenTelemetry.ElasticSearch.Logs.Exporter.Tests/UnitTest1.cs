
using System;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nest;
using OpenTelemetry.Logs;
using Serilog;
using Xunit;

namespace OpenTelemetry.ElasticSearch.Logs.Exporter.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            // ByteString bytestring = ByteString.CopyFrom(127, 143, 123, 110, 179, 220, 167, 217, 138, 199, 44, 46, 56, 187, 188, 214);
            // var test2 = bytestring.ToString(Encoding.UTF8);


            // var test = bytestring.ToBase64();
            // //{{ "timeUnixNano": "1635249984280992000", "severityText": "Information", "name": "Microsoft.AspNetCore.Hosting.Diagnostics", "body": { "stringValue": "Request starting HTTP/1.1 GET http://localhost:5200/weatherforecast - -" }, "attributes": [ { "key": "EventId", "value": { "stringValue": "1" } } ], "traceId": "f497brPcp9mKxywuOLu81g==", "spanId": "vSB/nQgERQc=" }}

            // var node = new Uri("http://localhost:9200");
            // var settings = new ConnectionSettings(node);
            // settings.DefaultIndex("people");
            // var client = new ElasticClient(settings);

            // try
            // {
            //     //await client.IndexDocumentAsync<LogEntry>(new LogEntry() { Message = "This is an error message", Level = "Warning", MessageTemplate = "This is a message template", TimeStamp = DateTime.UtcNow });
            // }
            // catch (System.Exception ex)
            // {
            //     throw;
            // }


            // //return;
            // var serviceCollection = new ServiceCollection();
            // serviceCollection.AddLogging(builder =>
            // {
            //     LoggerConfiguration loggerConfiguration = new LoggerConfiguration();


            //     builder.AddSerilogWithElasticSearch(null);

            //     builder.AddOpenTelemetry(config =>
            //     {
            //         config.AddConsoleExporter();
            //         config.ParseStateValues = true;
            //         config.IncludeFormattedMessage = true;
            //         //config.AddOtlpExporter(config => config.Endpoint = new Uri("http://"));
            //     }).AddConsole();
            // });


            // var serviceProvider = serviceCollection.BuildServiceProvider();

            // var logger = serviceProvider.GetRequiredService<ILogger<UnitTest1>>();

            // logger.LogWarning("TST");
        }
    }


    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }



}
