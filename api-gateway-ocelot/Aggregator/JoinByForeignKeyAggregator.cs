using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ocelot.Middleware;
using Ocelot.Middleware.Multiplexer;

namespace ApiGatewayOcelot.Aggregator
{
    public class JoinByForeignKeyAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<DownstreamContext> responses)
        {
            return await JoinAggregateContent(responses);
        }

        private static async Task<DownstreamResponse> JoinAggregateContent(List<DownstreamContext> downstreamContexts)
        {
            var jsonPayload = new String("");
            var responseKeys = downstreamContexts.ToDictionary(k => k.DownstreamReRoute.Key);
            var aggregatedPayload = new Dictionary<string, string>();
            foreach (var item in responseKeys)
            {
                var contentCompresed = await item.Value.DownstreamResponse.Content.ReadAsStreamAsync();
                using (var gsr = new GZipStream(contentCompresed, CompressionMode.Decompress))
                using (var sr = new StreamReader(gsr))
                {
                    string contentDecompresed = await sr.ReadToEndAsync();  
                    var payload = JsonSerializer.Deserialize<Dictionary<string, string>>(contentDecompresed);
                }

            }

            var resultString = JsonSerializer.Serialize(aggregatedPayload);
            var stringContent = new StringContent(resultString, Encoding.UTF8, "application/json");
            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "downstream data was joined");
        }

    }
}